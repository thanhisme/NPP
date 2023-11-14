using Entities;
using Infrastructure.Models.CommonModels;
using Infrastructure.Models.RequestModels.Permission;
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
    public class PermissionController : BaseController
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<HttpResponse<Guid?>>> Create(CreatePermissionRequest req)
        {
            var id = await _permissionService.Create(req);

            return id == null
                ? throw new HttpExceptionResponse(
                    HttpStatusCode.BadRequest,
                    "Permission group not found"
                )
                : CreatedResponse(id);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<HttpResponse<List<Permission>>> GetMany([FromQuery] KeywordWithPaginationRequest req)
        {
            var permissions = _permissionService.GetMany(req.Keyword, req.Page, req.PageSize);

            return SuccessResponse(permissions.Count, permissions);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<HttpResponse<Permission>> GetById(Guid id)
        {
            var permission = _permissionService.GetById(id);

            return permission == null
                ? throw new HttpExceptionResponse(HttpStatusCode.NotFound, HttpExceptionMessages.NOT_FOUND)
                : SuccessResponse(permission);
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<HttpResponse<Guid?>>> Update(Guid id, [FromBody] UpdatePermissionRequest req)
        {
            var result = await _permissionService.Update(id, req);

            return result == null
                ? throw new HttpExceptionResponse(
                    HttpStatusCode.BadRequest,
                    HttpExceptionMessages.NOT_FOUND
                )
                : SuccessResponse(result);
        }
    }
}
