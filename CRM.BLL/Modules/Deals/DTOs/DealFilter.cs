using System;

namespace CRM.BLL.Modules.Deals.DTOs
{
    public class DealFilter
    {
        public int? ProjectId { get; set; }
        public int? OwnerUserId { get; set; }
        public int? ContactId { get; set; }
        public int? DealStageId { get; set; }
        public bool? IsActive { get; set; }
        public string? Search { get; set; }
    }
}
