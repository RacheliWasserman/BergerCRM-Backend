using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.DAL.Modules.Identity.Entities
{
    public class RefreshToken
    {
        public long Id { get; set; }


        // User

        public int UserId { get; set; }

        public User User { get; set; } = null!;


        // אנחנו לא נשמור Token חשוף
        // אלא Hash שלו

        public string TokenHash { get; set; } = null!;


        // Dates

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime ExpiresAt { get; set; }

        public DateTime? RevokedAt { get; set; }


        // Token Rotation

        public long? ReplacedByTokenId { get; set; }


        // Security information

        public string? CreatedByIp { get; set; }

        public string? RevokedByIp { get; set; }

        public string? ReasonRevoked { get; set; }
    }
}