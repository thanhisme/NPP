using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using Utils.Constants.Strings;
using Utils.Filters;
using Utils.HttpResponseModels;
using Utils.Swagger;

namespace HRMS
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterAppServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.RegisterInfrastructureServices(configuration);

            services.RegisterFilters();

            services.RegisterAuthentication(configuration);

            services.RegisterSwagger();

            return services;
        }

        private static IServiceCollection RegisterAuthentication(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                                configuration.GetSection("AppSetting:JwtSecretKey").Value!
                            )
                        )
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = (context) =>
                        {
                            context.HandleResponse();

                            throw new HttpExceptionResponse(HttpStatusCode.Unauthorized, HttpExceptionMessages.UNAUTHORIZED);
                        },
                        OnForbidden = (context) =>
                        {
                            throw new HttpExceptionResponse(HttpStatusCode.Forbidden, HttpExceptionMessages.FORBIDDEN);
                        }
                    };
                });

            return services;
        }
    }
}
