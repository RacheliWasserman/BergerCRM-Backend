using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRM.DAL.Common.Models;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Data;
using CRM.DAL.Modules.Deals.Entities;

namespace CRM.DAL.Modules.Deals.Repositories
{
    public class DealRepository : Repository<Deal>, IDealRepository
    {
        public DealRepository(CrmDbContext context)
            : base(context)
        {
        }

        public async Task<PagedData<Deal>> GetPagedAsync(int page, int pageSize, int? projectId, int? ownerUserId, int? contactId, int? dealStageId, bool? isActive, string? search, System.Threading.CancellationToken cancellationToken = default)
        {
            var query = _context.Deals
                .AsNoTracking()
                .Include(d => d.Contact)
                .Include(d => d.Customer)
                .Include(d => d.Project)
                .Include(d => d.Unit)
                .Include(d => d.DealStage)
                .Include(d => d.OwnerUser)
                .Where(d => !d.IsDeleted);

            if (projectId.HasValue) query = query.Where(d => d.ProjectId == projectId.Value);
            if (ownerUserId.HasValue) query = query.Where(d => d.OwnerUserId == ownerUserId.Value);
            if (contactId.HasValue) query = query.Where(d => d.ContactId == contactId.Value);
            if (dealStageId.HasValue) query = query.Where(d => d.DealStageId == dealStageId.Value);
            if (isActive.HasValue) query = query.Where(d => d.IsActive == isActive.Value);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim();
                query = query.Where(d => d.DealNumber.Contains(s) || (d.Contact != null && (d.Contact.FirstName.Contains(s) || d.Contact.LastName.Contains(s))));
            }

            var total = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(d => d.OpenedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedData<Deal> { Items = items, TotalCount = total };
        }

        public async Task<Deal?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Deals
                .AsNoTracking()
                .Include(d => d.Contact)
                .Include(d => d.Customer)
                .Include(d => d.Project)
                .Include(d => d.Unit)
                .Include(d => d.DealStage)
                .Include(d => d.OwnerUser)
                .Include(d => d.StageHistory).ThenInclude(h => h.DealStage)
                .Include(d => d.StageHistory).ThenInclude(h => h.ChangedByUser)
                .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
        }

        public async Task<Deal?> GetByIdForUpdateAsync(int id)
        {
            return await _context.Deals
                .Include(d => d.StageHistory)
                .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
        }

        public async Task<bool> DealNumberExistsAsync(string dealNumber, int? excludeDealId = null)
        {
            var q = _context.Deals.AsQueryable();
            if (excludeDealId.HasValue) q = q.Where(d => d.Id != excludeDealId.Value);
            return await q.AnyAsync(d => d.DealNumber == dealNumber);
        }

        public async Task<bool> ContactExistsAsync(int contactId)
            => await _context.Contacts.AnyAsync(c => c.Id == contactId && !c.IsDeleted);

        public async Task<bool> ProjectExistsAsync(int projectId)
            => await _context.Projects.AnyAsync(p => p.Id == projectId && !p.IsDeleted);

        public async Task<bool> UnitExistsAsync(int unitId)
            => await _context.Units.AnyAsync(u => u.Id == unitId && !u.IsDeleted);

        public async Task<bool> CustomerExistsAsync(int customerId)
            => await _context.Customers.AnyAsync(c => c.Id == customerId && !c.IsDeleted);

        public async Task<bool> UserExistsAsync(int userId)
            => await _context.Users.AnyAsync(u => u.Id == userId && !u.IsDeleted);

        public async Task<DealStage?> GetDealStageByIdAsync(int dealStageId)
            => await _context.DealStages.FirstOrDefaultAsync(s => s.Id == dealStageId && !s.IsDeleted && s.IsActive);

        public async Task AddStageHistoryAsync(DealStageHistory history)
        {
            await _context.DealStageHistory.AddAsync(history);
        }

        public async Task<System.Collections.Generic.List<DealStageHistory>> GetStageHistoryAsync(int dealId)
        {
            return await _context.DealStageHistory
                .AsNoTracking()
                .Include(h => h.DealStage)
                .Include(h => h.ChangedByUser)
                .Where(h => h.DealId == dealId)
                .OrderBy(h => h.ChangedAt)
                .ToListAsync();
        }
    }
}
