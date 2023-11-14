using Entities;

namespace Infrastructure.Services.Interfaces
{
    public interface IAssignmentService
    {
        public List<User> GetMany(int page, int pageSize);

        public Assignment? GetById(Guid id);
    }
}
