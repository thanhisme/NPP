using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Utils.Constants.Numbers;
using Utils.Constants.Strings;

namespace Utils.Annotations.Validation
{
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false
    )]
    public class MaxFileSize : ValidationAttribute
    {
        private int MaxSize { get; }

        public MaxFileSize(int maxSizeInMb = NumberConstants.MAXIMUM_IMAGE_SIZE)
        {
            MaxSize = maxSizeInMb;
        }

        public override bool IsValid(object? value)
        {
            if (value is IFormFile file)
            {
                if (file.Length > 0 && file.Length <= MaxSize * 1024 * 1024)
                {
                    return true;
                }
            }

            return false;
        }
        public override string FormatErrorMessage(string _)
        {
            return string.Format(ValidationErrorMessages.MAX_FILE_SIZE_ERROR, MaxSize);
        }
    }
}
