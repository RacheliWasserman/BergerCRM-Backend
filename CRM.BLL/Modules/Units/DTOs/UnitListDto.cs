using System;

namespace CRM.BLL.Modules.Units.DTOs
{
    public class UnitListDto
    {
        public int Id { get; set; }
        public string UnitNumber { get; set; } = null!;
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public int BuildingId { get; set; }
        public string? BuildingName { get; set; }
        public int FloorId { get; set; }
        public string? FloorDisplay { get; set; }
        public int UnitTypeId { get; set; }
        public string? UnitTypeName { get; set; }
        public int UnitStatusId { get; set; }
        public string? UnitStatusName { get; set; }
        public decimal? CurrentPrice { get; set; }
        public decimal? TotalArea { get; set; }
        public decimal? BuiltArea { get; set; }
    }
}
