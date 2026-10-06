using System.Threading.Tasks;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Data;
using CRM.DAL.Modules.Audit.Entities;
using CRM.DAL.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM.DAL.Modules.Audit.Repositories
{
    public class AuditLogRepository : Repository<AuditLog>, IAuditLogRepository
    {
        public AuditLogRepository(CrmDbContext context)
            : base(context)
        {
        }

        public async Task AddAsync(AuditLog entry)
        {
            await _context.AuditLogs.AddAsync(entry);
        }

        public async Task<PagedData<AuditLog>> GetPagedAsync(int page, int pageSize, string? entityName, string? entityId, int? userId, string? action)
        {
            var query = _context.AuditLogs.AsQueryable();
            query = query.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(entityName)) query = query.Where(a => a.EntityName == entityName);
            if (!string.IsNullOrWhiteSpace(entityId)) query = query.Where(a => a.EntityId == entityId);
            if (userId.HasValue) query = query.Where(a => a.UserId == userId.Value);
            if (!string.IsNullOrWhiteSpace(action)) query = query.Where(a => a.Action == action);

            var total = await query.CountAsync();
            var items = await query.OrderByDescending(a => a.OccurredAt).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedData<AuditLog> { Items = items, TotalCount = total };
        }

        public async Task<AuditLog?> GetByIdAsync(int id)
        {
            return await _context.AuditLogs.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }
    }
}
