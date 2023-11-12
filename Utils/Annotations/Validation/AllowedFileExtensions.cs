using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Utils.Constants.Strings;

namespace Utils.Annotations.Validation
{
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false
    )]
    public class AllowedFileExtensions : ValidationAttribute
    {
        private string[] AllowedExtensions { get; }

        public AllowedFileExtensions(string[] allowedExtensions)
        {
            AllowedExtensions = allowedExtensions;
        }

        public override bool IsValid(object? value)
        {
            if (value is IFormFile file)
            {
                var fileExtension = Path.GetExtension(file.FileName).ToLower()[1..];
                return AllowedExtensions.Any(ext => ext.ToLower() == fileExtension);
            }

            return false;
        }
        public override string FormatErrorMessage(string _)
        {
            return string.Format(
                ValidationErrorMessages.ALLOWED_FILE_EXTENSIONS_ERROR,
                AllowedExtensions.Aggregate((ext1, ext2) => $"{ext1}, {ext2}")
            );
        }
    }
}
