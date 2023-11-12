using AutoMapper;
using Entities;
using Infrastructure.Models.RequestModels.Auth;

namespace Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        private static readonly Func<object, object, object, bool> _skipNullProps =
            (_, __, srcMember) => srcMember != null;

        public AutoMapperProfile()
        {
            CreateMap<SignUpRequest, User>().ForAllMembers(
                opts => opts.Condition(_skipNullProps)
            );
        }
    }
}
