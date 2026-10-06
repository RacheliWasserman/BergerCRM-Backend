using System;
using System.Collections.Generic;
using System.Text;
using CRM.DAL.Modules.Projects.Entities;
namespace CRM.DAL.Modules.Identity.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string? EmployeeNumber { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string NormalizedEmail { get; set; } = null!;

        public string? Phone { get; set; }

        public string PasswordHash { get; set; } = null!;

        public string? JobTitle { get; set; }


        // =========================
        // Department
        // =========================

        public int? DepartmentId { get; set; }

        public Department? Department { get; set; }


        // =========================
        // Manager
        // =========================

        public int? ManagerUserId { get; set; }

        public User? ManagerUser { get; set; }

        public ICollection<User> ManagedUsers { get; set; } = new List<User>();


        // =========================
        // Profile
        // =========================

        public string? ProfileImageUrl { get; set; }


        // =========================
        // Account Status
        // =========================

        public bool IsActive { get; set; } = true;

        public bool IsLocked { get; set; } = false;

        public DateTime? LockedUntil { get; set; }

        public int FailedLoginAttempts { get; set; } = 0;

        public bool MustChangePassword { get; set; } = false;

        public DateTime? LastLoginAt { get; set; }

        public DateTime? PasswordChangedAt { get; set; }


        // =========================
        // Preferences
        // =========================

        // פרויקט ברירת המחדל של המשתמש
        public int? DefaultProjectId { get; set; }
        public Project? DefaultProject { get; set; }

        public string? PreferredLanguage { get; set; }

        public string? TimeZone { get; set; }


        // =========================
        // Audit
        // =========================

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public bool IsDeleted { get; set; } = false;


        // =========================
        // Navigation Properties
        // =========================

        public ICollection<UserRole> UserRoles { get; set; }
            = new List<UserRole>();

        public ICollection<UserPermission> UserPermissions { get; set; }
            = new List<UserPermission>();

        public ICollection<LoginHistory> LoginHistories { get; set; }
            = new List<LoginHistory>();

        public ICollection<RefreshToken> RefreshTokens { get; set; }
            = new List<RefreshToken>();
    }
}
