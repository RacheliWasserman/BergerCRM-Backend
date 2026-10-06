using System;
using System.Collections.Generic;
using System.Text;

using CRM.DAL.Common;

namespace CRM.DAL.Modules.Projects.Entities
{
    public class Unit : BaseEntity
    {
        public int FloorId { get; set; }

        public Floor Floor { get; set; } = null!;


        public int UnitTypeId { get; set; }

        public UnitType UnitType { get; set; } = null!;


        public int UnitStatusId { get; set; }

        public UnitStatus UnitStatus { get; set; } = null!;


        public string UnitNumber { get; set; } = null!;

        public string? InternalCode { get; set; }


        // Rooms

        public decimal? NumberOfRooms { get; set; }

        public int? NumberOfBedrooms { get; set; }

        public int? NumberOfBathrooms { get; set; }


        // Areas

        public decimal? BuiltArea { get; set; }

        public decimal? BalconyArea { get; set; }

        public decimal? GardenArea { get; set; }

        public decimal? RoofArea { get; set; }

        public decimal? TotalArea { get; set; }


        // Pricing

        public decimal? CurrentPrice { get; set; }

        public decimal? PricePerSquareMeter { get; set; }


        // Apartment properties

        public string? Direction { get; set; }

        public string? ViewDescription { get; set; }

        public bool HasBalcony { get; set; }

        public bool HasGarden { get; set; }

        public bool HasRoof { get; set; }

        public bool IsAccessible { get; set; }


        // Plans / description

        public string? MarketingDescription { get; set; }

        public string? InternalNotes { get; set; }


        public bool IsActive { get; set; } = true;


        // Navigation

        public ICollection<UnitStatusHistory> StatusHistory { get; set; }
            = new List<UnitStatusHistory>();

        public ICollection<UnitPriceHistory> PriceHistory { get; set; }
            = new List<UnitPriceHistory>();

        public ICollection<UnitAssetAssignment> AssetAssignments { get; set; }
            = new List<UnitAssetAssignment>();
    }
}
