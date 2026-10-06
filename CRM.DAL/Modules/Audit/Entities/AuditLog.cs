using System;
using CRM.DAL.Common;
using CRM.DAL.Modules.Identity.Entities;

namespace CRM.DAL.Modules.Audit.Entities
{
    public class AuditLog : BaseEntity
    {
        public int? UserId { get; set; }
        public User? User { get; set; }

        public string Action { get; set; } = null!;

        public string EntityName { get; set; } = null!;

        public string? EntityId { get; set; }

        public string? OldValues { get; set; }

        public string? NewValues { get; set; }

        public string? IpAddress { get; set; }

        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
    }
}
