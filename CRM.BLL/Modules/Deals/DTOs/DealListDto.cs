using System;

namespace CRM.BLL.Modules.Deals.DTOs
{
    public class DealListDto
    {
        public int Id { get; set; }
        public string DealNumber { get; set; } = null!;
        public int? LeadId { get; set; }
        public string? LeadNumber { get; set; }
        public int ContactId { get; set; }
        public string? ContactName { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public int? UnitId { get; set; }
        public string? UnitDisplay { get; set; }
        public int DealStageId { get; set; }
        public string? DealStageCode { get; set; }
        public string? DealStageName { get; set; }
        public int? OwnerUserId { get; set; }
        public string? OwnerUserName { get; set; }
        public decimal? ExpectedAmount { get; set; }
        public DateTime OpenedAt { get; set; }
        public DateTime? ExpectedCloseAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
    }
}
