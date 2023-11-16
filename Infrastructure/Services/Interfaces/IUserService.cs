using Entities;
using Infrastructure.Models.ResponseModels.User;

namespace Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        public User? GetById(Guid id);

        public List<UserResponse> GetMany();
    }
}
