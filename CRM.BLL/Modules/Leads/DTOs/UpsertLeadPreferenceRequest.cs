namespace CRM.BLL.Modules.Leads.DTOs
{
    public class UpsertLeadPreferenceRequest
    {
        public decimal? MinBudget { get; set; }
        public decimal? MaxBudget { get; set; }
        public decimal? MinRooms { get; set; }
        public decimal? MaxRooms { get; set; }
        public decimal? MinArea { get; set; }
        public decimal? MaxArea { get; set; }
        public int? MinFloor { get; set; }
        public int? MaxFloor { get; set; }
        public string? PreferredCity { get; set; }
        public bool? WantsBalcony { get; set; }
        public bool? WantsGarden { get; set; }
        public bool? WantsParking { get; set; }
        public bool? WantsStorage { get; set; }
        public bool? RequiresAccessibility { get; set; }
        public string? PreferredDirections { get; set; }
        public string? Notes { get; set; }
    }
}
