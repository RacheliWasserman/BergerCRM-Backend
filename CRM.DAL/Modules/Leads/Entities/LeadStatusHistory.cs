using System;
using CRM.DAL.Common;
using CRM.DAL.Modules.Identity.Entities;

namespace CRM.DAL.Modules.Leads.Entities
{
    public class LeadStatusHistory : BaseEntity
    {
        public int LeadId { get; set; }
        public Lead Lead { get; set; } = null!;

        public int LeadStatusId { get; set; }
        public LeadStatus LeadStatus { get; set; } = null!;

        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        public int? ChangedByUserId { get; set; }
        public User? ChangedByUser { get; set; }

        public string? Reason { get; set; }

        public string? Notes { get; set; }
    }
}
