using System;

namespace CRM.BLL.Modules.Leads.DTOs
{
    public class LeadListDto
    {
        public int Id { get; set; }
        public string LeadNumber { get; set; } = null!;
        public int ContactId { get; set; }
        public string? ContactName { get; set; }
        public int? ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public int LeadSourceId { get; set; }
        public string? LeadSourceName { get; set; }
        public int LeadStatusId { get; set; }
        public string? LeadStatusName { get; set; }
        public int? AssignedUserId { get; set; }
        public DateTime ReceivedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
