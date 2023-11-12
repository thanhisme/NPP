using System.ComponentModel.DataAnnotations;
using Utils.Constants.Strings;

namespace Utils.Annotations.Validation
{
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false
    )]
    public class ValidEmailAddress : ValidationAttribute
    {
        private readonly string[] _allowedDomains = new string[] { "namphuongtech.com" };

        public override bool IsValid(object? value)
        {
            if (value is string email)
            {
                if (!new EmailAddressAttribute().IsValid(email))
                {
                    return false;
                }

                string domain = email.Split('@').Last();

                return _allowedDomains.Any(
                    allowedDomain => string.Equals(domain, allowedDomain)
                );
            }
            return false;
        }

        public override string FormatErrorMessage(string _)
        {
            return string.Format(ValidationErrorMessages.NOT_ALLOWED_EMAIL);
        }
    }
}
