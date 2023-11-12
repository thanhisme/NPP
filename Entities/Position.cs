using Utils.Constants.Enums;

namespace Entities
{
    public class Position
    {
        public int Id { get; set; }

        public Positions Name { get; set; } = Positions.PERSONNEL;

        public virtual ICollection<Permission> DefaultPermissions { get; set; } = new List<Permission>();
    }
}
