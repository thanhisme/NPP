using AutoMapper;
using Entities;
using Infrastructure.Models.ResponseModels.Project;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Utils.UnitOfWork.Interfaces;

namespace Infrastructure.Services.Implementations
{
    public class ProjectService : BaseService, IProjectService
    {
        private readonly IGenericRepository<Project> _projectRepository;

        public ProjectService(
            IUnitOfWork unitOfWork,
            IMemoryCache memoryCache,
            IMapper mapper
        ) : base(unitOfWork, memoryCache, mapper)
        {
            _projectRepository = unitOfWork.Repository<Project>();
        }

        public Project? GetById(Guid id)
        {
            return _projectRepository.GetOne(
                (project) => project.Id == id
            );
        }

        public List<ProjectResponse> GetMany(int page, int pageSize)
        {
            var projects = _projectRepository.GetMany(page, pageSize);

            return projects
                .Select(project => new ProjectResponse
                {
                    Id = project.Id,
                    Name = project.Name,
                    Code = project.Code,
                }
                )
                .ToList();
        }
    }
}
