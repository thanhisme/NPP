namespace Entities
{
    public class Permission
    {
        public int Id { get; set; }

        public string PermissionName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;
    }
}
