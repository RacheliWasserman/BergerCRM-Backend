namespace CRM.BLL.Modules.Customers.DTOs
{
    public class ContactListDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? City { get; set; }

        public bool IsActive { get; set; }
    }
}
