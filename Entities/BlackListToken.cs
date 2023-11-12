using Microsoft.EntityFrameworkCore;

namespace Entities
{
    [Index(nameof(Token), IsUnique = true, Name = "Access_Token")]
    public class BlackListToken
    {
        public int Id { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime Expiry { get; set; }
    }
}
