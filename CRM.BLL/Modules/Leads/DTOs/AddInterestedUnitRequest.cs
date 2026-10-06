namespace CRM.BLL.Modules.Leads.DTOs
{
    public class AddInterestedUnitRequest
    {
        public int UnitId { get; set; }
        public int? AddedByUserId { get; set; }
        public string? Notes { get; set; }
    }
}
