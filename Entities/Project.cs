namespace Entities
{
    public class Project
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime FinishDay { get; set; }

        public List<User> Members { get; set; }

        public string Status { get; set; }
    }
}
