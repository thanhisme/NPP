using System.ComponentModel.DataAnnotations;
using Utils.Annotations.Validation;
using Utils.Constants.Strings;

namespace Infrastructure.Models.RequestModels.Project
{
    public class CreateProjectRequest
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
        [StringLength(
            50, MinimumLength = 5,
            ErrorMessage = ValidationErrorMessages.LENGTH_RANGE_ERROR_MESSAGE
        )]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        public DateTime DueDate { get; set; } = DateTime.UtcNow;

        public DateTime? FinishDay { get; set; }

        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        [AtLeastOneElement]
        public IList<Guid> Members { get; set; }
    }
}
