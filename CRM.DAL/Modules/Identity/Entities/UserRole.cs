
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.DAL.Modules.Identity.Entities
{
        public class UserRole
        {
            public int Id { get; set; }


            // User

            public int UserId { get; set; }

            public User User { get; set; } = null!;


            // Role

            public int RoleId { get; set; }

            public Role Role { get; set; } = null!;


            // Assignment information

            public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

            public int? AssignedBy { get; set; }

            public DateTime? ExpiresAt { get; set; }

            public bool IsActive { get; set; } = true;
        }
}
