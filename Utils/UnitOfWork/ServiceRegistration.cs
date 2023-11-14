using Microsoft.Extensions.DependencyInjection;
using Utils.UnitOfWork.Interfaces;

namespace Utils.UnitOfWork
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, Implementations.UnitOfWork>();

            return services;
        }
    }
}
