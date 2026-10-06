using Microsoft.AspNetCore.Mvc;
using CRM.BLL.Modules.Leads.Interfaces;
using CRM.BLL.Modules.Leads.DTOs;
using CRM.BLL.Common.Pagination;

namespace CRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeadsController : ControllerBase
    {
        private readonly ILeadService _leadService;

        public LeadsController(ILeadService leadService)
        {
            _leadService = leadService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<LeadListDto>>> Get([FromQuery] PageRequest pageRequest, [FromQuery] LeadFilter filter)
        {
            var result = await _leadService.GetPagedAsync(pageRequest, filter);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LeadDetailsDto>> GetById(int id)
        {
            var result = await _leadService.GetByIdAsync(id);
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
        public async Task<IActionResult> Create([FromBody] CreateLeadDto dto)
        {
            var result = await _leadService.CreateAsync(dto);
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLeadDto dto)
        {
            var result = await _leadService.UpdateAsync(id, dto);
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
            var result = await _leadService.DeleteAsync(id);
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

        [HttpGet("{id:int}/preference")]
        public async Task<IActionResult> GetPreference(int id)
        {
            var result = await _leadService.GetPreferenceAsync(id);
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

        [HttpPut("{id:int}/preference")]
        public async Task<IActionResult> UpsertPreference(int id, [FromBody] UpsertLeadPreferenceRequest request)
        {
            var result = await _leadService.UpsertPreferenceAsync(id, request);
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

        [HttpDelete("{id:int}/preference")]
        public async Task<IActionResult> DeletePreference(int id)
        {
            var result = await _leadService.RemovePreferenceAsync(id);
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

        [HttpGet("{id:int}/interested-units")]
        public async Task<IActionResult> GetInterestedUnits(int id)
        {
            var lead = await _leadService.GetByIdAsync(id);
            if (lead.Status == CRM.BLL.Common.ServiceResultStatus.NotFound) return NotFound();
            return Ok(lead.Data!.InterestedUnits);
        }

        [HttpPost("{id:int}/interested-units")]
        public async Task<IActionResult> AddInterestedUnit(int id, [FromBody] AddInterestedUnitRequest request)
        {
            var result = await _leadService.AddInterestedUnitAsync(id, request);
            switch (result.Status)
            {
                case CRM.BLL.Common.ServiceResultStatus.Success:
                    return Ok(result.Data);
                case CRM.BLL.Common.ServiceResultStatus.Conflict:
                    return Conflict(new { error = result.ErrorMessage });
                case CRM.BLL.Common.ServiceResultStatus.NotFound:
                    return NotFound();
                case CRM.BLL.Common.ServiceResultStatus.ValidationError:
                    return BadRequest(new { error = result.ErrorMessage });
                default:
                    return BadRequest(new { error = result.ErrorMessage });
            }
        }

        [HttpDelete("{id:int}/interested-units/{unitId:int}")]
        public async Task<IActionResult> RemoveInterestedUnit(int id, int unitId)
        {
            var result = await _leadService.RemoveInterestedUnitAsync(id, unitId);
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

        [HttpGet("{id:int}/assignments")]
        public async Task<IActionResult> GetAssignments(int id)
        {
            var result = await _leadService.GetByIdAsync(id);
            if (result.Status == CRM.BLL.Common.ServiceResultStatus.NotFound) return NotFound();
            return Ok(result.Data!.Assignments);
        }

        [HttpGet("{id:int}/status-history")]
        public async Task<IActionResult> GetStatusHistory(int id)
        {
            var result = await _leadService.GetStatusHistoryAsync(id);
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

        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] ChangeLeadStatusRequest request)
        {
            var result = await _leadService.ChangeStatusAsync(id, request);
            switch (result.Status)
            {
                case CRM.BLL.Common.ServiceResultStatus.Success:
                    return Ok();
                case CRM.BLL.Common.ServiceResultStatus.NotFound:
                    return NotFound();
                case CRM.BLL.Common.ServiceResultStatus.ValidationError:
                    return BadRequest(new { error = result.ErrorMessage });
                default:
                    return BadRequest(new { error = result.ErrorMessage });
            }
        }

        [HttpPost("{id:int}/assign")]
        public async Task<IActionResult> Assign(int id, [FromBody] AssignLeadRequest request)
        {
            var result = await _leadService.AssignAsync(id, request);
            switch (result.Status)
            {
                case CRM.BLL.Common.ServiceResultStatus.Success:
                    return Ok();
                case CRM.BLL.Common.ServiceResultStatus.NotFound:
                    return NotFound();
                case CRM.BLL.Common.ServiceResultStatus.ValidationError:
                    return BadRequest(new { error = result.ErrorMessage });
                default:
                    return BadRequest(new { error = result.ErrorMessage });
            }
        }

        [HttpDelete("{id:int}/assignment")]
        public async Task<IActionResult> Unassign(int id, [FromBody] AssignLeadRequest request)
        {
            var result = await _leadService.UnassignAsync(id, request);
            switch (result.Status)
            {
                case CRM.BLL.Common.ServiceResultStatus.Success:
                    return Ok();
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
