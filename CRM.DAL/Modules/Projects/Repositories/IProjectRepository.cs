using System;
using System.Collections.Generic;
using System.Text;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Modules.Projects.Entities;

namespace CRM.DAL.Modules.Projects.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<Project?> GetByCodeAsync(string code);

        Task<Project?> GetByIdWithDetailsAsync(int id);

        Task<List<Project>> GetActiveProjectsAsync();

        Task<List<Project>> GetAllProjectsAsync();

        Task<bool> CodeExistsAsync(string code, int? excludeProjectId = null);

        Task<bool> ProjectTypeExistsAsync(int projectTypeId);

        Task<bool> ProjectStatusExistsAsync(int projectStatusId);

        Task<bool> UserExistsAsync(int userId);

        Task<Project?> GetByIdForUpdateAsync(int id);
    }
}
