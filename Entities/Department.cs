using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("NPP_Departments")]
    public class Department
    {
        public Guid Id { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }
    }
}
