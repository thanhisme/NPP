using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Utils.HelperFuncs
{
    public class Jwt
    {
        public static string GenerateToken(List<Claim> claims, string secretKey)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddYears(2),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public static DateTime GetTokenExpiry(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var expirationClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp");

            if (expirationClaim != null &&
                long.TryParse(expirationClaim.Value, out long expirationUnixTime)
            )
            {
                return DateTimeOffset.FromUnixTimeSeconds(expirationUnixTime).UtcDateTime;
            }

            return DateTime.UtcNow;
        }
    }
}
