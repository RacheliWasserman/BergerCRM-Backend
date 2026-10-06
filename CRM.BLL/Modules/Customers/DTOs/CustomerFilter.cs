namespace CRM.BLL.Modules.Customers.DTOs
{
    public class CustomerFilter
    {
        public int? CustomerTypeId { get; set; }

        public int? CustomerStatusId { get; set; }

        public bool? IsActive { get; set; }

        public string? Search { get; set; }
    }
}
