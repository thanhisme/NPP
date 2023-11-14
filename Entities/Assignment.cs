
namespace Entities
{
    public class Assignment
    {
        public Guid Id { get; set; }

        public string? Description { get; set; }

        public string? Note { get; set; }

        public string? State { get; set; }

        public User? Assignee { get; set; }

        public Project? Project { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? DueDate { get; set; }


    }
}
