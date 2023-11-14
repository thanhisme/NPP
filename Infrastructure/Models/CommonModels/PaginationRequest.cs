using System.ComponentModel.DataAnnotations;
using Utils.Constants.Numbers;
using Utils.Constants.Strings;

namespace Infrastructure.Models.CommonModels
{
    public class PaginationRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorMessages.PAGE_NUMBER_ERROR)]
        public int Page { get; set; } = NumberConstants.PAGE_DEFAULT;

        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorMessages.PAGE_SIZE_ERROR)]
        public int PageSize { get; set; } = NumberConstants.PAGE_SIZE_DEFAULT;
    }
}
