using System;
using System.Collections.Generic;

namespace CRM.BLL.Modules.Leads.DTOs
{
    public class LeadDetailsDto
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
        public int? LostReasonId { get; set; }
        public string? LostReasonName { get; set; }
        public int? AssignedUserId { get; set; }
        public string? AssignedUserName { get; set; }
        public DateTime ReceivedAt { get; set; }
        public DateTime? FirstContactAt { get; set; }
        public DateTime? QualifiedAt { get; set; }
        public DateTime? ConvertedAt { get; set; }
        public DateTime? LostAt { get; set; }
        public decimal? EstimatedBudget { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }

        public LeadPreferenceDto? Preference { get; set; }
        public List<LeadInterestedUnitDto> InterestedUnits { get; set; } = new List<LeadInterestedUnitDto>();
        public List<LeadStatusHistoryDto> StatusHistory { get; set; } = new List<LeadStatusHistoryDto>();
        public List<LeadAssignmentDto> Assignments { get; set; } = new List<LeadAssignmentDto>();
    }
}
