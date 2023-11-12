using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using Utils.Transformers;

namespace Utils.Filters
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterFilters(this IServiceCollection services)
        {
            services
                .AddControllers(
                    options =>
                    {
                        options.Conventions.Add(new RouteTokenTransformerConvention(new UrlTransformer()));
                        options.Filters.Add(new AuthorizeFilter());
                        options.Filters.Add<ExceptionFilter>();
                    }
                )
                .ConfigureApiBehaviorOptions(
                    options => options.SuppressModelStateInvalidFilter = true
                )
                .AddJsonOptions(
                    options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                );

            return services;
        }
    }
}
