using System.ComponentModel.DataAnnotations;
using Utils.Constants.Strings;

namespace Infrastructure.Models.RequestModels.Assignment
{
    public class CreateAssignmentRequest
    {
        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        [StringLength(
            500, MinimumLength = 5,
            ErrorMessage = ValidationErrorMessages.LENGTH_RANGE_ERROR_MESSAGE
        )]
        public string Description { get; set; }

        [StringLength(
            500, MinimumLength = 5,
            ErrorMessage = ValidationErrorMessages.LENGTH_RANGE_ERROR_MESSAGE
        )]
        public string? Note { get; set; }

        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        public Guid AssigneeId { get; set; }

        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        public Guid ProjectId { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = ValidationErrorMessages.FIELD_REQUIRED_ERROR_MESSAGE)]
        public DateTime DueDate { get; set; }
    }
}
