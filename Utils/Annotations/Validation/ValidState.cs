using System.ComponentModel.DataAnnotations;
using Utils.Constants.Strings;

namespace Utils.Annotations.Validation
{
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false
    )]
    public class ValidState : ValidationAttribute
    {
        private readonly string[] _validStates = new string[] { "done", "in-progress" };

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }

            if (value is string state)
            {
                return _validStates.Any(val => val == state);
            }

            return false;
        }
        public override string FormatErrorMessage(string _)
        {
            return string.Format(
                ValidationErrorMessages.INVALID_STATE_ERROR,
                _validStates.Aggregate((state1, state2) => $"{state1}, {state2}")
            );
        }
    }
}
