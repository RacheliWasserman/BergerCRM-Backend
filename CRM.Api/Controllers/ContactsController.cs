using Microsoft.AspNetCore.Mvc;
using CRM.BLL.Modules.Customers.Interfaces;
using CRM.BLL.Modules.Customers.DTOs;
using CRM.BLL.Common.Pagination;

namespace CRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _service;

        public ContactsController(IContactService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ContactListDto>>> Get([FromQuery] ContactFilter filter, [FromQuery] PageRequest pageRequest)
        {
            var result = await _service.GetPagedAsync(filter, pageRequest);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ContactDetailsDto>> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContactDto dto)
        {
            var result = await _service.CreateAsync(dto);
            switch (result.Status)
            {
                case CRM.BLL.Common.ServiceResultStatus.Success:
                    return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
                case CRM.BLL.Common.ServiceResultStatus.ValidationError:
                    return BadRequest(new { error = result.ErrorMessage });
                default:
                    return BadRequest(new { error = result.ErrorMessage });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateContactDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
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
    }
}
