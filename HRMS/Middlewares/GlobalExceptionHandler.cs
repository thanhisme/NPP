using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Utils.Constants.Strings;
using Utils.Converters;
using Utils.HttpResponseModels;

namespace HRMS.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _environment;

        public GlobalExceptionHandler(RequestDelegate next, IWebHostEnvironment environment)
        {
            _next = next;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpExceptionResponse ex)
            {
                await SendError(context, ex);
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;

                if (innerException is SqlException sqlException)
                {
                    var errors = HandleSqlException(sqlException);
                    var exceptionResult = new HttpExceptionResponse(
                        HttpStatusCode.BadRequest,
                        HttpExceptionMessages.CONTRAINT_ERRORS,
                        errors
                    );

                    await SendError(context, exceptionResult);
                }
                else
                {
                    throw;
                }
            }
            catch (Exception _)
            {
                var exceptionResult = new HttpExceptionResponse(
                    HttpStatusCode.InternalServerError,
                    HttpExceptionMessages.INTERNAL_SERVER_ERROR
                );

                await SendError(context, exceptionResult);
            }
        }

        private Dictionary<string, List<string>> HandleSqlException(SqlException sqlException)
        {
            var errors = new Dictionary<string, List<string>>();

            foreach (SqlError error in sqlException.Errors)
            {
                string duplicateConstraint = ExtractWithPattern(error.Message, @"unique index '([^']*)'");
                string duplicateValue = ExtractWithPattern(error.Message, @"\(([^)]*)\)");

                if (error.Number == 2601)
                {
                    if (!errors.ContainsKey(duplicateConstraint))
                    {
                        errors[duplicateConstraint] = new List<string>();
                    }

                    errors[duplicateConstraint].Add(
                        string.Format(
                            ValidationErrorMessages.UNIQUE_CONTRAINT_ERROR,
                            duplicateValue
                        )
                    );
                }
            }

            return errors;
        }

        private async Task SendError(HttpContext context, HttpExceptionResponse exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exception.StatusCode;

            var isDevEnv = _environment.EnvironmentName == "Development";
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };

            options.Converters.Add(new IgnoreEmptyStringConverter());
            var exceptionResult = JsonSerializer.Serialize(
                new
                {
                    statusCode = exception.StatusCode,
                    message = exception.Message,
                    errors = exception.Errors,
                    stackTrace = isDevEnv ? exception.StackTrace : null
                },
                options
            );

            await context.Response.WriteAsync(exceptionResult);
            return;
        }

        private static string ExtractWithPattern(string srcString, string pattern)
        {
            Match match = Regex.Match(srcString, pattern);

            if (match.Success)
            {
                string value = match.Groups[1].Value;
                return value;
            }

            return string.Empty;
        }
    }
}
