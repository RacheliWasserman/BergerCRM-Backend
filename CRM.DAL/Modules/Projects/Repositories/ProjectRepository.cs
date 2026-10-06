using System;
using System.Collections.Generic;
using System.Text;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Data;
using CRM.DAL.Modules.Projects.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DAL.Modules.Projects.Repositories
{
    public class ProjectRepository
        : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(CrmDbContext context)
            : base(context)
        {
        }

        public async Task<Project?> GetByCodeAsync(string code)
        {
            return await _context.Projects
                .AsNoTracking()
                .FirstOrDefaultAsync(p =>
                    p.Code == code &&
                    !p.IsDeleted);
        }

        public async Task<List<Project>> GetActiveProjectsAsync()
        {
            return await _context.Projects
                .AsNoTracking()
                .Include(p => p.ProjectType)
                .Include(p => p.ProjectStatus)
                .Where(p =>
                    p.IsActive &&
                    !p.IsDeleted)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<Project?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Projects
                .AsNoTracking()
                .Include(p => p.ProjectType)
                .Include(p => p.ProjectStatus)
                .Include(p => p.ProjectManagerUser)
                .FirstOrDefaultAsync(p =>
                    p.Id == id &&
                    !p.IsDeleted);
        }

        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _context.Projects
                .AsNoTracking()
                .Include(p => p.ProjectType)
                .Include(p => p.ProjectStatus)
                .Where(p => !p.IsDeleted)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<bool> CodeExistsAsync(string code, int? excludeProjectId = null)
        {
            var query = _context.Projects
                .AsQueryable()
                .Where(p => !p.IsDeleted && p.Code == code);

            if (excludeProjectId.HasValue)
            {
                query = query.Where(p => p.Id != excludeProjectId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<bool> ProjectTypeExistsAsync(int projectTypeId)
        {
            return await _context.ProjectTypes
                .AsNoTracking()
                .AnyAsync(pt => pt.Id == projectTypeId && pt.IsActive && !pt.IsDeleted);
        }

        public async Task<bool> ProjectStatusExistsAsync(int projectStatusId)
        {
            return await _context.ProjectStatuses
                .AsNoTracking()
                .AnyAsync(ps => ps.Id == projectStatusId && ps.IsActive && !ps.IsDeleted);
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            return await _context.Users
                .AsNoTracking()
                .AnyAsync(u => u.Id == userId && !u.IsDeleted && u.IsActive);
        }

        public async Task<Project?> GetByIdForUpdateAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.ProjectType)
                .Include(p => p.ProjectStatus)
                .Include(p => p.ProjectManagerUser)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }
    }
}
