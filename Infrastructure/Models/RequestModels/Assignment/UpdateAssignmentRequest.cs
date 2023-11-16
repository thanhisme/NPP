using System.ComponentModel.DataAnnotations;
using Utils.Annotations.Validation;
using Utils.Constants.Strings;

namespace Infrastructure.Models.RequestModels.Assignment
{
    public class UpdateAssignmentRequest
    {
        [StringLength(
            500, MinimumLength = 5,
            ErrorMessage = ValidationErrorMessages.LENGTH_RANGE_ERROR_MESSAGE
        )]
        public string? Description { get; set; }

        [StringLength(
            500, MinimumLength = 5,
            ErrorMessage = ValidationErrorMessages.LENGTH_RANGE_ERROR_MESSAGE
        )]
        public string? Note { get; set; }

        public Guid? AssigneeId { get; set; }

        public Guid? ProjectId { get; set; }

        public DateTime? StartDate { get; set; } = DateTime.Now;

        public DateTime? DueDate { get; set; }

        public DateTime? FinishDate { get; set; }

        [ValidState]
        public string? State { get; set; }
    }
}
