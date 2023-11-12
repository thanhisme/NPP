using Microsoft.Extensions.DependencyInjection;
using Utils.UnitOfWork.Interfaces;

namespace Utils.UnitOfWork
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, Implementations.UnitOfWork>();

            return services;
        }
    }
}
