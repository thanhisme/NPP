using System.ComponentModel.DataAnnotations;
using Utils.Constants.Strings;

namespace Utils.Annotations.Validation
{
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false
    )]
    public class ValidTel : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return false;
            }

            if (value is string tel)
            {
                if (tel.Length != 10)
                {
                    return false;
                }

                foreach (char c in tel)
                {
                    if (!char.IsDigit(c) && c != '-' && c != '(' && c != ')')
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override string FormatErrorMessage(string _)
        {
            return string.Format(ValidationErrorMessages.INVALID_TEL);
        }
    }
}
