using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CRM.DAL.Modules.Audit.Entities;
using CRM.DAL.Modules.Audit.Repositories;

namespace CRM.BLL.Audit
{
    public class AuditService : IAuditService
    {
        private readonly IAuditLogRepository _auditRepo;

        public AuditService(IAuditLogRepository auditRepo)
        {
            _auditRepo = auditRepo;
        }

        public async Task LogAsync(string action, string entityName, string? entityId, int? userId, object? oldValues, object? newValues, string? ipAddress = null, CancellationToken cancellationToken = default)
        {
            var entry = new AuditLog
            {
                Action = action,
                EntityName = entityName,
                EntityId = entityId,
                UserId = userId,
                OldValues = oldValues == null ? null : JsonSerializer.Serialize(oldValues),
                NewValues = newValues == null ? null : JsonSerializer.Serialize(newValues),
                IpAddress = ipAddress
            };

            await _auditRepo.AddAsync(entry);
            // Intentionally do not call SaveChanges here. Caller will commit.
        }
    }
}
