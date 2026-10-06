using System.Threading.Tasks;
using CRM.DAL.Common.Models;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Modules.Projects.Entities;
using System.Collections.Generic;

namespace CRM.DAL.Modules.Projects.Repositories
{
    public interface IUnitRepository : IRepository<Unit>
    {
        Task<PagedData<Unit>> GetPagedAsync(int page, int pageSize, int? projectId, int? buildingId, int? floorId, int? unitStatusId, int? unitTypeId, string? search, System.Threading.CancellationToken cancellationToken = default);

        Task<Unit?> GetByIdWithDetailsAsync(int id);

        Task<List<Unit>> GetLookupAsync(int? projectId = null, int? unitStatusId = null);
    }
}
