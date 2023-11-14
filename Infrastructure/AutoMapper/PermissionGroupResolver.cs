using AutoMapper;
using Entities;
using Infrastructure.Models.RequestModels.Permission;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.AutoMapper
{
    public class PermissionGroupResolver : IValueResolver<CreatePermissionRequest, Permission, PermissionGroup>
    {
        private readonly IPermissionGroupService _permissionGroupService;

        public PermissionGroupResolver(IPermissionGroupService permissionGroupService)
        {
            _permissionGroupService = permissionGroupService;
        }

        public PermissionGroup? Resolve(
            CreatePermissionRequest source,
            Permission _,
            PermissionGroup __,
            ResolutionContext ___
        )
        {
            return _permissionGroupService.GetById(source.Group);
        }
    }
}
