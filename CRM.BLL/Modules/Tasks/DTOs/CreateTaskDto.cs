using System;

namespace CRM.BLL.Modules.Tasks.DTOs
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int TaskStateId { get; set; }
        public int PriorityId { get; set; }
        public int? AssignedToUserId { get; set; }
        public int? CreatedForLeadId { get; set; }
        public int? CustomerId { get; set; }
        public int? ProjectId { get; set; }
        public DateTime? DueAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
