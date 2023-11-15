using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("NPP_Users")]
    [Index(nameof(Email), IsUnique = true, Name = "Username")]
    public class User : BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Tel { get; set; }

        public string Department { get; set; }

        public string DepartmentId { get; set; }

        public DateTime WorkStartDate { get; set; }
    }
}
