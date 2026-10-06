using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRM.BLL.Modules.Activities.Interfaces;
using CRM.BLL.Modules.Activities.DTOs;
using CRM.BLL.Common.Pagination;

namespace CRM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityService _service;

        public ActivitiesController(IActivityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] ActivityFilter filter = null)
        {
            var paged = await _service.GetPagedAsync(new PageRequest { Page = page, PageSize = pageSize }, filter ?? new ActivityFilter());
            return Ok(paged);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var r = await _service.GetByIdAsync(id);
            switch (r.Status)
            {
                case CRM.BLL.Common.ServiceResultStatus.Success:
                    return Ok(r.Data);
                case CRM.BLL.Common.ServiceResultStatus.NotFound:
                    return NotFound();
                case CRM.BLL.Common.ServiceResultStatus.ValidationError:
                    return BadRequest(new { error = r.ErrorMessage });
                default:
                    return BadRequest(new { error = r.ErrorMessage });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateActivityDto dto)
        {
            var r = await _service.CreateAsync(dto);
            switch (r.Status)
            {
                case CRM.BLL.Common.ServiceResultStatus.Success:
                    return CreatedAtAction(nameof(GetById), new { id = r.Data!.Id }, r.Data);
                case CRM.BLL.Common.ServiceResultStatus.Conflict:
                    return Conflict(new { error = r.ErrorMessage });
                case CRM.BLL.Common.ServiceResultStatus.ValidationError:
                    return BadRequest(new { error = r.ErrorMessage });
                default:
                    return BadRequest(new { error = r.ErrorMessage });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateActivityDto dto)
        {
            var r = await _service.UpdateAsync(id, dto);
            switch (r.Status)
            {
                case CRM.BLL.Common.ServiceResultStatus.Success:
                    return Ok(r.Data);
                case CRM.BLL.Common.ServiceResultStatus.NotFound:
                    return NotFound();
                case CRM.BLL.Common.ServiceResultStatus.Conflict:
                    return Conflict(new { error = r.ErrorMessage });
                case CRM.BLL.Common.ServiceResultStatus.ValidationError:
                    return BadRequest(new { error = r.ErrorMessage });
                default:
                    return BadRequest(new { error = r.ErrorMessage });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var r = await _service.DeleteAsync(id);
            switch (r.Status)
            {
                case CRM.BLL.Common.ServiceResultStatus.Success:
                    return NoContent();
                case CRM.BLL.Common.ServiceResultStatus.NotFound:
                    return NotFound();
                default:
                    return BadRequest(new { error = r.ErrorMessage });
            }
        }
    }
}
