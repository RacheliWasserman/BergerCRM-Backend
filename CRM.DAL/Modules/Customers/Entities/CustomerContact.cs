using System;
using CRM.DAL.Common;

namespace CRM.DAL.Modules.Customers.Entities
{
    public class CustomerContact : BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public int ContactId { get; set; }
        public Contact Contact { get; set; } = null!;

        public string? RelationshipType { get; set; }

        public bool IsPrimary { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime LinkedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UnlinkedAt { get; set; }
    }
}
