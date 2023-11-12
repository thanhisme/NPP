using System.ComponentModel.DataAnnotations;
using Utils.Annotations.Validation;
using Utils.Constants.Strings;

namespace Infrastructure.Models.RequestModels.Auth
{
    public class SignUpRequest
    {
        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        [StringLength(
            50, MinimumLength = 5,
            ErrorMessage = ValidationErrorMessages.LENGTH_RANGE_ERROR_MESSAGE
        )]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        [ValidEmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        [StringLength(
            16, MinimumLength = 5,
            ErrorMessage = ValidationErrorMessages.LENGTH_RANGE_ERROR_MESSAGE
        )]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        [Compare(
            "Password",
            ErrorMessage = ValidationErrorMessages.CONFIRM_PASSWORD_NOT_MATCH
        )]
        public string PasswordConfirm { get; set; } = string.Empty;

        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        [ValidTel]
        public string Tel { get; set; } = string.Empty;

        public DateTime WorkStartDate { get; set; } = DateTime.Now;
    }
}
