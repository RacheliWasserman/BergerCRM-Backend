using System.Threading;
using System.Threading.Tasks;

namespace CRM.BLL.Audit
{
    public interface IAuditService
    {
        Task LogAsync(string action, string entityName, string? entityId, int? userId, object? oldValues, object? newValues, string? ipAddress = null, CancellationToken cancellationToken = default);
    }
}
