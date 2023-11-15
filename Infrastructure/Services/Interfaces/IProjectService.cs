using Entities;
using Infrastructure.Models.ResponseModels.Project;

namespace Infrastructure.Services.Interfaces
{
    public interface IProjectService
    {
        public List<ProjectResponse> GetMany(int page, int pageSize);

        public Project? GetById(Guid id);
    }
}
