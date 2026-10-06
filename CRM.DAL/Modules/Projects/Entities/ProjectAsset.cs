using System;
using System.Collections.Generic;
using System.Text;

using CRM.DAL.Common;

namespace CRM.DAL.Modules.Projects.Entities
{
    public class ProjectAsset : BaseEntity
    {
        public int ProjectId { get; set; }

        public Project Project { get; set; } = null!;


        public int AssetTypeId { get; set; }

        public AssetType AssetType { get; set; } = null!;


        public int AssetStatusId { get; set; }

        public AssetStatus AssetStatus { get; set; } = null!;


        public string Code { get; set; } = null!;

        public string? Name { get; set; }

        public string? Description { get; set; }


        // Location

        public string? BuildingCode { get; set; }

        public int? FloorNumber { get; set; }

        public string? LocationDescription { get; set; }


        // Details

        public decimal? Area { get; set; }

        public decimal? CurrentPrice { get; set; }


        public bool IsActive { get; set; } = true;


        public ICollection<UnitAssetAssignment> UnitAssignments { get; set; }
            = new List<UnitAssetAssignment>();
    }
}
