using System;

namespace CRM.BLL.Modules.Leads.DTOs
{
    public class LeadFilter
    {
        public string? Search { get; set; }
        public int? LeadStatusId { get; set; }
        public int? LeadSourceId { get; set; }
        public int? ProjectId { get; set; }
        public int? AssignedUserId { get; set; }
        public int? ContactId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? ReceivedFrom { get; set; }
        public DateTime? ReceivedTo { get; set; }
    }
}
