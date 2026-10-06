using System.Collections.Generic;
using CRM.DAL.Common;

namespace CRM.DAL.Modules.Deals.Entities
{
    public class DealStage : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        public int SortOrder { get; set; }

        public bool IsClosed { get; set; } = false;

        public bool IsWon { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public ICollection<Deal> Deals { get; set; } = new List<Deal>();

        public ICollection<DealStageHistory> DealStageHistories { get; set; } = new List<DealStageHistory>();
    }
}
