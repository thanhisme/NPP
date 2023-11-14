using Microsoft.EntityFrameworkCore;

namespace Entities
{
    [Index(nameof(Token), IsUnique = true, Name = "Refresh_Token")]
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public string Token { get; set; }

        public DateTime Expiry { get; set; } = DateTime.UtcNow;

        public string CreatedByIp { get; set; }

        public DateTime? RevokedAt { get; set; }

        public string RevokedByIp { get; set; }

        public virtual User? User { get; set; }
    }
}
