using AutoMapper;
using Entities;
using Infrastructure.Models.RequestModels.Assignment;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;
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

        public List<User> GetMany(AssignmentFilterRequest req)
        {
            var users = _userRepository.GetMany(
                req.Page,
                req.PageSize,
                predicate: GetUserPredicate(req),
                projection: GetUserProjection(req)
            );

            return users;
        }

        private static Expression<Func<User, bool>> GetUserPredicate(AssignmentFilterRequest req)
        {
            return user =>
                (req.Project == null || user.Projects.Any(project => project.Id == req.Project)) &&
                (req.Department == null || user.Department.Id == req.Department);
        }

        private static Expression<Func<User, User>> GetUserProjection(AssignmentFilterRequest req)
        {
            return user => new User
            {
                Id = user.Id,
                FullName = user.FullName,
                Department = user.Department,
                Assignments = user.Assignments
                    .Where(
                        assignment =>
                            assignment.StartDate >= req.StartDate &&
                            assignment.StartDate <= req.EndDate &&
                            (req.Project == null || assignment.Project.Id == req.Project)
                    )
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
                    })
                    .ToList(),
            };
        }
    }
}
