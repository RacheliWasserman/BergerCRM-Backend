using System;

namespace CRM.BLL.Modules.Customers.DTOs
{
    public class CustomerListDto
    {
        public int Id { get; set; }

        public string CustomerNumber { get; set; } = null!;

        public string? DisplayName { get; set; }

        public int CustomerTypeId { get; set; }

        public string CustomerTypeName { get; set; } = null!;

        public int CustomerStatusId { get; set; }

        public string CustomerStatusName { get; set; } = null!;

        public DateTime CustomerSince { get; set; }

        public bool IsActive { get; set; }
    }
}
