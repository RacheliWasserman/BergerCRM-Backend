using System;

namespace CRM.BLL.Modules.Leads.DTOs
{
    public class ChangeLeadStatusRequest
    {
        public int LeadStatusId { get; set; }
        public int? LostReasonId { get; set; }
        public int? ChangedByUserId { get; set; }
        public string? Reason { get; set; }
        public string? Notes { get; set; }
    }
}
