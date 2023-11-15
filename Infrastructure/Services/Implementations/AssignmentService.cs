using AutoMapper;
using Entities;
using Infrastructure.Models.RequestModels.Assignment;
using Infrastructure.Models.ResponseModels.Assignment;
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

        private readonly IGenericRepository<Project> _projectRepository;

        public AssignmentService(
            IUnitOfWork unitOfWork,
            IMemoryCache memoryCache,
            IMapper mapper
        ) : base(unitOfWork, memoryCache, mapper)
        {
            _assignmentRepository = unitOfWork.Repository<Assignment>();
            _userRepository = unitOfWork.Repository<User>();
            _projectRepository = unitOfWork.Repository<Project>();
        }

        public Assignment? GetById(Guid id)
        {
            return _assignmentRepository.GetOne(
                (permission) => permission.Id == id
            );
        }

        public async Task<Guid?> Create(CreateAssignmentRequest req)
        {
            var assignment = _mapper.Map<Assignment>(req);

            var assignee = _userRepository.GetOne(user => user.Id == req.AssigneeId);
            var project = _projectRepository.GetOne(project => project.Id == req.ProjectId);

            assignment.Assignee = assignee!.FullName;
            assignment.Project = project!.Name;
            assignment.Creator = "Quản lí 1";
            assignment.CreatorId = Guid.Parse("CE79DE25-01D4-42E7-969E-38B8E3A11FA9");
            assignment.Modifier = "Quản lí 1";
            assignment.ModifierId = Guid.Parse("CE79DE25-01D4-42E7-969E-38B8E3A11FA9");
            // add createdBy


            _assignmentRepository.Create(assignment);
            await _unitOfWork.SaveChangesAsync();

            return assignment.Id;
        }

        public async Task<Guid?> Remove(Guid id)
        {
            var assignment = _assignmentRepository.GetOne(assignment => assignment.Id == id && !assignment.IsDeleted);

            if (assignment == null)
            {
                return null;
            }

            assignment.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
            // add modifier
            return id;
        }

        public List<AssignmentResponse> GetMany(int page, int pageSize)
        {
            var assignments = _assignmentRepository
                .GetMany(
                    page,
                    pageSize,
                    predicate: assignment => !assignment.IsDeleted,
                    projection: assignment => new Assignment
                    {
                        Id = assignment.Id,
                        Description = assignment.Description,
                        Project = assignment.Project,
                        ProjectId = assignment.ProjectId,
                        State = assignment.State,
                        StartDate = assignment.StartDate,
                        DueDate = assignment.DueDate,
                        FinishDate = assignment.FinishDate,
                        Creator = assignment.Creator,
                        Assignee = assignment.Assignee,
                        AssigneeId = assignment.AssigneeId,
                        CreatorId = assignment.CreatorId,
                        CreatedDate = assignment.CreatedDate
                    }
                )
                .GroupBy(
                    assignment => new { assignment.Assignee, assignment.AssigneeId }
                )
                .Select(group => new AssignmentResponse
                {
                    Assignee = group.Key.Assignee,
                    AssigneeId = group.Key.AssigneeId,
                    assignments = group.Select(assignment => new AssignmentItem
                    {
                        Id = assignment.Id,
                        Description = assignment.Description,
                        Project = assignment.Project,
                        ProjectId = assignment.ProjectId,
                        State = assignment.State,
                        StartDate = assignment.StartDate,
                        DueDate = assignment.DueDate,
                        FinishDate = assignment.FinishDate,
                        Creator = assignment.Creator,
                        CreatorId = assignment.CreatorId,
                        CreatedDate = assignment.CreatedDate
                    }
                    ).ToList(),
                }
                )
                .ToList();

            return assignments;
        }
    }
}
