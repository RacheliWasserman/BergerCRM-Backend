using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Common.Models;
using CRM.DAL.Modules.Tasks.Entities;
using CRM.DAL.Data;

namespace CRM.DAL.Modules.Tasks.Repositories
{
    public class TaskRepository : Repository<TaskItem>, ITaskRepository
    {
        public TaskRepository(CrmDbContext context)
            : base(context)
        {
        }

        public async Task<PagedData<TaskItem>> GetPagedAsync(int page, int pageSize, int? taskStateId, int? priorityId, int? assignedToUserId, int? createdForLeadId, int? customerId, int? projectId)
        {
            var q = _context.Tasks.AsNoTracking().Where(t => !t.IsDeleted && t.IsActive);

            if (taskStateId.HasValue) q = q.Where(t => t.TaskStateId == taskStateId.Value);
            if (priorityId.HasValue) q = q.Where(t => t.PriorityId == priorityId.Value);
            if (assignedToUserId.HasValue) q = q.Where(t => t.AssignedToUserId == assignedToUserId.Value);
            if (createdForLeadId.HasValue) q = q.Where(t => t.CreatedForLeadId == createdForLeadId.Value);
            if (customerId.HasValue) q = q.Where(t => t.CustomerId == customerId.Value);
            if (projectId.HasValue) q = q.Where(t => t.ProjectId == projectId.Value);

            var total = await q.CountAsync();

            var items = await q
                .Include(t => t.TaskState)
                .Include(t => t.Priority)
                .Include(t => t.AssignedToUser)
                .Include(t => t.CreatedForLead)
                .Include(t => t.Customer)
                .Include(t => t.Project)
                .OrderByDescending(t => t.DueAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedData<TaskItem>
            {
                Items = items,
                TotalCount = total
            };
        }

        public async Task<TaskItem?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Tasks
                .AsNoTracking()
                .Include(t => t.TaskState)
                .Include(t => t.Priority)
                .Include(t => t.AssignedToUser)
                .Include(t => t.CreatedForLead)
                .Include(t => t.Customer)
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id).AsTask();
        }

        public async Task AddAsync(TaskItem item)
        {
            await _context.Tasks.AddAsync(item);
        }

        public void Update(TaskItem item)
        {
            _context.Tasks.Update(item);
        }

        public async Task<bool> TaskStateExistsAsync(int id)
        {
            return await _context.TaskStates.AnyAsync(s => s.Id == id && !s.IsDeleted && s.IsActive);
        }

        public async Task<bool> PriorityExistsAsync(int id)
        {
            return await _context.Priorities.AnyAsync(p => p.Id == id && !p.IsDeleted && p.IsActive);
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id && !u.IsDeleted);
        }

        public async Task<bool> LeadExistsAsync(int id)
        {
            return await _context.Leads.AnyAsync(l => l.Id == id && !l.IsDeleted);
        }

        public async Task<bool> CustomerExistsAsync(int id)
        {
            return await _context.Customers.AnyAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<bool> ProjectExistsAsync(int id)
        {
            return await _context.Projects.AnyAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<TaskState?> GetTaskStateByIdAsync(int id)
        {
            return await _context.TaskStates.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        }
    }
}
