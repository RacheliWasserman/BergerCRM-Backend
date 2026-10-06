namespace CRM.BLL.Modules.Leads.DTOs
{
    public class AssignLeadRequest
    {
        public int? AssignedUserId { get; set; }
        public int? AssignedByUserId { get; set; }
        public string? Reason { get; set; }
    }
}
