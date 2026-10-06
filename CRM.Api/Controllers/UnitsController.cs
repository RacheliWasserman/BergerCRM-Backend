using Microsoft.AspNetCore.Mvc;
using CRM.BLL.Modules.Units.Interfaces;
using CRM.BLL.Modules.Units.DTOs;
using CRM.BLL.Common.Pagination;

namespace CRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitsController : ControllerBase
    {
        private readonly IUnitService _unitService;

        public UnitsController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<UnitListDto>>> Get([FromQuery] PageRequest pageRequest, [FromQuery] int? projectId, [FromQuery] int? buildingId, [FromQuery] int? floorId, [FromQuery] int? unitStatusId, [FromQuery] int? unitTypeId, [FromQuery] string? search)
        {
            // pass filters as a JSON element via object to keep service signature stable
            var filter = new { projectId, buildingId, floorId, unitStatusId, unitTypeId, search };
            var result = await _unitService.GetPagedAsync(pageRequest, System.Text.Json.JsonSerializer.SerializeToElement(filter));
            return Ok(result);
        }

        [HttpGet("lookup")]
        public async Task<ActionResult<System.Collections.Generic.List<UnitLookupDto>>> Lookup([FromQuery] int? projectId, [FromQuery] int? unitStatusId)
        {
            var list = await _unitService.GetLookupAsync(projectId, unitStatusId);
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _unitService.GetByIdAsync(id);
            switch (result.Status)
            {
                case CRM.BLL.Common.ServiceResultStatus.Success:
                    return Ok(result.Data);
                case CRM.BLL.Common.ServiceResultStatus.NotFound:
                    return NotFound();
                default:
                    return BadRequest(new { error = result.ErrorMessage });
            }
        }
    }
}
