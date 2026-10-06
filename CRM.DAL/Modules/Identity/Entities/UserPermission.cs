using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.DAL.Modules.Identity.Entities
{
    public class UserPermission
    {
        public int Id { get; set; }


        // User

        public int UserId { get; set; }

        public User User { get; set; } = null!;


        // Permission

        public int PermissionId { get; set; }

        public Permission Permission { get; set; } = null!;


        // true  = נותן הרשאה
        // false = חוסם הרשאה

        public bool IsAllowed { get; set; }


        // הסיבה להרשאה / חסימה

        public string? Reason { get; set; }


        // Audit

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? CreatedBy { get; set; }


        // הרשאה יכולה להיות זמנית

        public DateTime? ExpiresAt { get; set; }
    }
}
