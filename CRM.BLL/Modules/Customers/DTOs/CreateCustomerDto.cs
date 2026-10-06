using System;

namespace CRM.BLL.Modules.Customers.DTOs
{
    public class CreateCustomerDto
    {
        public string CustomerNumber { get; set; } = null!;

        public int CustomerTypeId { get; set; }

        public int CustomerStatusId { get; set; }

        public string? DisplayName { get; set; }

        public DateTime? CustomerSince { get; set; }

        public string? Notes { get; set; }
    }
}
