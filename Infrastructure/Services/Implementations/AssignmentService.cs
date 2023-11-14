using AutoMapper;
using Entities;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Utils.UnitOfWork.Interfaces;

namespace Infrastructure.Services.Implementations
{
    public class AssignmentService : BaseService, IAssignmentService
    {
        private readonly IGenericRepository<Assignment> _assignmentRepository;

        private readonly IGenericRepository<User> _userRepository;

        public AssignmentService(
            IUnitOfWork unitOfWork,
            IMemoryCache memoryCache,
            IMapper mapper
        ) : base(unitOfWork, memoryCache, mapper)
        {
            _assignmentRepository = unitOfWork.Repository<Assignment>();
            _userRepository = unitOfWork.Repository<User>();
        }

        public Assignment? GetById(Guid id)
        {
            return _assignmentRepository.GetOne(
                (permission) => permission.Id == id
            );
        }

        public List<User> GetMany(int page, int pageSize)
        {
            var users = _userRepository.GetMany(page, pageSize, projection: (user) => new User
            {
                Id = user.Id,
                FullName = user.FullName,
                Department = user.Department,
                Assignments = user.Assignments
                    .Select(assignment => new Assignment
                    {
                        Id = assignment.Id,
                        Description = assignment.Description,
                        Note = assignment.Note,
                        DueDate = assignment.DueDate,
                        StartDate = assignment.StartDate,
                        State = assignment.State,
                        Project = new Project
                        {
                            Id = assignment.Project.Id,
                            Name = assignment.Project.Name,
                            Code = assignment.Project.Code,
                        },
                    }
                    )
                    .ToList(),
            });

            return users;

            //_assignmentRepository.GetMany(
            //    page,
            //    pageSize,
            //    projection: assignment => new Assignment
            //    {
            //        Assignee = assignment.Assignee,
            //    }
            //);
        }
    }
}
