using System.Threading.Tasks;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Modules.Activities.Entities;
using CRM.DAL.Common.Models;
using System.Collections.Generic;

namespace CRM.DAL.Modules.Activities.Repositories
{
    public interface IActivityRepository : IRepository<Activity>
    {
        Task<PagedData<Activity>> GetPagedAsync(int page, int pageSize, int? activityTypeId, int? contactId, int? customerId, int? leadId, int? projectId, int? unitId, int? userId, System.Threading.CancellationToken cancellationToken = default);

        Task<Activity?> GetByIdWithDetailsAsync(int id);

        Task<bool> ActivityTypeExistsAsync(int activityTypeId);
        Task<bool> ContactExistsAsync(int contactId);
        Task<bool> CustomerExistsAsync(int customerId);
        Task<bool> LeadExistsAsync(int leadId);
        Task<bool> ProjectExistsAsync(int projectId);
        Task<bool> UnitExistsAsync(int unitId);
        Task<bool> UserExistsAsync(int userId);
    }
}
