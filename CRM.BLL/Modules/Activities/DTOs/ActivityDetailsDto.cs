using System;

namespace CRM.BLL.Modules.Activities.DTOs
{
    public class ActivityDetailsDto
    {
        public int Id { get; set; }
        public int ActivityTypeId { get; set; }
        public string? ActivityTypeName { get; set; }
        public int? ContactId { get; set; }
        public string? ContactName { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int? LeadId { get; set; }
        public string? LeadNumber { get; set; }
        public int? ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public int? UnitId { get; set; }
        public string? UnitName { get; set; }
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime ActivityAt { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public bool IsInbound { get; set; }
    }
}
