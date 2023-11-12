using System.ComponentModel.DataAnnotations;
using Utils.Constants.Strings;

namespace Infrastructure.Models.RequestModels.Auth
{
    public class SignInRequest
    {
        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        public string Password { get; set; } = string.Empty;
    }
}
