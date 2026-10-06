using System.Threading.Tasks;
using CRM.DAL.Common.Models;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Modules.Tasks.Entities;

namespace CRM.DAL.Modules.Tasks.Repositories
{
    public interface ITaskRepository
    {
        Task<PagedData<TaskItem>> GetPagedAsync(int page, int pageSize, int? taskStateId, int? priorityId, int? assignedToUserId, int? createdForLeadId, int? customerId, int? projectId);
        Task<TaskItem?> GetByIdWithDetailsAsync(int id);
        Task<TaskItem?> GetByIdAsync(int id);
        Task AddAsync(TaskItem item);
        void Update(TaskItem item);

        // Existence/validation helpers
        Task<bool> TaskStateExistsAsync(int id);
        Task<bool> PriorityExistsAsync(int id);
        Task<bool> UserExistsAsync(int id);
        Task<bool> LeadExistsAsync(int id);
        Task<bool> CustomerExistsAsync(int id);
        Task<bool> ProjectExistsAsync(int id);

        Task<TaskState?> GetTaskStateByIdAsync(int id);
    }
}
