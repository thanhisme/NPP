using AutoMapper;
using Entities;
using Infrastructure.Models.ResponseModels.User;
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

        public List<UserResponse> GetMany()
        {
            var users = _userRepository.GetMany(
                1,
                int.MaxValue,
                predicate: (user) => !user.IsDeleted
            );

            return users.Select(user => new UserResponse
            {
                Id = user.Id,
                FullName = user.FullName,
                Department = user.Department,
                DepartmentId = user.DepartmentId,
            }).ToList();
        }
    }
}
