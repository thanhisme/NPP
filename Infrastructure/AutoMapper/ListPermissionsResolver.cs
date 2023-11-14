using AutoMapper;
using Entities;
using Infrastructure.Models.RequestModels.Position;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.AutoMapper
{
    public class ListPermissionsResolver : IValueResolver<CreatePositionRequest, Position, IList<Permission?>>
    {
        private readonly IPermissionService _permissionService;

        public ListPermissionsResolver(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public IList<Permission?> Resolve(
            CreatePositionRequest source,
            Position _,
            IList<Permission?> __,
            ResolutionContext ___
        )
        {
            var permissionIds = source.DefaultPermissions;

            return permissionIds
                .Select(permissionId => _permissionService.GetById(permissionId))
                .ToList();
        }
    }
}
