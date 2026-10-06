using System.Threading.Tasks;
using CRM.DAL.Common.Models;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Modules.Leads.Entities;

namespace CRM.DAL.Modules.Leads.Repositories
{
    public interface ILeadRepository : IRepository<Lead>
    {
        Task<PagedData<Lead>> GetPagedAsync(int page, int pageSize, int? leadStatusId, int? leadSourceId, int? projectId, int? assignedUserId, int? contactId, bool? isActive, string? search, System.Threading.CancellationToken cancellationToken = default);

        Task<Lead?> GetByIdWithDetailsAsync(int id);

        Task<Lead?> GetByIdForUpdateAsync(int id);

        Task<bool> LeadNumberExistsAsync(string leadNumber, int? excludeLeadId = null);

        Task<bool> ContactExistsAsync(int contactId);

        Task<bool> ProjectExistsAsync(int projectId);

        Task<bool> UserExistsAsync(int userId);

        Task<bool> LeadSourceExistsAsync(int leadSourceId);

        Task<LeadStatus?> GetLeadStatusByIdAsync(int leadStatusId);

        Task<bool> LostReasonExistsAsync(int lostReasonId);

        Task<bool> UnitExistsAsync(int unitId);
    }
}
