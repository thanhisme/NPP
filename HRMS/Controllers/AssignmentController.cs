using Entities;
using Infrastructure.Models.CommonModels;
using Infrastructure.Models.RequestModels.Assignment;
using Infrastructure.Models.ResponseModels.Assignment;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
        [AllowAnonymous]
        public async Task<ActionResult<HttpResponse<Guid?>>> Create([FromBody] CreateAssignmentRequest req)
        {
            var id = await _assignmentService.Create(req);

            return id == null
                ? throw new HttpExceptionResponse(HttpStatusCode.BadRequest, "Invalid assignee id or project id")
                : SuccessResponse(id);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<HttpResponse<List<AssignmentResponse>>> GetMany([FromQuery] PaginationRequest req)
        {
            var assignments = _assignmentService.GetMany(req.Page, req.PageSize);

            return SuccessResponse(assignments.Count, assignments);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<HttpResponse<Assignment>> GetById(Guid id)
        {
            var assignment = _assignmentService.GetById(id);

            return assignment == null
                ? throw new HttpExceptionResponse(HttpStatusCode.NotFound, HttpExceptionMessages.NOT_FOUND)
                : SuccessResponse(assignment);
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<HttpResponse<Guid?>>> Remove(Guid id)
        {
            var removedId = await _assignmentService.Remove(id);

            return removedId == null
                ? throw new HttpExceptionResponse(HttpStatusCode.NotFound, HttpExceptionMessages.NOT_FOUND)
                : SuccessResponse(removedId);
        }
    }
}
