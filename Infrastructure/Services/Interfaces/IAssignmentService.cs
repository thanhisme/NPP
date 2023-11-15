using Entities;
using Infrastructure.Models.RequestModels.Assignment;

namespace Infrastructure.Services.Interfaces
{
    public interface IAssignmentService
    {
        public List<User> GetMany(AssignmentFilterRequest req);

        public Assignment? GetById(Guid id);
    }
}
