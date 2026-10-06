using System.Threading.Tasks;
using CRM.DAL.Common.Models;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Modules.Audit.Entities;

namespace CRM.DAL.Modules.Audit.Repositories
{
    public interface IAuditLogRepository : IRepository<AuditLog>
    {
        // Adds the audit entry to the context; does not call SaveChanges
        Task AddAsync(AuditLog entry);

        Task<PagedData<AuditLog>> GetPagedAsync(int page, int pageSize, string? entityName, string? entityId, int? userId, string? action);

        Task<AuditLog?> GetByIdAsync(int id);
    }
}
