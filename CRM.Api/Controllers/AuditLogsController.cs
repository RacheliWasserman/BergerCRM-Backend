using Microsoft.AspNetCore.Mvc;
using CRM.DAL.Modules.Audit.Entities;
using CRM.DAL.Modules.Audit.Repositories;
using CRM.BLL.Common.Pagination;

namespace CRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditLogsController : ControllerBase
    {
        private readonly IAuditLogRepository _auditRepo;

        public AuditLogsController(IAuditLogRepository auditRepo)
        {
            _auditRepo = auditRepo;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<AuditLog>>> Get([FromQuery] PageRequest pageRequest, [FromQuery] string? entityName, [FromQuery] string? entityId, [FromQuery] int? userId, [FromQuery] string? action)
        {
            var data = await _auditRepo.GetPagedAsync(pageRequest.Page, pageRequest.PageSize, entityName, entityId, userId, action);
            return Ok(new PagedResult<AuditLog> { Items = data.Items, TotalCount = data.TotalCount, Page = pageRequest.Page, PageSize = pageRequest.PageSize });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _auditRepo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }
    }
}
