using Entities;
using Infrastructure.Models.CommonModels;
using Infrastructure.Models.RequestModels.Position;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utils.HttpResponseModels;

namespace HRMS.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class PositionController : BaseController
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<HttpResponse<Guid>>> Create(CreatePositionRequest req)
        {
            var id = await _positionService.Create(req)
                ?? throw new HttpExceptionResponse(
                        HttpStatusCode.BadRequest,
                        "Permission group not found"
                    );

            return CreatedResponse(id);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<HttpResponse<List<Position>>> GetMany([FromQuery] KeywordWithPaginationRequest req)
        {
            var permissions = _positionService.GetMany(req.Keyword, req.Page, req.PageSize);

            return SuccessResponse(permissions.Count, permissions);
        }

        [HttpPut("{id}/[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<HttpResponse<Guid?>>> AddDefaultPermission(
            Guid id,
            [FromBody] AddDefaultPermissionRequest req
        )
        {
            var result = await _positionService.AddDefaultPermission(id, req);

            if (result == null)
            {
                throw new HttpExceptionResponse(HttpStatusCode.BadRequest, "Id not found");
            }

            if (result == Guid.Empty)
            {
                throw new HttpExceptionResponse(HttpStatusCode.BadRequest, "Invalid permission id");
            }

            return SuccessResponse(result);
        }

        [HttpPut("{id}/[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<HttpResponse<Guid?>>> RemoveDefaultPermission(
            Guid id,
            [FromBody] AddDefaultPermissionRequest req
        )
        {
            var result = await _positionService.RemoveDefaultPermission(id, req);

            if (result == null)
            {
                throw new HttpExceptionResponse(HttpStatusCode.BadRequest, "Id not found");
            }

            if (result == Guid.Empty)
            {
                throw new HttpExceptionResponse(HttpStatusCode.BadRequest, "Invalid permission id");
            }

            return SuccessResponse(result);
        }

    }
}
