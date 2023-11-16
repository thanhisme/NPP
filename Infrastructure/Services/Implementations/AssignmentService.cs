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
                (assignment) => assignment.Id == id && !assignment.IsDeleted
            );
        }

        public async Task<Guid?> Create(CreateAssignmentRequest req, Guid userId, string username)
        {
            var assignment = _mapper.Map<Assignment>(req);

            var assignee = _userRepository.GetOne(user => user.Id == req.AssigneeId);
            var project = _projectRepository.GetOne(project => project.Id == req.ProjectId);

            assignment.Assignee = assignee!.FullName;
            assignment.Project = project!.Name;
            assignment.Creator = username;
            assignment.CreatorId = userId;
            assignment.Modifier = username;
            assignment.ModifierId = userId;


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

        public async Task<Guid?> Update(Guid id, UpdateAssignmentRequest req, Guid userId, string username)
        {
            var assignment = GetById(id);

            if (assignment == null)
            {
                return null;
            }

            _mapper.Map(req, assignment);

            if (req.AssigneeId != null)
            {
                var assignee = _userRepository.GetOne(user => user.Id == req.AssigneeId);
                assignment.Assignee = assignee!.FullName;
            }

            if (req.ProjectId != null)
            {
                var project = _projectRepository.GetOne(project => project.Id == req.ProjectId);
                assignment.Project = project!.Name;
            }

            assignment.Modifier = username;
            assignment.ModifierId = userId;
            assignment.UpdatedDate = DateTime.Now;
            await _unitOfWork.SaveChangesAsync();

            return id;

        }

        public (int, List<AssignmentResponse>) GetMany(AssignmentFilterRequest req)
        {
            var assignmentDbSet = _assignmentRepository.GetQueryableObject();
            var userDbSet = _userRepository.GetQueryableObject();

            var assignments = (from assignment
                               in assignmentDbSet
                               join user in userDbSet on assignment.AssigneeId equals user.Id
                               where !assignment.IsDeleted &&
                                    (req.Project == null || assignment.ProjectId == req.Project) &&
                                    (req.Department == null || user.DepartmentId == req.Department)
                               group assignment
                               by new { assignment.Assignee, assignment.AssigneeId }
                               into userAssignments
                               select new AssignmentResponse
                               {
                                   Assignee = userAssignments.Key.Assignee,
                                   AssigneeId = userAssignments.Key.AssigneeId,
                                   assignments = userAssignments.Select(assignment => new AssignmentItem
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
                                   ).ToList()
                               }).ToList();

            return (
                assignments.Count,
                assignments
                    .Skip((req.Page - 1) * req.PageSize)
                    .Take(req.PageSize)
                    .ToList()
            );
        }
    }
}
