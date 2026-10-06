using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CRM.BLL.Modules.Common.Interfaces;
using CRM.BLL.Common.DTOs;

namespace CRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LookupsController : ControllerBase
    {
        private readonly ILookupService _lookupService;

        public LookupsController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpGet("project-types")]
        public async Task<IActionResult> GetProjectTypes()
            => Ok(await _lookupService.GetProjectTypesAsync());

        [HttpGet("project-statuses")]
        public async Task<IActionResult> GetProjectStatuses()
            => Ok(await _lookupService.GetProjectStatusesAsync());

        [HttpGet("building-statuses")]
        public async Task<IActionResult> GetBuildingStatuses()
            => Ok(await _lookupService.GetBuildingStatusesAsync());

        [HttpGet("unit-types")]
        public async Task<IActionResult> GetUnitTypes()
            => Ok(await _lookupService.GetUnitTypesAsync());

        [HttpGet("unit-statuses")]
        public async Task<IActionResult> GetUnitStatuses()
            => Ok(await _lookupService.GetUnitStatusesAsync());

        [HttpGet("asset-types")]
        public async Task<IActionResult> GetAssetTypes()
            => Ok(await _lookupService.GetAssetTypesAsync());

        [HttpGet("asset-statuses")]
        public async Task<IActionResult> GetAssetStatuses()
            => Ok(await _lookupService.GetAssetStatusesAsync());

        [HttpGet("customer-types")]
        public async Task<IActionResult> GetCustomerTypes()
            => Ok(await _lookupService.GetCustomerTypesAsync());

        [HttpGet("customer-statuses")]
        public async Task<IActionResult> GetCustomerStatuses()
            => Ok(await _lookupService.GetCustomerStatusesAsync());

        [HttpGet("lead-sources")]
        public async Task<IActionResult> GetLeadSources()
            => Ok(await _lookupService.GetLeadSourcesAsync());

        [HttpGet("lead-statuses")]
        public async Task<IActionResult> GetLeadStatuses()
            => Ok(await _lookupService.GetLeadStatusesAsync());

        [HttpGet("lost-reasons")]
        public async Task<IActionResult> GetLostReasons()
            => Ok(await _lookupService.GetLostReasonsAsync());

        [HttpGet("activity-types")]
        public async Task<IActionResult> GetActivityTypes()
            => Ok(await _lookupService.GetActivityTypesAsync());

        [HttpGet("task-states")]
        public async Task<IActionResult> GetTaskStates()
            => Ok(await _lookupService.GetTaskStatesAsync());

        [HttpGet("priorities")]
        public async Task<IActionResult> GetPriorities()
            => Ok(await _lookupService.GetPrioritiesAsync());

        [HttpGet("deal-stages")]
        public async Task<IActionResult> GetDealStages()
            => Ok(await _lookupService.GetDealStagesAsync());
    }
}
