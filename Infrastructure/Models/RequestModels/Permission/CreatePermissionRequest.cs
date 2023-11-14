using System.ComponentModel.DataAnnotations;
using Utils.Constants.Strings;

namespace Infrastructure.Models.RequestModels.Permission
{
    public class CreatePermissionRequest
    {
        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        [StringLength(
            50, MinimumLength = 5,
            ErrorMessage = ValidationErrorMessages.LENGTH_RANGE_ERROR_MESSAGE
        )]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        [StringLength(
            50, MinimumLength = 5,
            ErrorMessage = ValidationErrorMessages.LENGTH_RANGE_ERROR_MESSAGE
        )]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        public Guid Group { get; set; }
    }
}
