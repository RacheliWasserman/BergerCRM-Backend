using System;
using System.Collections.Generic;
using System.Text;

using CRM.DAL.Common;

namespace CRM.DAL.Modules.Projects.Entities
{
    public class UnitAssetAssignment : BaseEntity
    {
        public int UnitId { get; set; }

        public Unit Unit { get; set; } = null!;


        public int ProjectAssetId { get; set; }

        public ProjectAsset ProjectAsset { get; set; } = null!;


        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UnassignedAt { get; set; }

        public bool IsPrimary { get; set; } = false;

        public string? Notes { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
