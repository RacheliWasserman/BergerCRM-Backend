using System;

namespace CRM.BLL.Modules.Activities.DTOs
{
    public class CreateActivityDto
    {
        public int ActivityTypeId { get; set; }
        public int? ContactId { get; set; }
        public int? CustomerId { get; set; }
        public int? LeadId { get; set; }
        public int? ProjectId { get; set; }
        public int? UnitId { get; set; }
        public int? UserId { get; set; }
        public DateTime ActivityAt { get; set; } = DateTime.UtcNow;
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public bool IsInbound { get; set; } = false;
    }
}
