using System;
using System.Collections.Generic;
using CRM.DAL.Common;

namespace CRM.DAL.Modules.Customers.Entities
{
    public class Contact : BaseEntity
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Email { get; set; }

        public string? NormalizedEmail { get; set; }

        public string? Phone { get; set; }

        public string? SecondaryPhone { get; set; }

        public string? IdentificationNumber { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? PostalCode { get; set; }

        public string? Country { get; set; }

        public string? PreferredLanguage { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<CustomerContact> CustomerContacts { get; set; } = new List<CustomerContact>();
    }
}
