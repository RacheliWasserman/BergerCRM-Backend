using System.Collections.Generic;
using CRM.DAL.Common;

namespace CRM.DAL.Modules.Leads.Entities
{
    public class LostReason : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Lead> Leads { get; set; } = new List<Lead>();
    }
}
