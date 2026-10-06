using System.Threading.Tasks;
using CRM.DAL.Common.Models;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Modules.Deals.Entities;

namespace CRM.DAL.Modules.Deals.Repositories
{
    public interface IDealRepository : IRepository<Deal>
    {
        Task<PagedData<Deal>> GetPagedAsync(int page, int pageSize, int? projectId, int? ownerUserId, int? contactId, int? dealStageId, bool? isActive, string? search, System.Threading.CancellationToken cancellationToken = default);

        Task<Deal?> GetByIdWithDetailsAsync(int id);

        Task<Deal?> GetByIdForUpdateAsync(int id);

        Task<bool> DealNumberExistsAsync(string dealNumber, int? excludeDealId = null);

        Task<bool> ContactExistsAsync(int contactId);

        Task<bool> ProjectExistsAsync(int projectId);

        Task<bool> UnitExistsAsync(int unitId);

        Task<bool> CustomerExistsAsync(int customerId);

        Task<bool> UserExistsAsync(int userId);

        Task<DealStage?> GetDealStageByIdAsync(int dealStageId);

        Task AddStageHistoryAsync(DealStageHistory history);

        Task<System.Collections.Generic.List<DealStageHistory>> GetStageHistoryAsync(int dealId);
    }
}
