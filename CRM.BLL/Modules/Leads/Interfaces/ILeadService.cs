using System.Threading.Tasks;
using CRM.BLL.Common;
using CRM.BLL.Modules.Leads.DTOs;
using CRM.BLL.Common.Pagination;

namespace CRM.BLL.Modules.Leads.Interfaces
{
    public interface ILeadService
    {
        Task<PagedResult<LeadListDto>> GetPagedAsync(PageRequest pageRequest, LeadFilter filter);

        Task<ServiceResult<LeadDetailsDto>> GetByIdAsync(int id);

        Task<ServiceResult<LeadDetailsDto>> CreateAsync(CreateLeadDto dto);

        Task<ServiceResult<LeadDetailsDto>> UpdateAsync(int id, UpdateLeadDto dto);

        Task<ServiceResult<bool>> DeleteAsync(int id);

        Task<ServiceResult<bool>> AssignAsync(int id, AssignLeadRequest request);

        Task<ServiceResult<bool>> UnassignAsync(int id, AssignLeadRequest request);

        Task<ServiceResult<LeadPreferenceDto>> GetPreferenceAsync(int id);

        Task<ServiceResult<LeadPreferenceDto>> UpsertPreferenceAsync(int id, UpsertLeadPreferenceRequest request);

        Task<ServiceResult<bool>> RemovePreferenceAsync(int id);

        Task<ServiceResult<LeadInterestedUnitDto>> AddInterestedUnitAsync(int id, AddInterestedUnitRequest request);

        Task<ServiceResult<bool>> RemoveInterestedUnitAsync(int id, int unitId);

        Task<ServiceResult<bool>> ChangeStatusAsync(int id, ChangeLeadStatusRequest request);

        Task<ServiceResult<System.Collections.Generic.List<LeadStatusHistoryDto>>> GetStatusHistoryAsync(int id);
    }
}
