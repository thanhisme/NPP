using Azure.Storage.Blobs;
using Entities;
using Infrastructure.AutoMapper;
using Infrastructure.Services.Implementations;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Utils.UnitOfWork;


namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddScoped(_ => new BlobServiceClient(
                configuration.GetConnectionString("BlobStorage")
            ));

            services.AddDbContext<HRMSContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("SqlServer")
                )
            );

            services.AddTransient<DbContext, HRMSContext>();

            services.AddMemoryCache();

            services.RegisterUnitOfWork();

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            services.RegisterAppServices();

            return services;
        }

        private static IServiceCollection RegisterAppServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            //services.AddTransient<IFileService, FileService>();

            return services;
        }
    }
}
