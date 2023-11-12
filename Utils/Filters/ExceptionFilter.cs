using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using Utils.Constants.Strings;
using Utils.HttpResponseModels;

namespace Utils.Filters
{
    public class ExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = NormalizeValidationErrors(context.ModelState);

                throw new HttpExceptionResponse(
                    HttpStatusCode.BadRequest,
                    HttpExceptionMessages.VALIDATION_ERRORS,
                    errors
                );
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                throw context.Exception;
            }
        }

        private static Dictionary<string, List<string>> NormalizeValidationErrors(ModelStateDictionary state)
        {
            var errors = new Dictionary<string, List<string>>();

            // Iterate through ModelState errors to create the key-value mappings
            foreach (var key in state.Keys)
            {
                var errorMessages = state[key]!.Errors
                    .Select(error => error.ErrorMessage)
                    .ToList();

                errors[key] = errorMessages;
            }

            return errors;
        }
    }
}
