using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using CRM.BLL.Common;
using CRM.BLL.Common.Pagination;
using CRM.BLL.Modules.Leads.DTOs;
using CRM.BLL.Modules.Leads.Interfaces;
using CRM.DAL.Modules.Leads.Repositories;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Modules.Leads.Entities;

namespace CRM.BLL.Modules.Leads.Services
{
    public class LeadService : ILeadService
    {
        private readonly ILeadRepository _leadRepo;
        private readonly ILeadPreferenceRepository _preferenceRepo;
        private readonly ILeadInterestedUnitRepository _interestedRepo;
        private readonly ILeadAssignmentRepository _assignmentRepo;
        private readonly IUnitOfWork _unitOfWork;

        public LeadService(ILeadRepository leadRepo, ILeadPreferenceRepository preferenceRepo, ILeadInterestedUnitRepository interestedRepo, ILeadAssignmentRepository assignmentRepo, IUnitOfWork unitOfWork)
        {
            _leadRepo = leadRepo;
            _preferenceRepo = preferenceRepo;
            _interestedRepo = interestedRepo;
            _assignmentRepo = assignmentRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<LeadListDto>> GetPagedAsync(PageRequest pageRequest, LeadFilter filter)
        {
            var data = await _leadRepo.GetPagedAsync(pageRequest.Page, pageRequest.PageSize, filter.LeadStatusId, filter.LeadSourceId, filter.ProjectId, filter.AssignedUserId, filter.ContactId, filter.IsActive, filter.Search);

            var items = data.Items.Select(l => new LeadListDto
            {
                Id = l.Id,
                LeadNumber = l.LeadNumber,
                ContactId = l.ContactId,
                ContactName = l.Contact != null ? $"{l.Contact.FirstName} {l.Contact.LastName}".Trim() : null,
                ProjectId = l.ProjectId,
                ProjectName = l.Project?.Name,
                LeadSourceId = l.LeadSourceId,
                LeadSourceName = l.LeadSource?.Name,
                LeadStatusId = l.LeadStatusId,
                LeadStatusName = l.LeadStatus?.Name,
                AssignedUserId = l.AssignedUserId,
                ReceivedAt = l.ReceivedAt,
                IsActive = l.IsActive
            }).ToList();

            return new PagedResult<LeadListDto>
            {
                Items = items,
                TotalCount = data.TotalCount,
                Page = pageRequest.Page,
                PageSize = pageRequest.PageSize
            };
        }

        public async Task<ServiceResult<LeadDetailsDto>> GetByIdAsync(int id)
        {
            var lead = await _leadRepo.GetByIdWithDetailsAsync(id);
            if (lead == null) return ServiceResult<LeadDetailsDto>.NotFound();

            var dto = MapToDetails(lead);
            return ServiceResult<LeadDetailsDto>.Success(dto);
        }

        public async Task<ServiceResult<LeadDetailsDto>> CreateAsync(CreateLeadDto dto)
        {
            // Validate LeadNumber uniqueness including deleted
            if (await _leadRepo.LeadNumberExistsAsync(dto.LeadNumber))
                return ServiceResult<LeadDetailsDto>.Conflict("LeadNumber already exists");

            // Validate Contact exists and not deleted
            if (!await _leadRepo.ContactExistsAsync(dto.ContactId))
                return ServiceResult<LeadDetailsDto>.ValidationError("Contact is invalid or deleted");

            // Validate Project if supplied
            if (dto.ProjectId.HasValue && !await _leadRepo.ProjectExistsAsync(dto.ProjectId.Value))
                return ServiceResult<LeadDetailsDto>.ValidationError("Project is invalid or deleted");

            // Validate LeadSource
            if (!await _leadRepo.LeadSourceExistsAsync(dto.LeadSourceId))
                return ServiceResult<LeadDetailsDto>.ValidationError("LeadSource is invalid");

            // Validate LeadStatus
            var status = await _leadRepo.GetLeadStatusByIdAsync(dto.LeadStatusId);
            if (status == null || status.IsDeleted)
                return ServiceResult<LeadDetailsDto>.ValidationError("LeadStatus is invalid");

            // LOST status requires LostReason
            if (status.Code == "LOST")
            {
                if (!dto.LostReasonId.HasValue)
                    return ServiceResult<LeadDetailsDto>.ValidationError("LostReasonId is required when status is LOST");

                if (!await _leadRepo.LostReasonExistsAsync(dto.LostReasonId.Value))
                    return ServiceResult<LeadDetailsDto>.ValidationError("LostReason is invalid");
            }
            else
            {
                if (dto.LostReasonId.HasValue)
                    return ServiceResult<LeadDetailsDto>.ValidationError("LostReasonId must be null unless status is LOST");
            }

            // Validate AssignedUser if supplied
            if (dto.AssignedUserId.HasValue && !await _leadRepo.UserExistsAsync(dto.AssignedUserId.Value))
                return ServiceResult<LeadDetailsDto>.ValidationError("Assigned user is invalid or deleted");

            var lead = new Lead
            {
                LeadNumber = dto.LeadNumber,
                ContactId = dto.ContactId,
                ProjectId = dto.ProjectId,
                LeadSourceId = dto.LeadSourceId,
                LeadStatusId = dto.LeadStatusId,
                LostReasonId = dto.LostReasonId,
                AssignedUserId = dto.AssignedUserId,
                ReceivedAt = dto.ReceivedAt,
                FirstContactAt = dto.FirstContactAt,
                QualifiedAt = dto.QualifiedAt,
                EstimatedBudget = dto.EstimatedBudget,
                Notes = dto.Notes
            };

            if (status.Code == "LOST")
            {
                lead.LostAt = DateTime.UtcNow;
            }

            if (status.Code == "CONVERTED")
            {
                lead.ConvertedAt = DateTime.UtcNow;
            }

            // attach initial status history
            var history = new LeadStatusHistory
            {
                Lead = lead,
                LeadStatusId = dto.LeadStatusId,
                ChangedAt = DateTime.UtcNow
            };
            lead.StatusHistory.Add(history);

            // If AssignedUser supplied, create assignment
            if (dto.AssignedUserId.HasValue)
            {
                var assignment = new LeadAssignment
                {
                    Lead = lead,
                    UserId = dto.AssignedUserId.Value,
                    AssignedAt = DateTime.UtcNow,
                    IsActive = true
                };
                lead.Assignments.Add(assignment);
                lead.AssignedUserId = dto.AssignedUserId;
            }

            await _unitOfWork.SaveChangesAsync();

            var created = await _leadRepo.GetByIdWithDetailsAsync(lead.Id);
            return ServiceResult<LeadDetailsDto>.Success(MapToDetails(created!));
        }

        public async Task<ServiceResult<LeadDetailsDto>> UpdateAsync(int id, UpdateLeadDto dto)
        {
            var lead = await _leadRepo.GetByIdForUpdateAsync(id);
            if (lead == null) return ServiceResult<LeadDetailsDto>.NotFound();

            // LeadNumber uniqueness
            if (lead.LeadNumber != dto.LeadNumber)
            {
                if (await _leadRepo.LeadNumberExistsAsync(dto.LeadNumber, id))
                    return ServiceResult<LeadDetailsDto>.Conflict("LeadNumber already exists");
                lead.LeadNumber = dto.LeadNumber;
            }

            // Validate Contact
            if (!await _leadRepo.ContactExistsAsync(dto.ContactId))
                return ServiceResult<LeadDetailsDto>.ValidationError("Contact is invalid or deleted");

            // Validate Project
            if (dto.ProjectId.HasValue && !await _leadRepo.ProjectExistsAsync(dto.ProjectId.Value))
                return ServiceResult<LeadDetailsDto>.ValidationError("Project is invalid or deleted");

            // Validate LeadSource
            if (!await _leadRepo.LeadSourceExistsAsync(dto.LeadSourceId))
                return ServiceResult<LeadDetailsDto>.ValidationError("LeadSource is invalid");

            // Handle status change and validations (centralized)
            var statusResult = await ProcessStatusChangeAsync(lead, dto.LeadStatusId, dto.LostReasonId, null, null, null);
            if (statusResult.Status == ServiceResultStatus.ValidationError)
                return ServiceResult<LeadDetailsDto>.ValidationError(statusResult.ErrorMessage);

            // Update basic fields
            lead.ContactId = dto.ContactId;
            lead.ProjectId = dto.ProjectId;
            lead.LeadSourceId = dto.LeadSourceId;
            lead.LeadStatusId = dto.LeadStatusId;
            lead.FirstContactAt = dto.FirstContactAt;
            lead.QualifiedAt = dto.QualifiedAt;
            lead.EstimatedBudget = dto.EstimatedBudget;
            lead.Notes = dto.Notes;
            lead.UpdatedAt = DateTime.UtcNow;

            _leadRepo.Update(lead);
            await _unitOfWork.SaveChangesAsync();

            var updated = await _leadRepo.GetByIdWithDetailsAsync(lead.Id);
            return ServiceResult<LeadDetailsDto>.Success(MapToDetails(updated!));
        }

        private async Task<ServiceResult<bool>> ProcessStatusChangeAsync(Lead lead, int newStatusId, int? lostReasonId, int? changedByUserId, string? reason, string? notes)
        {
            // Validate target status
            var newStatus = await _leadRepo.GetLeadStatusByIdAsync(newStatusId);
            if (newStatus == null || newStatus.IsDeleted || !newStatus.IsActive)
                return ServiceResult<bool>.ValidationError("LeadStatus is invalid");

            // Validate changed-by user if supplied
            if (changedByUserId.HasValue && !await _leadRepo.UserExistsAsync(changedByUserId.Value))
                return ServiceResult<bool>.ValidationError("ChangedByUserId is invalid or deleted");

            var oldStatus = await _leadRepo.GetLeadStatusByIdAsync(lead.LeadStatusId);
            if (oldStatus == null)
                oldStatus = new LeadStatus { Id = lead.LeadStatusId, Code = string.Empty };

            if (oldStatus.Id == newStatus.Id)
            {
                // No real change
                return ServiceResult<bool>.Success(false);
            }

            // LOST rules
            if (newStatus.Code == "LOST")
            {
                if (!lostReasonId.HasValue)
                    return ServiceResult<bool>.ValidationError("LostReasonId is required when status is LOST");

                if (!await _leadRepo.LostReasonExistsAsync(lostReasonId.Value))
                    return ServiceResult<bool>.ValidationError("LostReason is invalid");

                lead.LostAt = DateTime.UtcNow;
                lead.LostReasonId = lostReasonId;
            }
            else
            {
                if (lostReasonId.HasValue)
                    return ServiceResult<bool>.ValidationError("LostReasonId must be null unless status is LOST");

                // moving away from LOST
                lead.LostAt = null;
                lead.LostReasonId = null;
            }

            // CONVERTED rules
            if (newStatus.Code == "CONVERTED")
            {
                if (!lead.ConvertedAt.HasValue)
                    lead.ConvertedAt = DateTime.UtcNow;
            }

            // update lead and add a single history row
            lead.LeadStatusId = newStatusId;
            lead.UpdatedAt = DateTime.UtcNow;

            var history = new LeadStatusHistory
            {
                LeadId = lead.Id,
                LeadStatusId = newStatusId,
                ChangedAt = DateTime.UtcNow,
                ChangedByUserId = changedByUserId,
                Reason = reason,
                Notes = notes
            };
            lead.StatusHistory.Add(history);

            return ServiceResult<bool>.Success(true);
        }

        public async Task<ServiceResult<bool>> ChangeStatusAsync(int id, ChangeLeadStatusRequest request)
        {
            var lead = await _leadRepo.GetByIdForUpdateAsync(id);
            if (lead == null) return ServiceResult<bool>.NotFound();

            var result = await ProcessStatusChangeAsync(lead, request.LeadStatusId, request.LostReasonId, request.ChangedByUserId, request.Reason, request.Notes);
            if (result.Status == ServiceResultStatus.ValidationError)
                return ServiceResult<bool>.ValidationError(result.ErrorMessage);

            if (!result.Data)
            {
                // no change
                return ServiceResult<bool>.Success(true);
            }

            _leadRepo.Update(lead);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }

        public async Task<ServiceResult<System.Collections.Generic.List<LeadStatusHistoryDto>>> GetStatusHistoryAsync(int id)
        {
            var lead = await _leadRepo.GetByIdWithDetailsAsync(id);
            if (lead == null) return ServiceResult<System.Collections.Generic.List<LeadStatusHistoryDto>>.NotFound();

            var list = lead.StatusHistory == null
                ? new System.Collections.Generic.List<LeadStatusHistoryDto>()
                : lead.StatusHistory
                    .Where(h => !h.IsDeleted)
                    .OrderByDescending(h => h.ChangedAt)
                    .Select(h => new LeadStatusHistoryDto { Id = h.Id, LeadStatusId = h.LeadStatusId, LeadStatusName = h.LeadStatus?.Name, ChangedAt = h.ChangedAt, ChangedByUserId = h.ChangedByUserId, Reason = h.Reason, Notes = h.Notes })
                    .ToList();

            return ServiceResult<System.Collections.Generic.List<LeadStatusHistoryDto>>.Success(list);
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var lead = await _leadRepo.GetByIdForUpdateAsync(id);
            if (lead == null) return ServiceResult<bool>.NotFound();

            lead.IsDeleted = true;
            lead.IsActive = false;
            lead.UpdatedAt = DateTime.UtcNow;

            _leadRepo.Update(lead);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }

        public async Task<ServiceResult<bool>> AssignAsync(int id, AssignLeadRequest request)
        {
            var lead = await _leadRepo.GetByIdForUpdateAsync(id);
            if (lead == null) return ServiceResult<bool>.NotFound();

            if (request.AssignedUserId.HasValue && !await _leadRepo.UserExistsAsync(request.AssignedUserId.Value))
                return ServiceResult<bool>.ValidationError("Assigned user is invalid or deleted");

            // close existing active assignments
            var active = await _assignmentRepo.GetActiveAssignmentsForLeadAsync(id);
            foreach (var a in active)
            {
                if (request.AssignedUserId.HasValue && a.UserId == request.AssignedUserId.Value)
                {
                    // same user - do nothing
                    return ServiceResult<bool>.Success(true);
                }

                a.IsActive = false;
                a.UnassignedAt = DateTime.UtcNow;
                a.UpdatedAt = DateTime.UtcNow;
                _assignmentRepo.Update(a);
            }

            if (request.AssignedUserId.HasValue)
            {
                // create new assignment
                var assignment = new LeadAssignment
                {
                    LeadId = id,
                    UserId = request.AssignedUserId.Value,
                    AssignedAt = DateTime.UtcNow,
                    AssignedByUserId = request.AssignedByUserId,
                    Reason = request.Reason,
                    IsActive = true
                };
                await _assignmentRepo.AddAsync(assignment);
                lead.AssignedUserId = request.AssignedUserId;
            }
            else
            {
                // unassign
                lead.AssignedUserId = null;
            }

            lead.UpdatedAt = DateTime.UtcNow;
            _leadRepo.Update(lead);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }

        public async Task<ServiceResult<bool>> UnassignAsync(int id, AssignLeadRequest request)
        {
            request.AssignedUserId = null;
            return await AssignAsync(id, request);
        }

        public async Task<ServiceResult<LeadPreferenceDto>> GetPreferenceAsync(int id)
        {
            var pref = await _preferenceRepo.GetByLeadIdIncludingDeletedAsync(id);
            if (pref == null || pref.IsDeleted) return ServiceResult<LeadPreferenceDto>.NotFound();

            return ServiceResult<LeadPreferenceDto>.Success(MapPreference(pref));
        }

        public async Task<ServiceResult<LeadPreferenceDto>> UpsertPreferenceAsync(int id, UpsertLeadPreferenceRequest request)
        {
            // validate ranges
            if (request.MinBudget.HasValue && request.MaxBudget.HasValue && request.MinBudget > request.MaxBudget)
                return ServiceResult<LeadPreferenceDto>.ValidationError("MinBudget must be <= MaxBudget");

            if (request.MinRooms.HasValue && request.MaxRooms.HasValue && request.MinRooms > request.MaxRooms)
                return ServiceResult<LeadPreferenceDto>.ValidationError("MinRooms must be <= MaxRooms");

            if (request.MinArea.HasValue && request.MaxArea.HasValue && request.MinArea > request.MaxArea)
                return ServiceResult<LeadPreferenceDto>.ValidationError("MinArea must be <= MaxArea");

            if (request.MinFloor.HasValue && request.MaxFloor.HasValue && request.MinFloor > request.MaxFloor)
                return ServiceResult<LeadPreferenceDto>.ValidationError("MinFloor must be <= MaxFloor");

            var existing = await _preferenceRepo.GetByLeadIdIncludingDeletedAsync(id);
            if (existing != null)
            {
                // reactivate if soft-deleted
                existing.IsDeleted = false;
                existing.MinBudget = request.MinBudget;
                existing.MaxBudget = request.MaxBudget;
                existing.MinRooms = request.MinRooms;
                existing.MaxRooms = request.MaxRooms;
                existing.MinArea = request.MinArea;
                existing.MaxArea = request.MaxArea;
                existing.MinFloor = request.MinFloor;
                existing.MaxFloor = request.MaxFloor;
                existing.PreferredCity = request.PreferredCity;
                existing.WantsBalcony = request.WantsBalcony;
                existing.WantsGarden = request.WantsGarden;
                existing.WantsParking = request.WantsParking;
                existing.WantsStorage = request.WantsStorage;
                existing.RequiresAccessibility = request.RequiresAccessibility;
                existing.PreferredDirections = request.PreferredDirections;
                existing.Notes = request.Notes;
                existing.UpdatedAt = DateTime.UtcNow;

                _preferenceRepo.Update(existing);
            }
            else
            {
                var pref = new LeadPreference
                {
                    LeadId = id,
                    MinBudget = request.MinBudget,
                    MaxBudget = request.MaxBudget,
                    MinRooms = request.MinRooms,
                    MaxRooms = request.MaxRooms,
                    MinArea = request.MinArea,
                    MaxArea = request.MaxArea,
                    MinFloor = request.MinFloor,
                    MaxFloor = request.MaxFloor,
                    PreferredCity = request.PreferredCity,
                    WantsBalcony = request.WantsBalcony,
                    WantsGarden = request.WantsGarden,
                    WantsParking = request.WantsParking,
                    WantsStorage = request.WantsStorage,
                    RequiresAccessibility = request.RequiresAccessibility,
                    PreferredDirections = request.PreferredDirections,
                    Notes = request.Notes
                };
                await _preferenceRepo.AddAsync(pref);
            }

            await _unitOfWork.SaveChangesAsync();

            var saved = await _preferenceRepo.GetByLeadIdIncludingDeletedAsync(id);
            return ServiceResult<LeadPreferenceDto>.Success(MapPreference(saved!));
        }

        public async Task<ServiceResult<bool>> RemovePreferenceAsync(int id)
        {
            var existing = await _preferenceRepo.GetByLeadIdIncludingDeletedAsync(id);
            if (existing == null) return ServiceResult<bool>.NotFound();

            existing.IsDeleted = true;
            existing.UpdatedAt = DateTime.UtcNow;
            _preferenceRepo.Update(existing);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }

        public async Task<ServiceResult<LeadInterestedUnitDto>> AddInterestedUnitAsync(int id, AddInterestedUnitRequest request)
        {
            // Validate lead exists
            var lead = await _leadRepo.GetByIdForUpdateAsync(id);
            if (lead == null) return ServiceResult<LeadInterestedUnitDto>.NotFound();

            // Validate unit exists and not deleted using repository
            if (!await _leadRepo.UnitExistsAsync(request.UnitId))
                return ServiceResult<LeadInterestedUnitDto>.ValidationError("Unit is invalid or deleted");

            var existing = await _interestedRepo.GetByLeadAndUnitIncludingDeletedAsync(id, request.UnitId);
            if (existing != null && !existing.IsDeleted && existing.IsActive)
                return ServiceResult<LeadInterestedUnitDto>.Conflict("Interested unit link already exists");

            if (existing != null)
            {
                existing.IsDeleted = false;
                existing.IsActive = true;
                existing.AddedAt = DateTime.UtcNow;
                existing.AddedByUserId = request.AddedByUserId;
                existing.Notes = request.Notes;
                existing.UpdatedAt = DateTime.UtcNow;
                _interestedRepo.Update(existing);

                await _unitOfWork.SaveChangesAsync();
                return ServiceResult<LeadInterestedUnitDto>.Success(MapInterested(existing));
            }

            var link = new LeadInterestedUnit
            {
                LeadId = id,
                UnitId = request.UnitId,
                AddedAt = DateTime.UtcNow,
                AddedByUserId = request.AddedByUserId,
                Notes = request.Notes,
                IsActive = true
            };
            await _interestedRepo.AddAsync(link);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<LeadInterestedUnitDto>.Success(MapInterested(link));
        }

        public async Task<ServiceResult<bool>> RemoveInterestedUnitAsync(int id, int unitId)
        {
            var existing = await _interestedRepo.GetByLeadAndUnitIncludingDeletedAsync(id, unitId);
            if (existing == null) return ServiceResult<bool>.NotFound();

            existing.IsDeleted = true;
            existing.IsActive = false;
            existing.UpdatedAt = DateTime.UtcNow;
            _interestedRepo.Update(existing);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }

        private LeadDetailsDto MapToDetails(Lead l)
        {
            var dto = new LeadDetailsDto
            {
                Id = l.Id,
                LeadNumber = l.LeadNumber,
                ContactId = l.ContactId,
                ContactName = l.Contact != null ? $"{l.Contact.FirstName} {l.Contact.LastName}".Trim() : null,
                ProjectId = l.ProjectId,
                ProjectName = l.Project?.Name,
                LeadSourceId = l.LeadSourceId,
                LeadSourceName = l.LeadSource?.Name,
                LeadStatusId = l.LeadStatusId,
                LeadStatusName = l.LeadStatus?.Name,
                LostReasonId = l.LostReasonId,
                LostReasonName = l.LostReason?.Name,
                AssignedUserId = l.AssignedUserId,
                AssignedUserName = l.AssignedUser != null ? $"{l.AssignedUser.FirstName} {l.AssignedUser.LastName}".Trim() : null,
                ReceivedAt = l.ReceivedAt,
                FirstContactAt = l.FirstContactAt,
                QualifiedAt = l.QualifiedAt,
                ConvertedAt = l.ConvertedAt,
                LostAt = l.LostAt,
                EstimatedBudget = l.EstimatedBudget,
                Notes = l.Notes,
                IsActive = l.IsActive,
                Preference = l.Preference != null && !l.Preference.IsDeleted ? MapPreference(l.Preference) : null,
                InterestedUnits = l.InterestedUnits != null ? l.InterestedUnits.Where(i => !i.IsDeleted).Select(MapInterested).ToList() : new List<LeadInterestedUnitDto>(),
                StatusHistory = l.StatusHistory != null ? l.StatusHistory.Select(h => new LeadStatusHistoryDto { Id = h.Id, LeadStatusId = h.LeadStatusId, LeadStatusName = h.LeadStatus?.Name, ChangedAt = h.ChangedAt, ChangedByUserId = h.ChangedByUserId, Reason = h.Reason, Notes = h.Notes }).ToList() : new List<LeadStatusHistoryDto>(),
                Assignments = l.Assignments != null ? l.Assignments.Select(MapAssignment).ToList() : new List<LeadAssignmentDto>()
            };

            return dto;
        }

        private LeadPreferenceDto MapPreference(LeadPreference p)
        {
            return new LeadPreferenceDto
            {
                Id = p.Id,
                LeadId = p.LeadId,
                MinBudget = p.MinBudget,
                MaxBudget = p.MaxBudget,
                MinRooms = p.MinRooms,
                MaxRooms = p.MaxRooms,
                MinArea = p.MinArea,
                MaxArea = p.MaxArea,
                MinFloor = p.MinFloor,
                MaxFloor = p.MaxFloor,
                PreferredCity = p.PreferredCity,
                WantsBalcony = p.WantsBalcony,
                WantsGarden = p.WantsGarden,
                WantsParking = p.WantsParking,
                WantsStorage = p.WantsStorage,
                RequiresAccessibility = p.RequiresAccessibility,
                PreferredDirections = p.PreferredDirections,
                Notes = p.Notes
            };
        }

        private LeadInterestedUnitDto MapInterested(LeadInterestedUnit i)
        {
            return new LeadInterestedUnitDto
            {
                Id = i.Id,
                UnitId = i.UnitId,
                UnitName = i.Unit != null ? i.Unit.UnitNumber : null,
                AddedAt = i.AddedAt,
                AddedByUserId = i.AddedByUserId,
                Notes = i.Notes,
                IsActive = i.IsActive
            };
        }

        private LeadAssignmentDto MapAssignment(LeadAssignment a)
        {
            return new LeadAssignmentDto
            {
                Id = a.Id,
                UserId = a.UserId,
                UserName = a.User != null ? $"{a.User.FirstName} {a.User.LastName}".Trim() : null,
                AssignedAt = a.AssignedAt,
                UnassignedAt = a.UnassignedAt,
                AssignedByUserId = a.AssignedByUserId,
                Reason = a.Reason,
                IsActive = a.IsActive
            };
        }
    }
}
