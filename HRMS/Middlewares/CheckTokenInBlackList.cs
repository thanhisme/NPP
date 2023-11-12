using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Utils.Constants.Strings;
using Utils.HttpResponseModels;

namespace HRMS.Middlewares
{
    public class CheckTokenInBlackList
    {
        private readonly RequestDelegate _next;

        public CheckTokenInBlackList(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"]
                .FirstOrDefault()
                ?.Replace("Bearer ", "");
            var authService = context.RequestServices.GetService(typeof(IAuthService)) as IAuthService;
            var endpoint = context.GetEndpoint();
            var isAllowAnonymous = endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null;

            if (token != null &&
                authService != null &&
                authService.IsTokenInBlackList(token)
            )
            {
                if (isAllowAnonymous)
                {
                    // remove token since from here, the user is mark to be anonymous
                    context.Request.Headers["Authorization"] = "";
                }
                else if (!isAllowAnonymous)
                {
                    throw new HttpExceptionResponse(
                        HttpStatusCode.Unauthorized,
                        HttpExceptionMessages.TOKEN_IN_BLACKLIST
                    );
                }
            }

            await _next(context);
        }
    }
}
