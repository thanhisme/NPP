using AutoMapper;
using Entities;
using Infrastructure.Models.RequestModels.Assignment;
using Infrastructure.Models.RequestModels.Auth;
using Infrastructure.Models.RequestModels.Permission;
using Infrastructure.Models.RequestModels.PermissionGroup;
using Infrastructure.Models.RequestModels.Position;

namespace Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        private static readonly Func<object, object, object, bool> _skipNullProps =
            (_, __, srcMember) => srcMember != null;

        public AutoMapperProfile()
        {
            CreateMap<SignUpRequest, User>().ForAllMembers(opts => opts.Condition(_skipNullProps));

            /*****************************************************************************************************************/
            CreateMap<CreatePermissionRequest, Permission>()
                .ForMember((dest) => dest.Group, opt => opt.MapFrom<PermissionGroupResolver>())
                .ForAllMembers(opts => opts.Condition(_skipNullProps));

            CreateMap<UpdatePermissionRequest, Permission>().ForAllMembers(opts => opts.Condition(_skipNullProps));
            /*****************************************************************************************************************/

            /*****************************************************************************************************************/
            CreateMap<CreatePositionRequest, Position>()
                .ForMember((dest) => dest.DefaultPermissions, opt => opt.MapFrom<ListPermissionsResolver>())
                .ForAllMembers(opts => opts.Condition(_skipNullProps));

            /*****************************************************************************************************************/

            /*****************************************************************************************************************/
            CreateMap<CreatePermissionGroupRequest, PermissionGroup>().ForAllMembers(opts => opts.Condition(_skipNullProps));

            CreateMap<UpdatePermissionGroupRequest, PermissionGroup>().ForAllMembers(opts => opts.Condition(_skipNullProps));
            /*****************************************************************************************************************/

            /*****************************************************************************************************************/
            CreateMap<CreateDepartmentRequest, Department>().ForAllMembers(opts => opts.Condition(_skipNullProps));
            /*****************************************************************************************************************/
            //CreateMap<CreateProjectRequest, Project>()
            //    .ForMember((dest) => dest.Members, opt => opt.MapFrom<MemberResolver>())
            //    .ForAllMembers(opts => opts.Condition(_skipNullProps));
            CreateMap<UpdateAssignmentRequest, Assignment>().ForAllMembers(opts => opts.Condition(_skipNullProps));

            CreateMap<CreateAssignmentRequest, Assignment>().ForAllMembers(opts => opts.Condition(_skipNullProps));
        }
    }
}
