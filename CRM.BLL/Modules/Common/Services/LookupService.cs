using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.BLL.Common.DTOs;
using CRM.BLL.Modules.Common.Interfaces;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Common.Models;

namespace CRM.BLL.Modules.Common.Services
{
    public class LookupService : ILookupService
    {
        private readonly ILookupRepository _repo;

        public LookupService(ILookupRepository repo)
        {
            _repo = repo;
        }

        private static LookupItemDto Map(LookupData d)
            => new LookupItemDto { Id = d.Id, Code = d.Code, Name = d.Name };

        public async Task<IReadOnlyList<LookupItemDto>> GetProjectTypesAsync()
            => (await _repo.GetProjectTypesAsync()).Select(Map).ToList();

        public async Task<IReadOnlyList<LookupItemDto>> GetProjectStatusesAsync()
            => (await _repo.GetProjectStatusesAsync()).Select(Map).ToList();

        public async Task<IReadOnlyList<LookupItemDto>> GetBuildingStatusesAsync()
            => (await _repo.GetBuildingStatusesAsync()).Select(Map).ToList();

        public async Task<IReadOnlyList<LookupItemDto>> GetUnitTypesAsync()
            => (await _repo.GetUnitTypesAsync()).Select(Map).ToList();

        public async Task<IReadOnlyList<LookupItemDto>> GetUnitStatusesAsync()
            => (await _repo.GetUnitStatusesAsync()).Select(Map).ToList();

        public async Task<IReadOnlyList<LookupItemDto>> GetAssetTypesAsync()
            => (await _repo.GetAssetTypesAsync()).Select(Map).ToList();

        public async Task<IReadOnlyList<LookupItemDto>> GetAssetStatusesAsync()
            => (await _repo.GetAssetStatusesAsync()).Select(Map).ToList();

        public async Task<IReadOnlyList<LookupItemDto>> GetCustomerTypesAsync()
            => (await _repo.GetCustomerTypesAsync()).Select(Map).ToList();

        public async Task<IReadOnlyList<LookupItemDto>> GetCustomerStatusesAsync()
            => (await _repo.GetCustomerStatusesAsync()).Select(Map).ToList();

        public async Task<IReadOnlyList<LookupItemDto>> GetLeadSourcesAsync()
            => (await _repo.GetLeadSourcesAsync()).Select(Map).ToList();

        public async Task<IReadOnlyList<LookupItemDto>> GetLeadStatusesAsync()
            => (await _repo.GetLeadStatusesAsync()).Select(Map).ToList();

        public async Task<IReadOnlyList<LookupItemDto>> GetLostReasonsAsync()
            => (await _repo.GetLostReasonsAsync()).Select(Map).ToList();

        public async Task<IReadOnlyList<LookupItemDto>> GetActivityTypesAsync()
            => (await _repo.GetActivityTypesAsync()).Select(Map).ToList();

        public async Task<IReadOnlyList<LookupItemDto>> GetTaskStatesAsync()
            => (await _repo.GetTaskStatesAsync()).Select(Map).ToList();

        public async Task<IReadOnlyList<LookupItemDto>> GetPrioritiesAsync()
            => (await _repo.GetPrioritiesAsync()).Select(Map).ToList();

        public async Task<IReadOnlyList<LookupItemDto>> GetDealStagesAsync()
            => (await _repo.GetDealStagesAsync()).Select(Map).ToList();
    }
}
