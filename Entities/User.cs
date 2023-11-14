using Microsoft.EntityFrameworkCore;

namespace Entities
{
    [Index(nameof(Email), IsUnique = true, Name = "Username")]
    public class User : BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }

        public string Tel { get; set; }

        public bool IsDeleted { get; set; }

        public Position Position { get; set; }

        public Department Department { get; set; }

        public DateTime WorkStartDate { get; set; }

        public virtual IList<Permission> AdditionalPermissions { get; set; }

        public virtual IList<Project> Projects { get; set; }

        public virtual IList<Assignment> Assignments { get; set; }
    }
}
