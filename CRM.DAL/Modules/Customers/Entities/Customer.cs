using System;
using System.Collections.Generic;
using CRM.DAL.Common;

namespace CRM.DAL.Modules.Customers.Entities
{
    public class Customer : BaseEntity
    {
        public string CustomerNumber { get; set; } = null!;

        public int CustomerTypeId { get; set; }
        public CustomerType CustomerType { get; set; } = null!;

        public int CustomerStatusId { get; set; }
        public CustomerStatus CustomerStatus { get; set; } = null!;

        public string? DisplayName { get; set; }

        public DateTime CustomerSince { get; set; } = DateTime.UtcNow;

        public string? Notes { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<CustomerContact> CustomerContacts { get; set; } = new List<CustomerContact>();
    }
}
