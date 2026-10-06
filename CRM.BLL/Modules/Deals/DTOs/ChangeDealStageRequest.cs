using System;

namespace CRM.BLL.Modules.Deals.DTOs
{
    public class ChangeDealStageRequest
    {
        public int DealStageId { get; set; }
        public int? ChangedByUserId { get; set; }
        public string? Reason { get; set; }
        public string? Notes { get; set; }
    }
}
