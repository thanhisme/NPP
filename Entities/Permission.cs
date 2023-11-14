namespace Entities
{
    public class Permission
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; } = false;

        public virtual PermissionGroup Group { get; set; }

        public virtual List<Position> Positions { get; set; }
    }
}
