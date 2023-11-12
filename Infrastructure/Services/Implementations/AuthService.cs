using AutoMapper;
using Entities;
using Infrastructure.Models.RequestModels.Auth;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Utils.HelperFuncs;
using Utils.UnitOfWork.Interfaces;

namespace Infrastructure.Services.Implementations
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IGenericRepository<User> _userRepository;

        private readonly IGenericRepository<RefreshToken> _refreshTokenRepository;

        private readonly IGenericRepository<BlackListToken> _blackListTokenRepository;

        private readonly string _secretKey;

        public AuthService(
            IUnitOfWork unitOfWork,
            IMemoryCache memoryCache,
            IMapper mapper,
            IConfiguration configuration
        ) : base(unitOfWork, memoryCache, mapper)
        {
            _userRepository = unitOfWork.Repository<User>();
            _refreshTokenRepository = unitOfWork.Repository<RefreshToken>();
            _blackListTokenRepository = unitOfWork.Repository<BlackListToken>();
            _secretKey = configuration.GetSection("AppSetting:JwtSecretKey").Value!;
        }

        public async Task<Guid> SignUp(SignUpRequest req)
        {
            var user = _mapper.Map<User>(req);

            _userRepository.Create(user);
            await _unitOfWork.SaveChangesAsync();

            return user.Id;
        }

        public async Task<(string, RefreshToken)?> SignIn(
            SignInRequest req,
            string ipAddress
        )
        {
            var hashedPassword = MD5Algorithm.HashMd5(req.Password);
            var user = _userRepository.GetOne(
                (user) => user.Email == req.Username + "@namphuongtech.com" && user.IsDeleted != true,
                (user) => new User
                {
                    Id = user.Id,
                    Password = user.Password,
                    AdditionalPermissions = user.AdditionalPermissions
                }
            );

            if (user == null)
            {
                return null;
            }

            //var isValidEmail = AuthenticateEmail(req.Username, req.Password);

            //if (!isValidEmail)
            //{
            //    return null;
            //}

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(
                    JwtRegisteredClaimNames.Iat,
                    DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64
                )
            };

            var accessToken = Jwt.GenerateToken(claims, _secretKey);
            var refreshToken = GenerateRefreshToken(user, ipAddress);

            await _unitOfWork.SaveChangesAsync();

            return (accessToken, refreshToken);
        }

        public async Task<(string, RefreshToken)?> Refresh2TokenTypes(
        string? refreshToken,
        string ipAddress
    )
        {
            if (refreshToken == null)
            {
                return null;
            }

            var token = _refreshTokenRepository.GetOne(
                (rt) => rt.Token == refreshToken && rt.RevokedAt != null && rt.Expiry > DateTime.Now,
                (rt) => new RefreshToken() { User = rt.User }
            );

            if (token == null)
            {
                return null;
            }

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Sid, token.User!.Id.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var newAccessToken = Jwt.GenerateToken(claims, _secretKey);
            var newRefreshToken = GenerateRefreshToken(token.User, ipAddress);

            await _unitOfWork.SaveChangesAsync();

            return (newAccessToken, newRefreshToken);
        }

        public async Task SignOut(
            string? refreshToken,
            string accessToken,
            string ipAddress
        )
        {
            RevokeRefreshToken(refreshToken!, ipAddress);
            AddAccessToken2BlackList(accessToken);

            await _unitOfWork.SaveChangesAsync();
        }

        public bool IsTokenInBlackList(string token)
        {
            var blToken = _blackListTokenRepository.GetOne(_blToken => _blToken.Token == token);

            if (blToken == null)
            {
                return false;
            }

            return true;
        }

        private void RevokeRefreshToken(string token, string ipAddress)
        {
            var refreshToken = _refreshTokenRepository.GetOne(
                rt => rt.Token == token,
                rt => new RefreshToken() { RevokedAt = rt.RevokedAt, RevokedByIp = rt.RevokedByIp }
            );

            if (refreshToken == null)
            {
                return;
            }

            refreshToken.RevokedAt = DateTime.Now;
            refreshToken.RevokedByIp = ipAddress;
        }

        private RefreshToken GenerateRefreshToken(User user, string ipAddress)
        {
            var refreshToken = new RefreshToken()
            {
                User = user,
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expiry = DateTime.Now.AddMonths(1),
                CreatedByIp = ipAddress
            };

            _refreshTokenRepository.Create(refreshToken);

            return refreshToken;
        }

        private void AddAccessToken2BlackList(string token)
        {
            var blackListToken = new BlackListToken
            {
                Token = token,
                Expiry = Jwt.GetTokenExpiry(token)
            };

            _blackListTokenRepository.Create(blackListToken);
        }

        private static bool AuthenticateEmail(string username, string password)
        {
            return true;
        }
    }
}
