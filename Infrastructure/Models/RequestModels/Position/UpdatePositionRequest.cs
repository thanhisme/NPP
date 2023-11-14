using System.ComponentModel.DataAnnotations;
using Utils.Constants.Strings;

namespace Infrastructure.Models.RequestModels.Position
{
    public class UpdatePositionRequest
    {
        [StringLength(
            50, MinimumLength = 5,
            ErrorMessage = ValidationErrorMessages.LENGTH_RANGE_ERROR_MESSAGE
        )]
        public string? Code { get; set; }

        [StringLength(
            50, MinimumLength = 5,
            ErrorMessage = ValidationErrorMessages.LENGTH_RANGE_ERROR_MESSAGE
        )]
        public string? Name { get; set; }
    }
}
