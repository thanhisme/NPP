using Entities;
using Infrastructure.Models.CommonModels;
using Infrastructure.Models.RequestModels.PermissionGroup;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utils.Constants.Strings;
using Utils.HttpResponseModels;

namespace HRMS.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost]
        public async Task<ActionResult<HttpResponse<Guid>>> Create(CreateDepartmentRequest req)
        {
            var id = await _departmentService.Create(req);

            return CreatedResponse(id);
        }

        [HttpGet]
        public ActionResult<HttpResponse<List<Department>>> GetMany([FromQuery] PaginationRequest req)
        {
            var permissionGroups = _departmentService.GetMany(req.Page, req.PageSize);

            return SuccessResponse(permissionGroups.Count, permissionGroups);
        }

        [HttpGet("{id}")]
        public ActionResult<HttpResponse<Department>> GetById(Guid id)
        {
            var permissionGroup = _departmentService.GetById(id);

            return permissionGroup == null
                ? throw new HttpExceptionResponse(HttpStatusCode.NotFound, HttpExceptionMessages.NOT_FOUND)
                : SuccessResponse(permissionGroup);
        }
    }
}
