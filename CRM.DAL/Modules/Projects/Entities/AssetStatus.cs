using System;
using System.Collections.Generic;
using System.Text;

using CRM.DAL.Common;

namespace CRM.DAL.Modules.Projects.Entities
{
    public class AssetStatus : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<ProjectAsset> ProjectAssets { get; set; }
            = new List<ProjectAsset>();
    }
}
