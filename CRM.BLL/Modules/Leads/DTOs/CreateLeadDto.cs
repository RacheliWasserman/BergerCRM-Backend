using System;

namespace CRM.BLL.Modules.Leads.DTOs
{
    public class CreateLeadDto
    {
        public string LeadNumber { get; set; } = null!;
        public int ContactId { get; set; }
        public int? ProjectId { get; set; }
        public int LeadSourceId { get; set; }
        public int LeadStatusId { get; set; }
        public int? LostReasonId { get; set; }
        public int? AssignedUserId { get; set; }
        public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
        public DateTime? FirstContactAt { get; set; }
        public DateTime? QualifiedAt { get; set; }
        public decimal? EstimatedBudget { get; set; }
        public string? Notes { get; set; }
    }
}
