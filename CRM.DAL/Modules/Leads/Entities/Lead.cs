using System;
using System.Collections.Generic;
using CRM.DAL.Common;
using CRM.DAL.Modules.Customers.Entities;
using CRM.DAL.Modules.Projects.Entities;
using CRM.DAL.Modules.Identity.Entities;

namespace CRM.DAL.Modules.Leads.Entities
{
    public class Lead : BaseEntity
    {
        public string LeadNumber { get; set; } = null!;

        public int ContactId { get; set; }
        public CRM.DAL.Modules.Customers.Entities.Contact Contact { get; set; } = null!;

        public int? ProjectId { get; set; }
        public Project? Project { get; set; }

        public int LeadSourceId { get; set; }
        public LeadSource LeadSource { get; set; } = null!;

        public int LeadStatusId { get; set; }
        public LeadStatus LeadStatus { get; set; } = null!;

        public int? LostReasonId { get; set; }
        public LostReason? LostReason { get; set; }

        public int? AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }

        public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;

        public DateTime? FirstContactAt { get; set; }

        public DateTime? QualifiedAt { get; set; }

        public DateTime? ConvertedAt { get; set; }

        public DateTime? LostAt { get; set; }

        public decimal? EstimatedBudget { get; set; }

        public string? Notes { get; set; }

        public bool IsActive { get; set; } = true;

        public LeadPreference? Preference { get; set; }

        public ICollection<LeadInterestedUnit> InterestedUnits { get; set; } = new List<LeadInterestedUnit>();

        public ICollection<LeadStatusHistory> StatusHistory { get; set; } = new List<LeadStatusHistory>();

        public ICollection<LeadAssignment> Assignments { get; set; } = new List<LeadAssignment>();
    }
}
