using Microsoft.EntityFrameworkCore;

namespace Entities
{
    [Index(nameof(Token), IsUnique = true, Name = "Refresh_Token")]
    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime Expiry { get; set; } = DateTime.UtcNow;

        public string CreatedByIp { get; set; } = string.Empty;

        public DateTime? RevokedAt { get; set; }

        public string RevokedByIp { get; set; } = string.Empty;

        public virtual User? User { get; set; }
    }
}
