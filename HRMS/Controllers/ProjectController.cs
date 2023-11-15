using Entities;
using Infrastructure.Models.CommonModels;
using Infrastructure.Models.ResponseModels.Project;
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
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<HttpResponse<List<ProjectResponse>>> GetMany([FromQuery] PaginationRequest req)
        {
            var permissionGroups = _projectService.GetMany(req.Page, req.PageSize);

            return SuccessResponse(permissionGroups.Count, permissionGroups);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<HttpResponse<Project>> GetById(Guid id)
        {
            var permissionGroup = _projectService.GetById(id);

            return permissionGroup == null
                ? throw new HttpExceptionResponse(HttpStatusCode.NotFound, HttpExceptionMessages.NOT_FOUND)
                : SuccessResponse(permissionGroup);
        }
    }
}
