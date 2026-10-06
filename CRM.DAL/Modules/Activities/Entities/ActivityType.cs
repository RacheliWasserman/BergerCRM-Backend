using System.Collections.Generic;
using CRM.DAL.Common;

namespace CRM.DAL.Modules.Activities.Entities
{
    public class ActivityType : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}
