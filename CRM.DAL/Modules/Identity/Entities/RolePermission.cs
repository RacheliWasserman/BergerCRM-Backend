using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.DAL.Modules.Identity.Entities
{
    public class RolePermission
    {
        public int Id { get; set; }


        // Role

        public int RoleId { get; set; }

        public Role Role { get; set; } = null!;


        // Permission

        public int PermissionId { get; set; }

        public Permission Permission { get; set; } = null!;


        // Audit

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? CreatedBy { get; set; }
    }
}
