using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using CRM.BLL.Common;
using CRM.BLL.Common.Pagination;
using CRM.BLL.Modules.Activities.DTOs;
using CRM.BLL.Modules.Activities.Interfaces;
using CRM.DAL.Modules.Activities.Repositories;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Modules.Activities.Entities;

namespace CRM.BLL.Modules.Activities.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _repo;
        private readonly IUnitOfWork _unitOfWork;

        public ActivityService(IActivityRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<ActivityListDto>> GetPagedAsync(PageRequest pageRequest, ActivityFilter filter)
        {
            var data = await _repo.GetPagedAsync(pageRequest.Page, pageRequest.PageSize, filter.ActivityTypeId, filter.ContactId, filter.CustomerId, filter.LeadId, filter.ProjectId, filter.UnitId, filter.UserId);

            var items = data.Items.Select(a => new ActivityListDto
            {
                Id = a.Id,
                ActivityTypeId = a.ActivityTypeId,
                ActivityTypeName = a.ActivityType?.Name,
                ActivityAt = a.ActivityAt,
                ContactId = a.ContactId,
                ContactName = a.Contact != null ? $"{a.Contact.FirstName} {a.Contact.LastName}".Trim() : null,
                CustomerId = a.CustomerId,
                CustomerName = a.Customer?.DisplayName,
                LeadId = a.LeadId,
                LeadNumber = a.Lead?.LeadNumber,
                ProjectId = a.ProjectId,
                ProjectName = a.Project?.Name,
                UnitId = a.UnitId,
                UnitName = a.Unit?.UnitNumber,
                UserId = a.UserId,
                UserName = a.User != null ? $"{a.User.FirstName} {a.User.LastName}".Trim() : null,
                Subject = a.Subject,
                IsInbound = a.IsInbound
            }).ToList();

            return new PagedResult<ActivityListDto>
            {
                Items = items,
                TotalCount = data.TotalCount,
                Page = pageRequest.Page,
                PageSize = pageRequest.PageSize
            };
        }

        public async Task<ServiceResult<ActivityDetailsDto>> GetByIdAsync(int id)
        {
            var a = await _repo.GetByIdWithDetailsAsync(id);
            if (a == null) return ServiceResult<ActivityDetailsDto>.NotFound();

            var dto = MapToDetails(a);
            return ServiceResult<ActivityDetailsDto>.Success(dto);
        }

        public async Task<ServiceResult<ActivityDetailsDto>> CreateAsync(CreateActivityDto dto)
        {
            // Validate ActivityType
            if (!await _repo.ActivityTypeExistsAsync(dto.ActivityTypeId))
                return ServiceResult<ActivityDetailsDto>.ValidationError("ActivityType is invalid");

            // Validate at least one business FK
            if (!dto.ContactId.HasValue && !dto.CustomerId.HasValue && !dto.LeadId.HasValue && !dto.ProjectId.HasValue && !dto.UnitId.HasValue)
                return ServiceResult<ActivityDetailsDto>.ValidationError("Activity must be linked to at least one of Contact, Customer, Lead, Project or Unit");

            if (dto.ContactId.HasValue && !await _repo.ContactExistsAsync(dto.ContactId.Value))
                return ServiceResult<ActivityDetailsDto>.ValidationError("Contact is invalid or deleted");

            if (dto.CustomerId.HasValue && !await _repo.CustomerExistsAsync(dto.CustomerId.Value))
                return ServiceResult<ActivityDetailsDto>.ValidationError("Customer is invalid or deleted");

            if (dto.LeadId.HasValue && !await _repo.LeadExistsAsync(dto.LeadId.Value))
                return ServiceResult<ActivityDetailsDto>.ValidationError("Lead is invalid or deleted");

            if (dto.ProjectId.HasValue && !await _repo.ProjectExistsAsync(dto.ProjectId.Value))
                return ServiceResult<ActivityDetailsDto>.ValidationError("Project is invalid or deleted");

            if (dto.UnitId.HasValue && !await _repo.UnitExistsAsync(dto.UnitId.Value))
                return ServiceResult<ActivityDetailsDto>.ValidationError("Unit is invalid or deleted");

            if (dto.UserId.HasValue && !await _repo.UserExistsAsync(dto.UserId.Value))
                return ServiceResult<ActivityDetailsDto>.ValidationError("User is invalid or deleted");

            var activity = new Activity
            {
                ActivityTypeId = dto.ActivityTypeId,
                ContactId = dto.ContactId,
                CustomerId = dto.CustomerId,
                LeadId = dto.LeadId,
                ProjectId = dto.ProjectId,
                UnitId = dto.UnitId,
                UserId = dto.UserId,
                ActivityAt = dto.ActivityAt,
                Subject = dto.Subject,
                Description = dto.Description,
                IsInbound = dto.IsInbound
            };

            await _repo.AddAsync(activity);
            await _unitOfWork.SaveChangesAsync();

            var created = await _repo.GetByIdWithDetailsAsync(activity.Id);
            if (created == null) return ServiceResult<ActivityDetailsDto>.NotFound();

            return ServiceResult<ActivityDetailsDto>.Success(MapToDetails(created));
        }

        public async Task<ServiceResult<ActivityDetailsDto>> UpdateAsync(int id, UpdateActivityDto dto)
        {
            // For updates we must work with a tracked entity so changes to scalar FKs are persisted correctly.
            var activity = await _repo.GetByIdAsync(id);
            if (activity == null) return ServiceResult<ActivityDetailsDto>.NotFound();
            if (activity.IsDeleted) return ServiceResult<ActivityDetailsDto>.NotFound();

            if (!await _repo.ActivityTypeExistsAsync(dto.ActivityTypeId))
                return ServiceResult<ActivityDetailsDto>.ValidationError("ActivityType is invalid");

            if (!dto.ContactId.HasValue && !dto.CustomerId.HasValue && !dto.LeadId.HasValue && !dto.ProjectId.HasValue && !dto.UnitId.HasValue)
                return ServiceResult<ActivityDetailsDto>.ValidationError("Activity must be linked to at least one of Contact, Customer, Lead, Project or Unit");

            if (dto.ContactId.HasValue && !await _repo.ContactExistsAsync(dto.ContactId.Value))
                return ServiceResult<ActivityDetailsDto>.ValidationError("Contact is invalid or deleted");
            if (dto.CustomerId.HasValue && !await _repo.CustomerExistsAsync(dto.CustomerId.Value))
                return ServiceResult<ActivityDetailsDto>.ValidationError("Customer is invalid or deleted");
            if (dto.LeadId.HasValue && !await _repo.LeadExistsAsync(dto.LeadId.Value))
                return ServiceResult<ActivityDetailsDto>.ValidationError("Lead is invalid or deleted");
            if (dto.ProjectId.HasValue && !await _repo.ProjectExistsAsync(dto.ProjectId.Value))
                return ServiceResult<ActivityDetailsDto>.ValidationError("Project is invalid or deleted");
            if (dto.UnitId.HasValue && !await _repo.UnitExistsAsync(dto.UnitId.Value))
                return ServiceResult<ActivityDetailsDto>.ValidationError("Unit is invalid or deleted");
            if (dto.UserId.HasValue && !await _repo.UserExistsAsync(dto.UserId.Value))
                return ServiceResult<ActivityDetailsDto>.ValidationError("User is invalid or deleted");

            activity.ActivityTypeId = dto.ActivityTypeId;
            activity.ContactId = dto.ContactId;
            activity.CustomerId = dto.CustomerId;
            activity.LeadId = dto.LeadId;
            activity.ProjectId = dto.ProjectId;
            activity.UnitId = dto.UnitId;
            activity.UserId = dto.UserId;
            activity.ActivityAt = dto.ActivityAt;
            activity.Subject = dto.Subject;
            activity.Description = dto.Description;
            activity.IsInbound = dto.IsInbound;
            activity.UpdatedAt = DateTime.UtcNow;

            _repo.Update(activity);
            await _unitOfWork.SaveChangesAsync();

            var updated = await _repo.GetByIdWithDetailsAsync(activity.Id);
            return ServiceResult<ActivityDetailsDto>.Success(MapToDetails(updated!));
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var activity = await _repo.GetByIdWithDetailsAsync(id);
            if (activity == null) return ServiceResult<bool>.NotFound();

            activity.IsDeleted = true;
            activity.UpdatedAt = DateTime.UtcNow;
            _repo.Update(activity);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }

        private ActivityDetailsDto MapToDetails(Activity a)
        {
            return new ActivityDetailsDto
            {
                Id = a.Id,
                ActivityTypeId = a.ActivityTypeId,
                ActivityTypeName = a.ActivityType?.Name,
                ContactId = a.ContactId,
                ContactName = a.Contact != null ? $"{a.Contact.FirstName} {a.Contact.LastName}".Trim() : null,
                CustomerId = a.CustomerId,
                CustomerName = a.Customer?.DisplayName,
                LeadId = a.LeadId,
                LeadNumber = a.Lead?.LeadNumber,
                ProjectId = a.ProjectId,
                ProjectName = a.Project?.Name,
                UnitId = a.UnitId,
                UnitName = a.Unit?.UnitNumber,
                UserId = a.UserId,
                UserName = a.User != null ? $"{a.User.FirstName} {a.User.LastName}".Trim() : null,
                ActivityAt = a.ActivityAt,
                Subject = a.Subject,
                Description = a.Description,
                IsInbound = a.IsInbound
            };
        }
    }
}
