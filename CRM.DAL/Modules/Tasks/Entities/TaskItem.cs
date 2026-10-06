using System;
using CRM.DAL.Common;
using CRM.DAL.Modules.Identity.Entities;
using CRM.DAL.Modules.Leads.Entities;
using CRM.DAL.Modules.Customers.Entities;
using CRM.DAL.Modules.Projects.Entities;

namespace CRM.DAL.Modules.Tasks.Entities
{
    public class TaskItem : BaseEntity
    {
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public int TaskStateId { get; set; }
        public TaskState TaskState { get; set; } = null!;

        public int PriorityId { get; set; }
        public Priority Priority { get; set; } = null!;

        public int? AssignedToUserId { get; set; }
        public User? AssignedToUser { get; set; }

        public int? CreatedForLeadId { get; set; }
        public Lead? CreatedForLead { get; set; }

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int? ProjectId { get; set; }
        public Project? Project { get; set; }

        public DateTime? DueAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
