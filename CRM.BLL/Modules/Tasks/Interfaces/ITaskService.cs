using System.Threading.Tasks;
using CRM.BLL.Common.Pagination;
using CRM.BLL.Modules.Tasks.DTOs;
using CRM.BLL.Common;

namespace CRM.BLL.Modules.Tasks.Interfaces
{
    public interface ITaskService
    {
        Task<PagedResult<TaskListDto>> GetPagedAsync(PageRequest pageRequest, TaskFilter filter);
        Task<ServiceResult<TaskDetailsDto>> GetByIdAsync(int id);
        Task<ServiceResult<TaskDetailsDto>> CreateAsync(CreateTaskDto dto);
        Task<ServiceResult<TaskDetailsDto>> UpdateAsync(int id, UpdateTaskDto dto);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}
