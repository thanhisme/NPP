using Microsoft.EntityFrameworkCore;

namespace Entities
{
    [Index(nameof(Code), IsUnique = true, Name = "Position_Code")]
    public class Position
    {
        public Guid Id { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public virtual IList<Permission> DefaultPermissions { get; set; } = new List<Permission>();
    }
}
