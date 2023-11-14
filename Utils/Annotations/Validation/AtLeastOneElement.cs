using System.Collections;
using System.ComponentModel.DataAnnotations;
using Utils.Constants.Strings;

namespace Utils.Annotations.Validation
{
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false
    )]
    public class AtLeastOneElement : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is IEnumerable enumerable && enumerable.GetEnumerator().MoveNext())
            {
                return true;
            }

            return false;
        }

        public override string FormatErrorMessage(string _)
        {
            return string.Format(ValidationErrorMessages.EMPTY_ARRAY_ERROR);
        }
    }
}
