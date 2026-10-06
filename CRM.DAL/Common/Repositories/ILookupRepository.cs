using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Common.Models;

namespace CRM.DAL.Common.Repositories
{
    public interface ILookupRepository
    {
        Task<IReadOnlyList<LookupData>> GetProjectTypesAsync();
        Task<IReadOnlyList<LookupData>> GetProjectStatusesAsync();
        Task<IReadOnlyList<LookupData>> GetBuildingStatusesAsync();
        Task<IReadOnlyList<LookupData>> GetUnitTypesAsync();
        Task<IReadOnlyList<LookupData>> GetUnitStatusesAsync();
        Task<IReadOnlyList<LookupData>> GetAssetTypesAsync();
        Task<IReadOnlyList<LookupData>> GetAssetStatusesAsync();

        Task<IReadOnlyList<LookupData>> GetCustomerTypesAsync();
        Task<IReadOnlyList<LookupData>> GetCustomerStatusesAsync();

        Task<IReadOnlyList<LookupData>> GetLeadSourcesAsync();
        Task<IReadOnlyList<LookupData>> GetLeadStatusesAsync();
        Task<IReadOnlyList<LookupData>> GetLostReasonsAsync();

        Task<IReadOnlyList<LookupData>> GetActivityTypesAsync();

        Task<IReadOnlyList<LookupData>> GetTaskStatesAsync();
        Task<IReadOnlyList<LookupData>> GetPrioritiesAsync();

        Task<IReadOnlyList<LookupData>> GetDealStagesAsync();
    }
}
