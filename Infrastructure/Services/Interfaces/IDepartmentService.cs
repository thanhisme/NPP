using Entities;
using Infrastructure.Models.RequestModels.PermissionGroup;

namespace Infrastructure.Services.Interfaces
{
    public interface IDepartmentService
    {
        public Task<Guid> Create(CreateDepartmentRequest req);

        public Department? GetById(Guid id);

        public List<Department> GetMany(int page, int pageSize);
    }
}
