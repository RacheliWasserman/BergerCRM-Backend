namespace CRM.BLL.Modules.Users.DTOs
{
    public class UserLookupDto
    {
        public int Id { get; set; }
        public string DisplayName { get; set; } = null!;
        public string? Email { get; set; }
        public bool IsActive { get; set; }
    }
}
