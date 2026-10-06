namespace CRM.BLL.Modules.Customers.DTOs
{
    public class LinkContactRequest
    {
        public int ContactId { get; set; }

        public string? RelationshipType { get; set; }

        public bool IsPrimary { get; set; }
    }
}
