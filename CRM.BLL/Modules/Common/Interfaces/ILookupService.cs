using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.BLL.Common.DTOs;

namespace CRM.BLL.Modules.Common.Interfaces
{
    public interface ILookupService
    {
        Task<IReadOnlyList<LookupItemDto>> GetProjectTypesAsync();
        Task<IReadOnlyList<LookupItemDto>> GetProjectStatusesAsync();
        Task<IReadOnlyList<LookupItemDto>> GetBuildingStatusesAsync();
        Task<IReadOnlyList<LookupItemDto>> GetUnitTypesAsync();
        Task<IReadOnlyList<LookupItemDto>> GetUnitStatusesAsync();
        Task<IReadOnlyList<LookupItemDto>> GetAssetTypesAsync();
        Task<IReadOnlyList<LookupItemDto>> GetAssetStatusesAsync();

        Task<IReadOnlyList<LookupItemDto>> GetCustomerTypesAsync();
        Task<IReadOnlyList<LookupItemDto>> GetCustomerStatusesAsync();

        Task<IReadOnlyList<LookupItemDto>> GetLeadSourcesAsync();
        Task<IReadOnlyList<LookupItemDto>> GetLeadStatusesAsync();
        Task<IReadOnlyList<LookupItemDto>> GetLostReasonsAsync();

        Task<IReadOnlyList<LookupItemDto>> GetActivityTypesAsync();

        Task<IReadOnlyList<LookupItemDto>> GetTaskStatesAsync();
        Task<IReadOnlyList<LookupItemDto>> GetPrioritiesAsync();

        Task<IReadOnlyList<LookupItemDto>> GetDealStagesAsync();
    }
}
