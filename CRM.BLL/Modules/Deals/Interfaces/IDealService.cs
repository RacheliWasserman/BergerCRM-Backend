using System.Threading.Tasks;
using CRM.BLL.Common.Pagination;
using CRM.BLL.Modules.Deals.DTOs;
using CRM.BLL.Common;

namespace CRM.BLL.Modules.Deals.Interfaces
{
    public interface IDealService
    {
        Task<PagedResult<DealListDto>> GetPagedAsync(PageRequest pageRequest, DealFilter filter);

        Task<ServiceResult<DealDetailsDto>> GetByIdAsync(int id);

        Task<ServiceResult<DealDetailsDto>> CreateAsync(CreateDealDto dto);

        Task<ServiceResult<DealDetailsDto>> UpdateAsync(int id, UpdateDealDto dto);

        Task<ServiceResult<bool>> DeleteAsync(int id, int? userId = null);

        Task<ServiceResult<DealDetailsDto>> ChangeStageAsync(int id, ChangeDealStageRequest request);

        Task<ServiceResult<System.Collections.Generic.List<DealStageHistoryDto>>> GetStageHistoryAsync(int id);
    }
}
