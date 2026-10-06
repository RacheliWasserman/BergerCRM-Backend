using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRM.DAL.Common.Models;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Data;
using CRM.DAL.Modules.Activities.Entities;

namespace CRM.DAL.Modules.Activities.Repositories
{
    public class ActivityRepository : Repository<Activity>, IActivityRepository
    {
        public ActivityRepository(CrmDbContext context)
            : base(context)
        {
        }

        public async Task<PagedData<Activity>> GetPagedAsync(int page, int pageSize, int? activityTypeId, int? contactId, int? customerId, int? leadId, int? projectId, int? unitId, int? userId, System.Threading.CancellationToken cancellationToken = default)
        {
            var query = _context.Activities
                .AsNoTracking()
                .Include(a => a.ActivityType)
                .Include(a => a.Contact)
                .Include(a => a.Customer)
                .Include(a => a.Lead)
                .Include(a => a.Project)
                .Include(a => a.Unit)
                .Include(a => a.User)
                .Where(a => !a.IsDeleted);

            if (activityTypeId.HasValue) query = query.Where(a => a.ActivityTypeId == activityTypeId.Value);
            if (contactId.HasValue) query = query.Where(a => a.ContactId == contactId.Value);
            if (customerId.HasValue) query = query.Where(a => a.CustomerId == customerId.Value);
            if (leadId.HasValue) query = query.Where(a => a.LeadId == leadId.Value);
            if (projectId.HasValue) query = query.Where(a => a.ProjectId == projectId.Value);
            if (unitId.HasValue) query = query.Where(a => a.UnitId == unitId.Value);
            if (userId.HasValue) query = query.Where(a => a.UserId == userId.Value);

            var total = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(a => a.ActivityAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedData<Activity> { Items = items, TotalCount = total };
        }

        public async Task<Activity?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Activities
                .AsNoTracking()
                .Include(a => a.ActivityType)
                .Include(a => a.Contact)
                .Include(a => a.Customer)
                .Include(a => a.Lead)
                .Include(a => a.Project)
                .Include(a => a.Unit)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<bool> ActivityTypeExistsAsync(int activityTypeId)
            => await _context.ActivityTypes.AnyAsync(x => x.Id == activityTypeId && x.IsActive && !x.IsDeleted);

        public async Task<bool> ContactExistsAsync(int contactId)
            => await _context.Contacts.AnyAsync(c => c.Id == contactId && !c.IsDeleted);

        public async Task<bool> CustomerExistsAsync(int customerId)
            => await _context.Customers.AnyAsync(c => c.Id == customerId && !c.IsDeleted);

        public async Task<bool> LeadExistsAsync(int leadId)
            => await _context.Leads.AnyAsync(l => l.Id == leadId && !l.IsDeleted);

        public async Task<bool> ProjectExistsAsync(int projectId)
            => await _context.Projects.AnyAsync(p => p.Id == projectId && !p.IsDeleted);

        public async Task<bool> UnitExistsAsync(int unitId)
            => await _context.Units.AnyAsync(u => u.Id == unitId && !u.IsDeleted);

        public async Task<bool> UserExistsAsync(int userId)
            => await _context.Users.AnyAsync(u => u.Id == userId && !u.IsDeleted);
    }
}
