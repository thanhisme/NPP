using AutoMapper;
using Entities;
using Infrastructure.Models.RequestModels.Position;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;
using Utils.UnitOfWork.Interfaces;

namespace Infrastructure.Services.Implementations
{
    public class PositionService : BaseService, IPositionService
    {
        private readonly IGenericRepository<Position> _positionRepository;

        private readonly IGenericRepository<Permission> _permissionRepository;

        private readonly Expression<Func<Position, Position>> _defaultProjection =
            (position) => new Position
            {
                Id = position.Id,
                Name = position.Name,
                DefaultPermissions = position.DefaultPermissions,
            };

        public PositionService(
            IUnitOfWork unitOfWork,
            IMemoryCache memoryCache,
            IMapper mapper
        ) : base(unitOfWork, memoryCache, mapper)
        {
            _positionRepository = unitOfWork.Repository<Position>();
            _permissionRepository = unitOfWork.Repository<Permission>();
        }

        public List<Position> GetMany(
            string keyword,
            int page,
            int pageSize,
            Expression<Func<Position, Position>>? projection = null
        )
        {
            return _positionRepository.GetMany(
                page: page,
                pageSize: pageSize,
                projection: projection ?? _defaultProjection
            );
        }

        public Position? GetById(Guid id)
        {
            return _positionRepository.GetOne(
                (permission) => permission.Id == id
            );
        }

        public async Task<Guid?> Create(CreatePositionRequest req)
        {
            var permission = _mapper.Map<Position>(req);

            _positionRepository.Create(permission);
            await _unitOfWork.SaveChangesAsync();

            return permission.Id;
        }

        public async Task<Guid?> AddDefaultPermission(Guid id, AddDefaultPermissionRequest req)
        {
            var position = GetById(id);
            if (position == null)
            {
                return null;
            }

            var permission = _permissionRepository.GetOne(_permission => _permission.Id == req.PermissionId);
            if (permission == null)
            {
                return Guid.Empty;
            }

            if (position.DefaultPermissions.IndexOf(permission) != -1)
            {
                return Guid.Empty;
            }

            position.DefaultPermissions.Add(permission);
            await _unitOfWork.SaveChangesAsync();

            return position.Id;
        }

        public async Task<Guid?> RemoveDefaultPermission(Guid id, AddDefaultPermissionRequest req)
        {
            var position = GetById(id);
            if (position == null)
            {
                return null;
            }

            var permission = _permissionRepository.GetOne(_permission => _permission.Id == req.PermissionId);
            if (permission == null)
            {
                return Guid.Empty;
            }

            if (position.DefaultPermissions.IndexOf(permission) == -1)
            {
                return Guid.Empty;
            }

            position.DefaultPermissions.Remove(permission);
            await _unitOfWork.SaveChangesAsync();

            return position.Id;
        }
    }
}
