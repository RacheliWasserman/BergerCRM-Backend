using System;

namespace CRM.BLL.Modules.Leads.DTOs
{
    public class LeadAssignmentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime AssignedAt { get; set; }
        public DateTime? UnassignedAt { get; set; }
        public int? AssignedByUserId { get; set; }
        public string? Reason { get; set; }
        public bool IsActive { get; set; }
    }
}
