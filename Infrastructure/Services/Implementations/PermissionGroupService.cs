using AutoMapper;
using Entities;
using Infrastructure.Models.RequestModels.PermissionGroup;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Utils.UnitOfWork.Interfaces;

namespace Infrastructure.Services.Implementations
{
    public class PermissionGroupService : BaseService, IPermissionGroupService
    {
        private readonly IGenericRepository<PermissionGroup> _permissionGroupRepository;

        public PermissionGroupService(
            IUnitOfWork unitOfWork,
            IMemoryCache memoryCache,
            IMapper mapper
        ) : base(unitOfWork, memoryCache, mapper)
        {
            _permissionGroupRepository = unitOfWork.Repository<PermissionGroup>();
        }

        public async Task<Guid> Create(CreatePermissionGroupRequest req)
        {
            var permissionGroup = _mapper.Map<PermissionGroup>(req);

            _permissionGroupRepository.Create(permissionGroup);
            await _unitOfWork.SaveChangesAsync();

            return permissionGroup.Id;
        }

        public PermissionGroup? GetById(Guid id)
        {
            return _permissionGroupRepository.GetOne(
                (permission) => permission.Id == id
            );
        }

        public List<PermissionGroup> GetMany(int page, int pageSize)
        {
            return _permissionGroupRepository.GetMany(page, pageSize);
        }

        public async Task<Guid?> Update(Guid id, UpdatePermissionGroupRequest req)
        {
            var permissionGroup = GetById(id);
            if (permissionGroup == null)
            {
                return null;
            }

            _mapper.Map(req, permissionGroup);
            _permissionGroupRepository.Update(permissionGroup);
            await _unitOfWork.SaveChangesAsync();

            return permissionGroup.Id;
        }
    }
}
