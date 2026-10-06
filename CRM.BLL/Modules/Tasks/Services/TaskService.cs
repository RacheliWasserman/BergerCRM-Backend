using System;
using System.Linq;
using System.Threading.Tasks;
using CRM.BLL.Common;
using CRM.BLL.Common.Pagination;
using CRM.BLL.Modules.Tasks.DTOs;
using CRM.BLL.Modules.Tasks.Interfaces;
using CRM.DAL.Modules.Tasks.Repositories;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Modules.Tasks.Entities;

namespace CRM.BLL.Modules.Tasks.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(ITaskRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<TaskListDto>> GetPagedAsync(PageRequest pageRequest, TaskFilter filter)
        {
            var data = await _repo.GetPagedAsync(pageRequest.Page, pageRequest.PageSize, filter.TaskStateId, filter.PriorityId, filter.AssignedToUserId, filter.CreatedForLeadId, filter.CustomerId, filter.ProjectId);

            var items = data.Items.Select(t => new TaskListDto
            {
                Id = t.Id,
                Title = t.Title,
                TaskStateId = t.TaskStateId,
                TaskStateCode = t.TaskState?.Code,
                TaskStateName = t.TaskState?.Name,
                PriorityId = t.PriorityId,
                PriorityCode = t.Priority?.Code,
                PriorityName = t.Priority?.Name,
                AssignedToUserId = t.AssignedToUserId,
                AssignedToUserName = t.AssignedToUser != null ? $"{t.AssignedToUser.FirstName} {t.AssignedToUser.LastName}".Trim() : null,
                CreatedForLeadId = t.CreatedForLeadId,
                LeadNumber = t.CreatedForLead?.LeadNumber,
                CustomerId = t.CustomerId,
                CustomerName = t.Customer?.DisplayName,
                ProjectId = t.ProjectId,
                ProjectName = t.Project?.Name,
                DueAt = t.DueAt,
                CompletedAt = t.CompletedAt,
                IsOverdue = CalculateIsOverdue(t),
                IsActive = t.IsActive
            }).ToList();

            return new PagedResult<TaskListDto>
            {
                Items = items,
                TotalCount = data.TotalCount,
                Page = pageRequest.Page,
                PageSize = pageRequest.PageSize
            };
        }

        public async Task<ServiceResult<TaskDetailsDto>> GetByIdAsync(int id)
        {
            var t = await _repo.GetByIdWithDetailsAsync(id);
            if (t == null) return ServiceResult<TaskDetailsDto>.NotFound();

            return ServiceResult<TaskDetailsDto>.Success(MapToDetails(t));
        }

        public async Task<ServiceResult<TaskDetailsDto>> CreateAsync(CreateTaskDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return ServiceResult<TaskDetailsDto>.ValidationError("Title is required");

            if (!await _repo.TaskStateExistsAsync(dto.TaskStateId))
                return ServiceResult<TaskDetailsDto>.ValidationError("TaskState is invalid");

            if (!await _repo.PriorityExistsAsync(dto.PriorityId))
                return ServiceResult<TaskDetailsDto>.ValidationError("Priority is invalid");

            if (dto.AssignedToUserId.HasValue && !await _repo.UserExistsAsync(dto.AssignedToUserId.Value))
                return ServiceResult<TaskDetailsDto>.ValidationError("AssignedToUser is invalid or deleted");

            if (dto.CreatedForLeadId.HasValue && !await _repo.LeadExistsAsync(dto.CreatedForLeadId.Value))
                return ServiceResult<TaskDetailsDto>.ValidationError("Lead is invalid or deleted");

            if (dto.CustomerId.HasValue && !await _repo.CustomerExistsAsync(dto.CustomerId.Value))
                return ServiceResult<TaskDetailsDto>.ValidationError("Customer is invalid or deleted");

            if (dto.ProjectId.HasValue && !await _repo.ProjectExistsAsync(dto.ProjectId.Value))
                return ServiceResult<TaskDetailsDto>.ValidationError("Project is invalid or deleted");

            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                TaskStateId = dto.TaskStateId,
                PriorityId = dto.PriorityId,
                AssignedToUserId = dto.AssignedToUserId,
                CreatedForLeadId = dto.CreatedForLeadId,
                CustomerId = dto.CustomerId,
                ProjectId = dto.ProjectId,
                DueAt = dto.DueAt,
                CompletedAt = dto.CompletedAt,
                IsActive = dto.IsActive
            };

            // CompletedAt semantics
            var state = await _repo.GetTaskStateByIdAsync(task.TaskStateId);
            if (state != null && state.Code == "COMPLETED")
            {
                if (!task.CompletedAt.HasValue) task.CompletedAt = DateTime.UtcNow;
            }

            await _repo.AddAsync(task);
            await _unitOfWork.SaveChangesAsync();

            var created = await _repo.GetByIdWithDetailsAsync(task.Id);
            return ServiceResult<TaskDetailsDto>.Success(MapToDetails(created!));
        }

        public async Task<ServiceResult<TaskDetailsDto>> UpdateAsync(int id, UpdateTaskDto dto)
        {
            // Load tracked entity for mutation to ensure FK scalar changes persist
            var task = await _repo.GetByIdAsync(id);
            if (task == null) return ServiceResult<TaskDetailsDto>.NotFound();
            if (task.IsDeleted) return ServiceResult<TaskDetailsDto>.NotFound();

            if (string.IsNullOrWhiteSpace(dto.Title))
                return ServiceResult<TaskDetailsDto>.ValidationError("Title is required");

            if (!await _repo.TaskStateExistsAsync(dto.TaskStateId))
                return ServiceResult<TaskDetailsDto>.ValidationError("TaskState is invalid");

            if (!await _repo.PriorityExistsAsync(dto.PriorityId))
                return ServiceResult<TaskDetailsDto>.ValidationError("Priority is invalid");

            if (dto.AssignedToUserId.HasValue && !await _repo.UserExistsAsync(dto.AssignedToUserId.Value))
                return ServiceResult<TaskDetailsDto>.ValidationError("AssignedToUser is invalid or deleted");

            if (dto.CreatedForLeadId.HasValue && !await _repo.LeadExistsAsync(dto.CreatedForLeadId.Value))
                return ServiceResult<TaskDetailsDto>.ValidationError("Lead is invalid or deleted");

            if (dto.CustomerId.HasValue && !await _repo.CustomerExistsAsync(dto.CustomerId.Value))
                return ServiceResult<TaskDetailsDto>.ValidationError("Customer is invalid or deleted");

            if (dto.ProjectId.HasValue && !await _repo.ProjectExistsAsync(dto.ProjectId.Value))
                return ServiceResult<TaskDetailsDto>.ValidationError("Project is invalid or deleted");

            // Previous state code requires details — load lightweight navigation if available after save
            var previousStateCode = (await _repo.GetByIdWithDetailsAsync(id))?.TaskState?.Code;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.TaskStateId = dto.TaskStateId;
            task.PriorityId = dto.PriorityId;
            task.AssignedToUserId = dto.AssignedToUserId;
            task.CreatedForLeadId = dto.CreatedForLeadId;
            task.CustomerId = dto.CustomerId;
            task.ProjectId = dto.ProjectId;
            task.DueAt = dto.DueAt;
            task.IsActive = dto.IsActive;

            var newState = await _repo.GetTaskStateByIdAsync(dto.TaskStateId);
            var newCode = newState?.Code;

            // CompletedAt semantics
            if (newCode == "COMPLETED")
            {
                if (!task.CompletedAt.HasValue) task.CompletedAt = DateTime.UtcNow;
            }
            else
            {
                // If moved from COMPLETED to something else, clear CompletedAt
                if (previousStateCode == "COMPLETED") task.CompletedAt = null;
                // If user supplied CompletedAt in DTO when not completed, preserve as provided? Business rule says only set when completed
                // We'll not set CompletedAt here except as above.
            }

            _repo.Update(task);
            await _unitOfWork.SaveChangesAsync();

            var updated = await _repo.GetByIdWithDetailsAsync(task.Id);
            return ServiceResult<TaskDetailsDto>.Success(MapToDetails(updated!));
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var task = await _repo.GetByIdWithDetailsAsync(id);
            if (task == null) return ServiceResult<bool>.NotFound();

            task.IsDeleted = true;
            task.UpdatedAt = DateTime.UtcNow;
            _repo.Update(task);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }

        private bool CalculateIsOverdue(TaskItem t)
        {
            if (!t.DueAt.HasValue) return false;
            var state = t.TaskState?.Code;
            if (state == "COMPLETED" || state == "CANCELLED") return false;
            return t.DueAt.Value < DateTime.UtcNow;
        }

        private TaskDetailsDto MapToDetails(TaskItem t)
        {
            return new TaskDetailsDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                TaskStateId = t.TaskStateId,
                TaskStateCode = t.TaskState?.Code,
                TaskStateName = t.TaskState?.Name,
                PriorityId = t.PriorityId,
                PriorityCode = t.Priority?.Code,
                PriorityName = t.Priority?.Name,
                AssignedToUserId = t.AssignedToUserId,
                AssignedToUserName = t.AssignedToUser != null ? $"{t.AssignedToUser.FirstName} {t.AssignedToUser.LastName}".Trim() : null,
                CreatedForLeadId = t.CreatedForLeadId,
                LeadNumber = t.CreatedForLead?.LeadNumber,
                CustomerId = t.CustomerId,
                CustomerName = t.Customer?.DisplayName,
                ProjectId = t.ProjectId,
                ProjectName = t.Project?.Name,
                DueAt = t.DueAt,
                CompletedAt = t.CompletedAt,
                IsOverdue = CalculateIsOverdue(t),
                IsActive = t.IsActive
            };
        }
    }
}
