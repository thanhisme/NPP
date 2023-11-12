using System.ComponentModel.DataAnnotations;
using Utils.Constants.Strings;

namespace Utils.Annotations.Validation
{
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false
    )]
    public class ValidUrl : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }

            if (value is string url)
            {
                url = url.Trim();

                if (
                    Uri.TryCreate(url, UriKind.Absolute, out Uri? result) &&
                    result != null &&
                    (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps)
                )
                {
                    return true;
                }
            }

            return false;
        }

        public override string FormatErrorMessage(string _)
        {
            return string.Format(ValidationErrorMessages.INVALID_URL);
        }
    }
}
