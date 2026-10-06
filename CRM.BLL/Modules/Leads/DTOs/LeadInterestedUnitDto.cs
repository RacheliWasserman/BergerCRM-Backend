using System;

namespace CRM.BLL.Modules.Leads.DTOs
{
    public class LeadInterestedUnitDto
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public DateTime AddedAt { get; set; }
        public int? AddedByUserId { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
    }
}
