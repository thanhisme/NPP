using Entities;
using Infrastructure.Models.RequestModels.PermissionGroup;

namespace Infrastructure.Services.Interfaces
{
    public interface IPermissionGroupService
    {
        public Task<Guid> Create(CreatePermissionGroupRequest req);

        public PermissionGroup? GetById(Guid id);

        public List<PermissionGroup> GetMany(int page, int pageSize);

        public Task<Guid?> Update(Guid id, UpdatePermissionGroupRequest req);
    }
}
