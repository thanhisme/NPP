using Microsoft.EntityFrameworkCore;

namespace Entities
{
    [Index(nameof(Token), IsUnique = true, Name = "Access_Token")]
    public class BlackListToken
    {
        public Guid Id { get; set; }

        public string? Token { get; set; }

        public DateTime? Expiry { get; set; }
    }
}
