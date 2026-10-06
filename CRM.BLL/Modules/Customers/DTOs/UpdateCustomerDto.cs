using System;

namespace CRM.BLL.Modules.Customers.DTOs
{
    public class UpdateCustomerDto
    {
        public string? DisplayName { get; set; }

        public int CustomerTypeId { get; set; }

        public int CustomerStatusId { get; set; }

        public string? Notes { get; set; }

        public bool IsActive { get; set; }
    }
}
