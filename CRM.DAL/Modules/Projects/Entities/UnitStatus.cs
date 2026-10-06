using System;
using System.Collections.Generic;
using System.Text;

using CRM.DAL.Common;

namespace CRM.DAL.Modules.Projects.Entities
{
    public class UnitStatus : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Unit> Units { get; set; }
            = new List<Unit>();

        public ICollection<UnitStatusHistory> UnitStatusHistories { get; set; }
            = new List<UnitStatusHistory>();
    }
}
