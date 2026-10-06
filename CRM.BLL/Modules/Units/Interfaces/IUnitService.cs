using System.Threading.Tasks;
using CRM.BLL.Common.Pagination;
using CRM.BLL.Modules.Units.DTOs;

namespace CRM.BLL.Modules.Units.Interfaces
{
    public interface IUnitService
    {
        Task<PagedResult<UnitListDto>> GetPagedAsync(PageRequest pageRequest, object filter);

        Task<CRM.BLL.Common.ServiceResult<UnitDetailsDto>> GetByIdAsync(int id);

        Task<System.Collections.Generic.List<UnitLookupDto>> GetLookupAsync(int? projectId = null, int? unitStatusId = null);
    }
}
