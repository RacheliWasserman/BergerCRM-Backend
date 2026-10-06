using CRM.DAL.Common;
using CRM.DAL.Modules.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.DAL.Modules.Projects.Entities
{
    public class ProjectUser : BaseEntity
    {
        public int ProjectId { get; set; }

        public Project Project { get; set; } = null!;


        public int UserId { get; set; }

        public User User { get; set; } = null!;


        // תפקיד העובד בתוך הפרויקט עצמו
        public string? ProjectRole { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        public DateTime? RemovedAt { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
