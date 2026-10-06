using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.DAL.Modules.Identity.Entities
{
    public class LoginHistory
    {
        public long Id { get; set; }


        // יכול להיות NULL במקרה שמישהו ניסה
        // להתחבר עם Email שלא קיים

        public int? UserId { get; set; }

        public User? User { get; set; }


        // Email שהוזן בזמן ניסיון ההתחברות

        public string? EmailAttempted { get; set; }


        // Login information

        public DateTime LoginAt { get; set; } = DateTime.UtcNow;

        public DateTime? LogoutAt { get; set; }


        // Client information

        public string? IpAddress { get; set; }

        public string? UserAgent { get; set; }

        public string? Device { get; set; }

        public string? Browser { get; set; }

        public string? OperatingSystem { get; set; }


        // Result

        public bool WasSuccessful { get; set; }

        public string? FailureReason { get; set; }
    }
}
