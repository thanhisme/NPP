using Entities;
using Infrastructure.Models.RequestModels.Assignment;
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

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<HttpResponse<List<User>>> GetMany([FromQuery] AssignmentFilterRequest req)
        {
            var assignments = _assignmentService.GetMany(req);

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
    }
}
