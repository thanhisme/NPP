using Entities;

namespace Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        public User? GetById(Guid id);
    }
}
