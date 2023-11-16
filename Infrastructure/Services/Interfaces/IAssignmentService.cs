using Entities;
using Infrastructure.Models.RequestModels.Assignment;
using Infrastructure.Models.ResponseModels.Assignment;

namespace Infrastructure.Services.Interfaces
{
    public interface IAssignmentService
    {
        public (int, List<AssignmentResponse>) GetMany(AssignmentFilterRequest req);

        public Assignment? GetById(Guid id);

        public Task<Guid?> Create(CreateAssignmentRequest req, Guid userId, string username);

        public Task<Guid?> Update(Guid id, UpdateAssignmentRequest req, Guid userId, string username);

        public Task<Guid?> Remove(Guid id);
    }
}
