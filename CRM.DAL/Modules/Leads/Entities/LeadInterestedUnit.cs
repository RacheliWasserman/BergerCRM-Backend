using System;
using CRM.DAL.Common;
using CRM.DAL.Modules.Projects.Entities;
using CRM.DAL.Modules.Identity.Entities;

namespace CRM.DAL.Modules.Leads.Entities
{
    public class LeadInterestedUnit : BaseEntity
    {
        public int LeadId { get; set; }
        public Lead Lead { get; set; } = null!;

        public int UnitId { get; set; }
        public Unit Unit { get; set; } = null!;

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        public int? AddedByUserId { get; set; }
        public User? AddedByUser { get; set; }

        public string? Notes { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
