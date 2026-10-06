using System;
using CRM.DAL.Common;
using CRM.DAL.Modules.Customers.Entities;
using CRM.DAL.Modules.Leads.Entities;
using CRM.DAL.Modules.Projects.Entities;
using CRM.DAL.Modules.Identity.Entities;

namespace CRM.DAL.Modules.Activities.Entities
{
    public class Activity : BaseEntity
    {
        public int ActivityTypeId { get; set; }
        public ActivityType ActivityType { get; set; } = null!;

        public int? ContactId { get; set; }
        public Contact? Contact { get; set; }

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int? LeadId { get; set; }
        public Lead? Lead { get; set; }

        public int? ProjectId { get; set; }
        public Project? Project { get; set; }

        public int? UnitId { get; set; }
        public Unit? Unit { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }

        public DateTime ActivityAt { get; set; } = DateTime.UtcNow;

        public string? Subject { get; set; }

        public string? Description { get; set; }

        public bool IsInbound { get; set; } = false;
    }
}
