using Entities;
using Infrastructure.Models.CommonModels;
using Infrastructure.Models.RequestModels.PermissionGroup;
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
    public class PermissionGroupController : BaseController
    {
        private readonly IPermissionGroupService _permissionGroupService;

        public PermissionGroupController(IPermissionGroupService permissionGroupService)
        {
            _permissionGroupService = permissionGroupService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<HttpResponse<Guid>>> Create(CreatePermissionGroupRequest req)
        {
            var id = await _permissionGroupService.Create(req);

            return CreatedResponse(id);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<HttpResponse<List<PermissionGroup>>> GetMany([FromQuery] PaginationRequest req)
        {
            var permissionGroups = _permissionGroupService.GetMany(req.Page, req.PageSize);

            return SuccessResponse(permissionGroups.Count, permissionGroups);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<HttpResponse<PermissionGroup>> GetById(Guid id)
        {
            var permissionGroup = _permissionGroupService.GetById(id);

            return permissionGroup == null
                ? throw new HttpExceptionResponse(HttpStatusCode.NotFound, HttpExceptionMessages.NOT_FOUND)
                : SuccessResponse(permissionGroup);
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<HttpResponse<Guid?>>> Update(Guid id, [FromBody] UpdatePermissionGroupRequest req)
        {
            var result = await _permissionGroupService.Update(id, req);

            return result == null
                ? throw new HttpExceptionResponse(
                    HttpStatusCode.BadRequest,
                    HttpExceptionMessages.NOT_FOUND
                )
                : SuccessResponse(result);
        }
    }
}
