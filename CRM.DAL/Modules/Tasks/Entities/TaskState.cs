using System.Collections.Generic;
using CRM.DAL.Common;

namespace CRM.DAL.Modules.Tasks.Entities
{
    public class TaskState : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
    }
}
