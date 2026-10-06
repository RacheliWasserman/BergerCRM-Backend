using System;

namespace CRM.BLL.Modules.Deals.DTOs
{
    public class CreateDealDto
    {
        public string DealNumber { get; set; } = null!;
        public int? LeadId { get; set; }
        public int ContactId { get; set; }
        public int? CustomerId { get; set; }
        public int ProjectId { get; set; }
        public int? UnitId { get; set; }
        public int DealStageId { get; set; }
        public int? OwnerUserId { get; set; }
        public decimal? ExpectedAmount { get; set; }
        public DateTime OpenedAt { get; set; }
        public DateTime? ExpectedCloseAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
