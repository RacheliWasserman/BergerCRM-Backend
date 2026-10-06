using Microsoft.AspNetCore.Mvc;
using CRM.BLL.Modules.Deals.Interfaces;
using CRM.BLL.Modules.Deals.DTOs;
using CRM.BLL.Common.Pagination;

namespace CRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DealsController : ControllerBase
    {
        private readonly IDealService _dealService;

        public DealsController(IDealService dealService)
        {
            _dealService = dealService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<DealListDto>>> Get([FromQuery] PageRequest pageRequest, [FromQuery] DealFilter filter)
        {
            var result = await _dealService.GetPagedAsync(pageRequest, filter);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DealDetailsDto>> GetById(int id)
        {
            var result = await _dealService.GetByIdAsync(id);
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDealDto dto)
        {
            var result = await _dealService.CreateAsync(dto);
            switch (result.Status)
            {
                case CRM.BLL.Common.ServiceResultStatus.Success:
                    return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
                case CRM.BLL.Common.ServiceResultStatus.Conflict:
                    return Conflict(new { error = result.ErrorMessage });
                case CRM.BLL.Common.ServiceResultStatus.ValidationError:
                    return BadRequest(new { error = result.ErrorMessage });
                default:
                    return BadRequest(new { error = result.ErrorMessage });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDealDto dto)
        {
            var result = await _dealService.UpdateAsync(id, dto);
            switch (result.Status)
            {
                case CRM.BLL.Common.ServiceResultStatus.Success:
                    return Ok(result.Data);
                case CRM.BLL.Common.ServiceResultStatus.NotFound:
                    return NotFound();
                case CRM.BLL.Common.ServiceResultStatus.Conflict:
                    return Conflict(new { error = result.ErrorMessage });
                case CRM.BLL.Common.ServiceResultStatus.ValidationError:
                    return BadRequest(new { error = result.ErrorMessage });
                default:
                    return BadRequest(new { error = result.ErrorMessage });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _dealService.DeleteAsync(id);
            switch (result.Status)
            {
                case CRM.BLL.Common.ServiceResultStatus.Success:
                    return NoContent();
                case CRM.BLL.Common.ServiceResultStatus.NotFound:
                    return NotFound();
                default:
                    return BadRequest(new { error = result.ErrorMessage });
            }
        }

        [HttpGet("{id:int}/stage-history")]
        public async Task<IActionResult> GetStageHistory(int id)
        {
            var result = await _dealService.GetStageHistoryAsync(id);
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

        [HttpPut("{id:int}/stage")]
        public async Task<IActionResult> ChangeStage(int id, [FromBody] ChangeDealStageRequest request)
        {
            var result = await _dealService.ChangeStageAsync(id, request);
            switch (result.Status)
            {
                case CRM.BLL.Common.ServiceResultStatus.Success:
                    return Ok(result.Data);
                case CRM.BLL.Common.ServiceResultStatus.NotFound:
                    return NotFound();
                case CRM.BLL.Common.ServiceResultStatus.ValidationError:
                    return BadRequest(new { error = result.ErrorMessage });
                default:
                    return BadRequest(new { error = result.ErrorMessage });
            }
        }
    }
}
