using System.Threading.Tasks;
using CRM.BLL.Common.Pagination;
using CRM.BLL.Common;
using CRM.BLL.Modules.Activities.DTOs;
using System.Collections.Generic;

namespace CRM.BLL.Modules.Activities.Interfaces
{
    public interface IActivityService
    {
        Task<PagedResult<ActivityListDto>> GetPagedAsync(PageRequest pageRequest, ActivityFilter filter);
        Task<ServiceResult<ActivityDetailsDto>> GetByIdAsync(int id);
        Task<ServiceResult<ActivityDetailsDto>> CreateAsync(CreateActivityDto dto);
        Task<ServiceResult<ActivityDetailsDto>> UpdateAsync(int id, UpdateActivityDto dto);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}
