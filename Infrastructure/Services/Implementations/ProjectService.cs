using AutoMapper;
using Entities;
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

        public List<Project> GetMany(int page, int pageSize)
        {
            return _projectRepository.GetMany(
                page,
                pageSize
            //projection: project => new Project()
            //{
            //    Id = project.Id,
            //    Code = project.Code,
            //    Name = project.Name,
            //    Members = project.Members
            //        .Select(member => new User
            //        {
            //            Id = member.Id,
            //            FullName = member.FullName,
            //        }
            //        )
            //        .ToList()
            //}
            );
        }
    }
}
