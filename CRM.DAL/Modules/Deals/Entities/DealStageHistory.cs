using System;
using CRM.DAL.Common;
using CRM.DAL.Modules.Identity.Entities;

namespace CRM.DAL.Modules.Deals.Entities
{
    public class DealStageHistory : BaseEntity
    {
        public int DealId { get; set; }
        public Deal Deal { get; set; } = null!;

        public int DealStageId { get; set; }
        public DealStage DealStage { get; set; } = null!;

        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        public int? ChangedByUserId { get; set; }
        public User? ChangedByUser { get; set; }

        public string? Reason { get; set; }

        public string? Notes { get; set; }
    }
}
