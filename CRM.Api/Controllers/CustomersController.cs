using Microsoft.AspNetCore.Mvc;
using CRM.BLL.Modules.Customers.Interfaces;
using CRM.BLL.Modules.Customers.DTOs;
using CRM.BLL.Common.Pagination;

namespace CRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<CustomerListDto>>> Get([FromQuery] CustomerFilter filter, [FromQuery] PageRequest pageRequest)
        {
            var result = await _service.GetPagedAsync(filter, pageRequest);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerDetailsDto>> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
        {
            var result = await _service.CreateAsync(dto);
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
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

        [HttpGet("{customerId:int}/contacts")]
        public async Task<IActionResult> GetContacts(int customerId)
        {
            var list = await _service.GetContactsAsync(customerId);
            if (list == null) return NotFound();
            return Ok(list);
        }

        [HttpPost("{customerId:int}/contacts")]
        public async Task<IActionResult> LinkContact(int customerId, [FromBody] LinkContactRequest request)
        {
            var result = await _service.LinkContactAsync(customerId, request);
            switch (result.Status)
            {
                case CRM.BLL.Common.ServiceResultStatus.Success:
                    return Created(string.Empty, result.Data);
                case CRM.BLL.Common.ServiceResultStatus.NotFound:
                    return NotFound(new { error = result.ErrorMessage });
                case CRM.BLL.Common.ServiceResultStatus.Conflict:
                    return Conflict(new { error = result.ErrorMessage });
                case CRM.BLL.Common.ServiceResultStatus.ValidationError:
                    return BadRequest(new { error = result.ErrorMessage });
                default:
                    return BadRequest(new { error = result.ErrorMessage });
            }
        }

        [HttpDelete("{customerId:int}/contacts/{contactId:int}")]
        public async Task<IActionResult> UnlinkContact(int customerId, int contactId)
        {
            var result = await _service.UnlinkContactAsync(customerId, contactId);
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
