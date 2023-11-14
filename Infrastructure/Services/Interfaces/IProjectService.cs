using Entities;

namespace Infrastructure.Services.Interfaces
{
    public interface IProjectService
    {
        public List<Project> GetMany(int page, int pageSize);

        public Project? GetById(Guid id);
    }
}
