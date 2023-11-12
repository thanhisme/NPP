using Entities;
using Infrastructure.Models.RequestModels.Auth;

namespace Infrastructure.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<Guid> SignUp(SignUpRequest req);

        public Task<(string, RefreshToken)?> SignIn(SignInRequest req, string ipAddress);

        public Task<(string, RefreshToken)?> Refresh2TokenTypes(string? refreshToken, string ipAddress);

        public Task SignOut(string? refreshToken, string accessToken, string ipAddress);

        public bool IsTokenInBlackList(string token);
    }
}
