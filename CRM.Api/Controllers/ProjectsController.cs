using CRM.BLL.Modules.Projects.DTOs;
using CRM.BLL.Modules.Projects.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("active")]
        public async Task<ActionResult<List<ProjectListDto>>> GetActiveProjects()
        {
            var projects =
                await _projectService.GetActiveProjectsAsync();

            return Ok(projects);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectListDto>>> GetAll()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProjectDetailsDto>> GetById(int id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null) return NotFound();
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectDto dto)
        {
            var result = await _projectService.CreateAsync(dto);
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProjectDto dto)
        {
            var result = await _projectService.UpdateAsync(id, dto);
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
            var result = await _projectService.DeleteAsync(id);
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
