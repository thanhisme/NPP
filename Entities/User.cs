using Microsoft.EntityFrameworkCore;

namespace Entities
{
    [Index(nameof(Email), IsUnique = true, Name = "Username")]
    public class User : BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Tel { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        public DateTime WorkStartDate { get; set; } = DateTime.Now;

        public virtual ICollection<Permission> AdditionalPermissions { get; set; } = new List<Permission>();
    }
}
