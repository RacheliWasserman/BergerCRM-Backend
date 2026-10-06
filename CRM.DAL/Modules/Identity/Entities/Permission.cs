using CRM.DAL.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.DAL.Modules.Identity.Entities
{
    public class Permission : BaseEntity
    {
        public int Id { get; set; }

        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Module { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? CreatedBy { get; set; }


        // Navigation Properties

        public ICollection<RolePermission> RolePermissions { get; set; }
            = new List<RolePermission>();

        public ICollection<UserPermission> UserPermissions { get; set; }
            = new List<UserPermission>();
    }
}
