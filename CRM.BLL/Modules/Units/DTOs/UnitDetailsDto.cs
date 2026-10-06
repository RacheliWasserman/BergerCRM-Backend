using System;

namespace CRM.BLL.Modules.Units.DTOs
{
    public class UnitDetailsDto
    {
        public int Id { get; set; }
        public string UnitNumber { get; set; } = null!;
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public int BuildingId { get; set; }
        public string? BuildingName { get; set; }
        public int FloorId { get; set; }
        public int FloorNumber { get; set; }
        public string? FloorName { get; set; }
        public int UnitTypeId { get; set; }
        public string? UnitTypeName { get; set; }
        public int UnitStatusId { get; set; }
        public string? UnitStatusName { get; set; }
        public decimal? CurrentPrice { get; set; }
        public decimal? TotalArea { get; set; }
        public decimal? BuiltArea { get; set; }
        public decimal? BalconyArea { get; set; }
        public decimal? GardenArea { get; set; }
        public decimal? RoofArea { get; set; }
        public decimal? PricePerSquareMeter { get; set; }
        public decimal? NumberOfRooms { get; set; }
        public int? NumberOfBedrooms { get; set; }
        public int? NumberOfBathrooms { get; set; }
        public string? MarketingDescription { get; set; }
        public string? InternalNotes { get; set; }
    }
}
