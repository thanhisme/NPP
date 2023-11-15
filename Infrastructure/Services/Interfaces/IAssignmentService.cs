using Entities;
using Infrastructure.Models.RequestModels.Assignment;
using Infrastructure.Models.ResponseModels.Assignment;

namespace Infrastructure.Services.Interfaces
{
    public interface IAssignmentService
    {
        public List<AssignmentResponse> GetMany(int page, int pageSize);

        public Assignment? GetById(Guid id);

        public Task<Guid?> Create(CreateAssignmentRequest req);

        public Task<Guid?> Remove(Guid id);
    }
}
