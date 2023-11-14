using Entities;
using Infrastructure.Models.RequestModels.Position;
using System.Linq.Expressions;

namespace Infrastructure.Services.Interfaces
{
    public interface IPositionService
    {
        public List<Position> GetMany(
            string keyword,
            int page,
            int pageSize,
            Expression<Func<Position, Position>>? projection = null
        );

        public Position? GetById(Guid id);

        public Task<Guid?> Create(CreatePositionRequest req);

        public Task<Guid?> AddDefaultPermission(Guid id, AddDefaultPermissionRequest req);

        public Task<Guid?> RemoveDefaultPermission(Guid id, AddDefaultPermissionRequest req);
    }
}
