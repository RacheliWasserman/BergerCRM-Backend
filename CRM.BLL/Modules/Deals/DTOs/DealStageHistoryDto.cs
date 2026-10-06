using System;

namespace CRM.BLL.Modules.Deals.DTOs
{
    public class DealStageHistoryDto
    {
        public int DealStageId { get; set; }
        public string? DealStageCode { get; set; }
        public string? DealStageName { get; set; }
        public DateTime ChangedAt { get; set; }
        public int? ChangedByUserId { get; set; }
        public string? ChangedByUserName { get; set; }
        public string? Reason { get; set; }
        public string? Notes { get; set; }
    }
}
