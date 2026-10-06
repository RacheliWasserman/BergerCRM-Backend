using System;
using CRM.DAL.Common;
using CRM.DAL.Modules.Identity.Entities;

namespace CRM.DAL.Modules.Leads.Entities
{
    public class LeadAssignment : BaseEntity
    {
        public int LeadId { get; set; }
        public Lead Lead { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UnassignedAt { get; set; }

        public int? AssignedByUserId { get; set; }
        public User? AssignedByUser { get; set; }

        public string? Reason { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
