using AutoMapper;
using Entities;
using Infrastructure.Models.RequestModels.Permission;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;
using Utils.UnitOfWork.Interfaces;

namespace Infrastructure.Services.Implementations
{
    public class PermissionService : BaseService, IPermissionService
    {
        private readonly IGenericRepository<Permission> _permissionRepository;

        private readonly Expression<Func<Permission, Permission>> _defaultProjection =
            (permission) => new Permission
            {
                Id = permission.Id,
                Group = permission.Group,
                Name = permission.Name,
                //Description = permission.Description,
            };

        public PermissionService(
            IUnitOfWork unitOfWork,
            IMemoryCache memoryCache,
            IMapper mapper
        ) : base(unitOfWork, memoryCache, mapper)
        {
            _permissionRepository = unitOfWork.Repository<Permission>();
        }

        public List<Permission> GetMany(
            string keyword,
            int page,
            int pageSize,
            Expression<Func<Permission, Permission>>? projection = null
        )
        {
            return _permissionRepository.GetMany(
                predicate: (permission) => permission.Description.Contains(keyword) && !permission.IsDeleted,
                page: page,
                pageSize: pageSize,
                projection: projection ?? _defaultProjection
            );
        }

        public Permission? GetById(Guid id)
        {
            return _permissionRepository.GetOne(
                (permission) => permission.Id == id && !permission.IsDeleted
            );
        }

        public async Task<Guid?> Create(CreatePermissionRequest req)
        {
            var permission = _mapper.Map<Permission>(req);

            _permissionRepository.Create(permission);
            await _unitOfWork.SaveChangesAsync();

            return permission.Id;
        }

        public async Task<Guid?> Update(Guid id, UpdatePermissionRequest req)
        {
            var permission = GetById(id);
            if (permission == null)
            {
                return null;
            }

            _mapper.Map(req, permission);
            if (permission.Group == null)
            {
                return null;
            }

            _permissionRepository.Update(permission);
            await _unitOfWork.SaveChangesAsync();

            return permission.Id;
        }
    }
}
