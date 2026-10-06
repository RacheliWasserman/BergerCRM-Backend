using System;

namespace CRM.BLL.Modules.Tasks.DTOs
{
    public class TaskDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int TaskStateId { get; set; }
        public string? TaskStateCode { get; set; }
        public string? TaskStateName { get; set; }
        public int PriorityId { get; set; }
        public string? PriorityCode { get; set; }
        public string? PriorityName { get; set; }
        public int? AssignedToUserId { get; set; }
        public string? AssignedToUserName { get; set; }
        public int? CreatedForLeadId { get; set; }
        public string? LeadNumber { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int? ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public DateTime? DueAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool IsOverdue { get; set; }
        public bool IsActive { get; set; }
    }
}
