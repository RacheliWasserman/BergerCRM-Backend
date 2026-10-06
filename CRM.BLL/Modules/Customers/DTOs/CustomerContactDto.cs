using System;

namespace CRM.BLL.Modules.Customers.DTOs
{
    public class CustomerContactDto
    {
        public int ContactId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? RelationshipType { get; set; }

        public bool IsPrimary { get; set; }

        public bool IsActive { get; set; }

        public DateTime LinkedAt { get; set; }
    }
}
