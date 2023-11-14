using System.ComponentModel.DataAnnotations;
using Utils.Annotations.Validation;
using Utils.Constants.Strings;

namespace Infrastructure.Models.RequestModels.Position
{
    public class CreatePositionRequest
    {
        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        [StringLength(
            50, MinimumLength = 5,
            ErrorMessage = ValidationErrorMessages.LENGTH_RANGE_ERROR_MESSAGE
        )]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        [StringLength(
            50, MinimumLength = 5,
            ErrorMessage = ValidationErrorMessages.LENGTH_RANGE_ERROR_MESSAGE
        )]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        [AtLeastOneElement]
        public IList<Guid> DefaultPermissions { get; set; }
    }
}
