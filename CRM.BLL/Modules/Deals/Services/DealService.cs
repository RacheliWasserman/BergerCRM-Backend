using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using CRM.BLL.Common;
using CRM.BLL.Common.Pagination;
using CRM.BLL.Modules.Deals.DTOs;
using CRM.BLL.Modules.Deals.Interfaces;
using CRM.DAL.Modules.Deals.Repositories;
using CRM.DAL.Common.Repositories;
using CRM.BLL.Audit;
using CRM.DAL.Modules.Deals.Entities;

namespace CRM.BLL.Modules.Deals.Services
{
    public class DealService : IDealService
    {
        private readonly IDealRepository _dealRepo;
        private readonly IAuditService _auditService;
        private readonly IUnitOfWork _unitOfWork;

        public DealService(IDealRepository dealRepo, IAuditService auditService, IUnitOfWork unitOfWork)
        {
            _dealRepo = dealRepo;
            _auditService = auditService;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<DealListDto>> GetPagedAsync(PageRequest pageRequest, DealFilter filter)
        {
            var data = await _dealRepo.GetPagedAsync(pageRequest.Page, pageRequest.PageSize, filter.ProjectId, filter.OwnerUserId, filter.ContactId, filter.DealStageId, filter.IsActive, filter.Search);

            var items = data.Items.Select(d => new DealListDto
            {
                Id = d.Id,
                DealNumber = d.DealNumber,
                LeadId = d.LeadId,
                LeadNumber = d.Lead?.GetType() != null ? null : null, // placeholder
                ContactId = d.ContactId,
                ContactName = d.Contact != null ? $"{d.Contact.FirstName} {d.Contact.LastName}".Trim() : null,
                CustomerId = d.CustomerId,
                CustomerName = d.Customer?.DisplayName,
                ProjectId = d.ProjectId,
                ProjectName = d.Project?.Name,
                UnitId = d.UnitId,
                UnitDisplay = d.Unit != null ? (d.Unit.UnitNumber + (string.IsNullOrWhiteSpace(d.Unit.InternalCode) ? "" : " (" + d.Unit.InternalCode + ")")) : null,
                DealStageId = d.DealStageId,
                DealStageCode = d.DealStage?.Code,
                DealStageName = d.DealStage?.Name,
                OwnerUserId = d.OwnerUserId,
                OwnerUserName = d.OwnerUser != null ? $"{d.OwnerUser.FirstName} {d.OwnerUser.LastName}".Trim() : null,
                ExpectedAmount = d.ExpectedAmount,
                OpenedAt = d.OpenedAt,
                ExpectedCloseAt = d.ExpectedCloseAt,
                ClosedAt = d.ClosedAt,
                Notes = d.Notes,
                IsActive = d.IsActive
            }).ToList();

            return new PagedResult<DealListDto>
            {
                Items = items,
                TotalCount = data.TotalCount,
                Page = pageRequest.Page,
                PageSize = pageRequest.PageSize
            };
        }

        public async Task<ServiceResult<DealDetailsDto>> GetByIdAsync(int id)
        {
            var deal = await _dealRepo.GetByIdWithDetailsAsync(id);
            if (deal == null) return ServiceResult<DealDetailsDto>.NotFound();

            return ServiceResult<DealDetailsDto>.Success(MapToDetails(deal));
        }

        public async Task<ServiceResult<DealDetailsDto>> CreateAsync(CreateDealDto dto)
        {
            // validations
            if (string.IsNullOrWhiteSpace(dto.DealNumber))
                return ServiceResult<DealDetailsDto>.ValidationError("DealNumber is required");

            if (await _dealRepo.DealNumberExistsAsync(dto.DealNumber))
                return ServiceResult<DealDetailsDto>.Conflict("DealNumber already exists");

            if (!await _dealRepo.ContactExistsAsync(dto.ContactId))
                return ServiceResult<DealDetailsDto>.ValidationError("Contact is invalid or deleted");

            if (!await _dealRepo.ProjectExistsAsync(dto.ProjectId))
                return ServiceResult<DealDetailsDto>.ValidationError("Project is invalid or deleted");

            if (dto.CustomerId.HasValue && !await _dealRepo.CustomerExistsAsync(dto.CustomerId.Value))
                return ServiceResult<DealDetailsDto>.ValidationError("Customer is invalid or deleted");

            if (dto.UnitId.HasValue && !await _dealRepo.UnitExistsAsync(dto.UnitId.Value))
                return ServiceResult<DealDetailsDto>.ValidationError("Unit is invalid or deleted");

            if (dto.OwnerUserId.HasValue && !await _dealRepo.UserExistsAsync(dto.OwnerUserId.Value))
                return ServiceResult<DealDetailsDto>.ValidationError("Owner user is invalid or deleted");

            if (dto.ExpectedAmount.HasValue && dto.ExpectedAmount < 0)
                return ServiceResult<DealDetailsDto>.ValidationError("ExpectedAmount must not be negative");

            // DealStage validation
            var stage = await _dealRepo.GetDealStageByIdAsync(dto.DealStageId);
            if (stage == null)
                return ServiceResult<DealDetailsDto>.ValidationError("DealStage is invalid");

            // date validation
            if (dto.ExpectedCloseAt.HasValue && dto.ExpectedCloseAt < dto.OpenedAt)
                return ServiceResult<DealDetailsDto>.ValidationError("ExpectedCloseAt must not be earlier than OpenedAt");

            var deal = new Deal
            {
                DealNumber = dto.DealNumber,
                LeadId = dto.LeadId,
                ContactId = dto.ContactId,
                CustomerId = dto.CustomerId,
                ProjectId = dto.ProjectId,
                UnitId = dto.UnitId,
                DealStageId = dto.DealStageId,
                OwnerUserId = dto.OwnerUserId,
                ExpectedAmount = dto.ExpectedAmount,
                OpenedAt = dto.OpenedAt,
                ExpectedCloseAt = dto.ExpectedCloseAt,
                ClosedAt = stage.IsClosed ? dto.ClosedAt ?? DateTime.UtcNow : null,
                Notes = dto.Notes,
                IsActive = dto.IsActive
            };

            // initial stage history
            var history = new DealStageHistory
            {
                Deal = deal,
                DealStageId = dto.DealStageId,
                ChangedAt = DateTime.UtcNow,
                ChangedByUserId = dto.OwnerUserId
            };
            deal.StageHistory.Add(history);

            await _dealRepo.AddAsync(deal);

            // Audit: Prepare concise new values
            var newValues = new { dto.DealNumber, dto.ContactId, dto.ProjectId, dto.UnitId, dto.DealStageId, dto.OwnerUserId, dto.ExpectedAmount, dto.OpenedAt, dto.ExpectedCloseAt, dto.ClosedAt };
            await _auditService.LogAsync("CREATE", "Deal", null, dto.OwnerUserId, null, newValues);

            await _unitOfWork.SaveChangesAsync();

            var saved = await _dealRepo.GetByIdWithDetailsAsync(deal.Id);
            return ServiceResult<DealDetailsDto>.Success(MapToDetails(saved!));
        }

        public async Task<ServiceResult<DealDetailsDto>> UpdateAsync(int id, UpdateDealDto dto)
        {
            var existing = await _dealRepo.GetByIdForUpdateAsync(id);
            if (existing == null) return ServiceResult<DealDetailsDto>.NotFound();

            if (string.IsNullOrWhiteSpace(dto.DealNumber))
                return ServiceResult<DealDetailsDto>.ValidationError("DealNumber is required");

            if (await _dealRepo.DealNumberExistsAsync(dto.DealNumber, id))
                return ServiceResult<DealDetailsDto>.Conflict("DealNumber already exists");

            if (!await _dealRepo.ContactExistsAsync(dto.ContactId))
                return ServiceResult<DealDetailsDto>.ValidationError("Contact is invalid or deleted");

            if (!await _dealRepo.ProjectExistsAsync(dto.ProjectId))
                return ServiceResult<DealDetailsDto>.ValidationError("Project is invalid or deleted");

            if (dto.CustomerId.HasValue && !await _dealRepo.CustomerExistsAsync(dto.CustomerId.Value))
                return ServiceResult<DealDetailsDto>.ValidationError("Customer is invalid or deleted");

            if (dto.UnitId.HasValue && !await _dealRepo.UnitExistsAsync(dto.UnitId.Value))
                return ServiceResult<DealDetailsDto>.ValidationError("Unit is invalid or deleted");

            if (dto.OwnerUserId.HasValue && !await _dealRepo.UserExistsAsync(dto.OwnerUserId.Value))
                return ServiceResult<DealDetailsDto>.ValidationError("Owner user is invalid or deleted");

            if (dto.ExpectedAmount.HasValue && dto.ExpectedAmount < 0)
                return ServiceResult<DealDetailsDto>.ValidationError("ExpectedAmount must not be negative");

            if (dto.ExpectedCloseAt.HasValue && dto.ExpectedCloseAt < dto.OpenedAt)
                return ServiceResult<DealDetailsDto>.ValidationError("ExpectedCloseAt must not be earlier than OpenedAt");

            // capture old values for audit
            var oldValues = new { existing.DealNumber, existing.ContactId, existing.ProjectId, existing.UnitId, existing.DealStageId, existing.OwnerUserId, existing.ExpectedAmount, existing.OpenedAt, existing.ExpectedCloseAt, existing.ClosedAt };

            existing.DealNumber = dto.DealNumber;
            existing.LeadId = dto.LeadId;
            existing.ContactId = dto.ContactId;
            existing.CustomerId = dto.CustomerId;
            existing.ProjectId = dto.ProjectId;
            existing.UnitId = dto.UnitId;
            existing.OwnerUserId = dto.OwnerUserId;
            existing.ExpectedAmount = dto.ExpectedAmount;
            existing.OpenedAt = dto.OpenedAt;
            existing.ExpectedCloseAt = dto.ExpectedCloseAt;
            existing.Notes = dto.Notes;
            existing.IsActive = dto.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            _dealRepo.Update(existing);

            var newValues = new { existing.DealNumber, existing.ContactId, existing.ProjectId, existing.UnitId, existing.DealStageId, existing.OwnerUserId, existing.ExpectedAmount, existing.OpenedAt, existing.ExpectedCloseAt, existing.ClosedAt };
            await _auditService.LogAsync("UPDATE", "Deal", existing.Id.ToString(), null, oldValues, newValues);

            await _unitOfWork.SaveChangesAsync();

            var saved = await _dealRepo.GetByIdWithDetailsAsync(existing.Id);
            return ServiceResult<DealDetailsDto>.Success(MapToDetails(saved!));
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id, int? userId = null)
        {
            var existing = await _dealRepo.GetByIdForUpdateAsync(id);
            if (existing == null) return ServiceResult<bool>.NotFound();

            var oldValues = new { existing.DealNumber, existing.IsDeleted };

            existing.IsDeleted = true;
            existing.UpdatedAt = DateTime.UtcNow;
            _dealRepo.Update(existing);

            await _auditService.LogAsync("DELETE", "Deal", existing.Id.ToString(), userId, oldValues, new { existing.IsDeleted });

            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }

        public async Task<ServiceResult<DealDetailsDto>> ChangeStageAsync(int id, ChangeDealStageRequest request)
        {
            var existing = await _dealRepo.GetByIdForUpdateAsync(id);
            if (existing == null) return ServiceResult<DealDetailsDto>.NotFound();

            var newStage = await _dealRepo.GetDealStageByIdAsync(request.DealStageId);
            if (newStage == null) return ServiceResult<DealDetailsDto>.ValidationError("DealStage is invalid");

            if (existing.DealStageId == request.DealStageId)
            {
                var current = await _dealRepo.GetByIdWithDetailsAsync(existing.Id);
                return ServiceResult<DealDetailsDto>.Success(MapToDetails(current!));
            }

            var oldStageId = existing.DealStageId;

            // apply ClosedAt rules
            if (!existing.ClosedAt.HasValue && newStage.IsClosed)
            {
                existing.ClosedAt = DateTime.UtcNow;
            }
            else if (existing.ClosedAt.HasValue && !newStage.IsClosed)
            {
                existing.ClosedAt = null;
            }

            existing.DealStageId = request.DealStageId;
            existing.UpdatedAt = DateTime.UtcNow;
            _dealRepo.Update(existing);

            var history = new DealStageHistory
            {
                DealId = existing.Id,
                DealStageId = request.DealStageId,
                ChangedAt = DateTime.UtcNow,
                ChangedByUserId = request.ChangedByUserId,
                Reason = request.Reason,
                Notes = request.Notes
            };
            await _dealRepo.AddStageHistoryAsync(history);

            await _auditService.LogAsync("STAGE_CHANGE", "Deal", existing.Id.ToString(), request.ChangedByUserId, new { OldStageId = oldStageId }, new { NewStageId = request.DealStageId, request.Reason, request.Notes });

            await _unitOfWork.SaveChangesAsync();

            var saved = await _dealRepo.GetByIdWithDetailsAsync(existing.Id);
            return ServiceResult<DealDetailsDto>.Success(MapToDetails(saved!));
        }

        public async Task<ServiceResult<List<DealStageHistoryDto>>> GetStageHistoryAsync(int id)
        {
            var history = await _dealRepo.GetStageHistoryAsync(id);
            var dto = history.Select(h => new DealStageHistoryDto
            {
                DealStageId = h.DealStageId,
                DealStageCode = h.DealStage?.Code,
                DealStageName = h.DealStage?.Name,
                ChangedAt = h.ChangedAt,
                ChangedByUserId = h.ChangedByUserId,
                ChangedByUserName = h.ChangedByUser != null ? $"{h.ChangedByUser.FirstName} {h.ChangedByUser.LastName}".Trim() : null,
                Reason = h.Reason,
                Notes = h.Notes
            }).ToList();

            return ServiceResult<List<DealStageHistoryDto>>.Success(dto);
        }

        private DealDetailsDto MapToDetails(Deal d)
        {
            return new DealDetailsDto
            {
                Id = d.Id,
                DealNumber = d.DealNumber,
                LeadId = d.LeadId,
                LeadNumber = d.Lead != null ? null : null,
                ContactId = d.ContactId,
                ContactName = d.Contact != null ? $"{d.Contact.FirstName} {d.Contact.LastName}".Trim() : null,
                CustomerId = d.CustomerId,
                CustomerName = d.Customer?.DisplayName,
                ProjectId = d.ProjectId,
                ProjectName = d.Project?.Name,
                UnitId = d.UnitId,
                UnitDisplay = d.Unit != null ? (d.Unit.UnitNumber + (string.IsNullOrWhiteSpace(d.Unit.InternalCode) ? "" : " (" + d.Unit.InternalCode + ")")) : null,
                DealStageId = d.DealStageId,
                DealStageCode = d.DealStage?.Code,
                DealStageName = d.DealStage?.Name,
                OwnerUserId = d.OwnerUserId,
                OwnerUserName = d.OwnerUser != null ? $"{d.OwnerUser.FirstName} {d.OwnerUser.LastName}".Trim() : null,
                ExpectedAmount = d.ExpectedAmount,
                OpenedAt = d.OpenedAt,
                ExpectedCloseAt = d.ExpectedCloseAt,
                ClosedAt = d.ClosedAt,
                Notes = d.Notes,
                IsActive = d.IsActive
            };
        }
    }
}
