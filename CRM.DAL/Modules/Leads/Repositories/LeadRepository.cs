using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRM.DAL.Common.Models;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Data;
using CRM.DAL.Modules.Leads.Entities;

namespace CRM.DAL.Modules.Leads.Repositories
{
    public class LeadRepository : Repository<Lead>, ILeadRepository
    {
        public LeadRepository(CrmDbContext context)
            : base(context)
        {
        }

        public async Task<PagedData<Lead>> GetPagedAsync(int page, int pageSize, int? leadStatusId, int? leadSourceId, int? projectId, int? assignedUserId, int? contactId, bool? isActive, string? search, System.Threading.CancellationToken cancellationToken = default)
        {
            var query = _context.Leads
                .AsNoTracking()
                .Include(l => l.Contact)
                .Include(l => l.Project)
                .Include(l => l.LeadSource)
                .Include(l => l.LeadStatus)
                .Where(l => !l.IsDeleted);

            if (leadStatusId.HasValue)
                query = query.Where(l => l.LeadStatusId == leadStatusId.Value);

            if (leadSourceId.HasValue)
                query = query.Where(l => l.LeadSourceId == leadSourceId.Value);

            if (projectId.HasValue)
                query = query.Where(l => l.ProjectId == projectId.Value);

            if (assignedUserId.HasValue)
                query = query.Where(l => l.AssignedUserId == assignedUserId.Value);

            if (contactId.HasValue)
                query = query.Where(l => l.ContactId == contactId.Value);

            if (isActive.HasValue)
                query = query.Where(l => l.IsActive == isActive.Value);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim();
                query = query.Where(l => l.LeadNumber.Contains(s) || (l.Contact != null && (l.Contact.FirstName.Contains(s) || l.Contact.LastName.Contains(s) || (l.Contact.Email != null && l.Contact.Email.Contains(s)))));
            }

            var total = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(l => l.LeadNumber)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedData<Lead>
            {
                Items = items,
                TotalCount = total
            };
        }

        public async Task<Lead?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Leads
                .AsNoTracking()
                .Include(l => l.Contact)
                .Include(l => l.Project)
                .Include(l => l.LeadSource)
                .Include(l => l.LeadStatus)
                .Include(l => l.LostReason)
                .Include(l => l.Preference)
                .Include(l => l.InterestedUnits).ThenInclude(i => i.Unit)
                .Include(l => l.StatusHistory).ThenInclude(h => h.LeadStatus)
                .Include(l => l.Assignments)
                .FirstOrDefaultAsync(l => l.Id == id && !l.IsDeleted);
        }

        public async Task<Lead?> GetByIdForUpdateAsync(int id)
        {
            return await _context.Leads
                .Include(l => l.Preference)
                .Include(l => l.InterestedUnits)
                .Include(l => l.StatusHistory)
                .Include(l => l.Assignments)
                .FirstOrDefaultAsync(l => l.Id == id && !l.IsDeleted);
        }

        public async Task<bool> LeadNumberExistsAsync(string leadNumber, int? excludeLeadId = null)
        {
            var q = _context.Leads.AsQueryable();
            if (excludeLeadId.HasValue)
                q = q.Where(l => l.Id != excludeLeadId.Value);

            return await q.AnyAsync(l => l.LeadNumber == leadNumber);
        }

        public async Task<bool> ContactExistsAsync(int contactId)
        {
            return await _context.Contacts.AnyAsync(c => c.Id == contactId && !c.IsDeleted);
        }

        public async Task<bool> ProjectExistsAsync(int projectId)
        {
            return await _context.Projects.AnyAsync(p => p.Id == projectId && !p.IsDeleted);
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId && !u.IsDeleted);
        }

        public async Task<bool> LeadSourceExistsAsync(int leadSourceId)
        {
            return await _context.LeadSources.AnyAsync(ls => ls.Id == leadSourceId && ls.IsActive && !ls.IsDeleted);
        }

        public async Task<LeadStatus?> GetLeadStatusByIdAsync(int leadStatusId)
        {
            return await _context.LeadStatuses.FirstOrDefaultAsync(s => s.Id == leadStatusId);
        }

        public async Task<bool> LostReasonExistsAsync(int lostReasonId)
        {
            return await _context.LostReasons.AnyAsync(lr => lr.Id == lostReasonId && lr.IsActive && !lr.IsDeleted);
        }

        public async Task<bool> UnitExistsAsync(int unitId)
        {
            return await _context.Units.AnyAsync(u => u.Id == unitId && !u.IsDeleted);
        }
    }
}
