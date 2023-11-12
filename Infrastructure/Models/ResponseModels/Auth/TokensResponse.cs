namespace Infrastructure.Models.ResponseModels.Auth
{
    public class TokensResponse
    {
        public string AccessToken { get; set; } = string.Empty;

        public string? RefreshToken { get; set; }
    }
}
