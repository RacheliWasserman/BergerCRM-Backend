using CRM.DAL.Common;
using CRM.DAL.Modules.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.DAL.Modules.Projects.Entities
{
    public class UnitStatusHistory : BaseEntity
    {
        public int UnitId { get; set; }

        public Unit Unit { get; set; } = null!;


        public int UnitStatusId { get; set; }

        public UnitStatus UnitStatus { get; set; } = null!;


        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        public int? ChangedByUserId { get; set; }

        public User? ChangedByUser { get; set; }


        public string? Reason { get; set; }

        public string? Notes { get; set; }
    }
}
