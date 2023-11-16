using Entities;
using Infrastructure.Models.RequestModels.Assignment;
using Infrastructure.Models.ResponseModels.Assignment;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Utils.Constants.Strings;
using Utils.HttpResponseModels;

namespace HRMS.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class AssignmentController : BaseController
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpPost]
        public async Task<ActionResult<HttpResponse<Guid?>>> Create([FromBody] CreateAssignmentRequest req)
        {
            var (userId, username) = GetCurrentUser();
            var id = await _assignmentService.Create(req, userId, username);

            return id == null
                ? throw new HttpExceptionResponse(HttpStatusCode.BadRequest, "Invalid assignee id or project id")
                : CreatedResponse(id);
        }

        [HttpGet]
        public ActionResult<HttpResponse<List<AssignmentResponse>>> GetMany([FromQuery] AssignmentFilterRequest req)
        {
            var (totalRecord, assignments) = _assignmentService.GetMany(req);

            return SuccessResponse(totalRecord, assignments);
        }

        [HttpGet("{id}")]
        public ActionResult<HttpResponse<Assignment>> GetById(Guid id)
        {
            var assignment = _assignmentService.GetById(id);

            return assignment == null
                ? throw new HttpExceptionResponse(HttpStatusCode.NotFound, HttpExceptionMessages.NOT_FOUND)
                : SuccessResponse(assignment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HttpResponse<Guid?>>> Update(Guid id, [FromBody] UpdateAssignmentRequest req)
        {
            var (userId, username) = GetCurrentUser();
            var updatedId = await _assignmentService.Update(id, req, userId, username);

            return updatedId == null
                ? throw new HttpExceptionResponse(HttpStatusCode.NotFound, HttpExceptionMessages.NOT_FOUND)
                : SuccessResponse(updatedId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<HttpResponse<Guid?>>> Remove(Guid id)
        {
            var removedId = await _assignmentService.Remove(id);

            return removedId == null
                ? throw new HttpExceptionResponse(HttpStatusCode.NotFound, HttpExceptionMessages.NOT_FOUND)
                : SuccessResponse(removedId);
        }

        private (Guid, string) GetCurrentUser()
        {
            string userId = User.FindFirst(ClaimTypes.Sid)!.Value;
            string username = User.FindFirst(ClaimTypes.Name)!.Value;

            return (Guid.Parse(userId), username);
        }
    }
}
