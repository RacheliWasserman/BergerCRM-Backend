using System;
using System.Collections.Generic;
using System.Text;

using CRM.BLL.Modules.Projects.DTOs;

namespace CRM.BLL.Modules.Projects.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectListDto>> GetActiveProjectsAsync();

        Task<List<ProjectListDto>> GetAllProjectsAsync();

        Task<ProjectDetailsDto?> GetByIdAsync(int id);

        Task<CRM.BLL.Common.ServiceResult<ProjectDetailsDto>> CreateAsync(CreateProjectDto dto);

        Task<CRM.BLL.Common.ServiceResult<ProjectDetailsDto>> UpdateAsync(int id, UpdateProjectDto dto);

        Task<CRM.BLL.Common.ServiceResult<object?>> DeleteAsync(int id);
    }
}
