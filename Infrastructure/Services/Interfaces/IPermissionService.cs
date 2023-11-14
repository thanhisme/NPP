using Entities;
using Infrastructure.Models.RequestModels.Permission;
using System.Linq.Expressions;

namespace Infrastructure.Services.Interfaces
{
    public interface IPermissionService
    {
        public List<Permission> GetMany(
            string keyword,
            int page,
            int pageSize,
            Expression<Func<Permission, Permission>>? projection = null
        );

        public Permission? GetById(Guid id);

        public Task<Guid?> Create(CreatePermissionRequest req);

        public Task<Guid?> Update(Guid id, UpdatePermissionRequest req);
    }
}
