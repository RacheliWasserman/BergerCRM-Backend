using System;
using System.Collections.Generic;
using System.Text;

using CRM.DAL.Common;

namespace CRM.DAL.Modules.Projects.Entities
{
    public class Floor : BaseEntity
    {
        public int BuildingId { get; set; }

        public Building Building { get; set; } = null!;


        public int FloorNumber { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }


        public int? NumberOfUnits { get; set; }

        public bool IsActive { get; set; } = true;


        public ICollection<Unit> Units { get; set; }
            = new List<Unit>();
    }
}
