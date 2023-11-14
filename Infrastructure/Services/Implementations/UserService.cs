using AutoMapper;
using Entities;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Utils.UnitOfWork.Interfaces;

namespace Infrastructure.Services.Implementations
{
    public class UserService : BaseService, IUserService
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserService(
            IUnitOfWork unitOfWork,
            IMemoryCache memoryCache,
            IMapper mapper
        ) : base(unitOfWork, memoryCache, mapper)
        {
            _userRepository = unitOfWork.Repository<User>();
        }

        public User? GetById(Guid id)
        {
            return _userRepository.GetOne(
                (user) => user.Id == id
            );
        }
    }
}
