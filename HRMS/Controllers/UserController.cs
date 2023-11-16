using Infrastructure.Models.ResponseModels.User;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Utils.HttpResponseModels;

namespace HRMS.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<HttpResponse<List<UserResponse>>> GetMany()
        {
            var users = _userService.GetMany();

            return SuccessResponse(users);
        }
    }
}
