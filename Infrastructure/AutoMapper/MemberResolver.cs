//using AutoMapper;
//using Entities;
//using Infrastructure.Models.RequestModels.Project;
//using Infrastructure.Services.Interfaces;

//namespace Infrastructure.AutoMapper
//{
//    public class MemberResolver : IValueResolver<CreateProjectRequest, Project, IList<User?>>
//    {
//        private readonly IUserService _userService;

//        public MemberResolver(IUserService userService)
//        {
//            _userService = userService;
//        }

//        public IList<User?> Resolve(
//            CreateProjectRequest source,
//            Project _,
//            IList<User?> __,
//            ResolutionContext ___
//        )
//        {
//            var userIds = source.Members;
//            return userIds
//                .Select(permissionId => _userService.GetById(permissionId))
//                .ToList();
//        }
//    }
//}
