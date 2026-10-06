using System;

namespace CRM.BLL.Modules.Leads.DTOs
{
    public class LeadStatusHistoryDto
    {
        public int Id { get; set; }
        public int LeadStatusId { get; set; }
        public string? LeadStatusName { get; set; }
        public DateTime ChangedAt { get; set; }
        public int? ChangedByUserId { get; set; }
        public string? Reason { get; set; }
        public string? Notes { get; set; }
    }
}
