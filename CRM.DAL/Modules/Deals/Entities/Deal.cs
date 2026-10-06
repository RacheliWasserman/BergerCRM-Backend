using System;
using System.Collections.Generic;
using CRM.DAL.Common;
using CRM.DAL.Modules.Leads.Entities;
using CRM.DAL.Modules.Customers.Entities;
using CRM.DAL.Modules.Projects.Entities;
using CRM.DAL.Modules.Identity.Entities;

namespace CRM.DAL.Modules.Deals.Entities
{
    public class Deal : BaseEntity
    {
        public string DealNumber { get; set; } = null!;

        public int? LeadId { get; set; }
        public Lead? Lead { get; set; }

        public int ContactId { get; set; }
        public CRM.DAL.Modules.Customers.Entities.Contact Contact { get; set; } = null!;

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public int? UnitId { get; set; }
        public CRM.DAL.Modules.Projects.Entities.Unit? Unit { get; set; }

        public int DealStageId { get; set; }
        public DealStage DealStage { get; set; } = null!;

        public int? OwnerUserId { get; set; }
        public User? OwnerUser { get; set; }

        public decimal? ExpectedAmount { get; set; }

        public DateTime OpenedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ExpectedCloseAt { get; set; }

        public DateTime? ClosedAt { get; set; }

        public string? Notes { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<DealStageHistory> StageHistory { get; set; } = new List<DealStageHistory>();
    }
}
