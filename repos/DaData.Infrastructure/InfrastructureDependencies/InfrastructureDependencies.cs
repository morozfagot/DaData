using DaData.Domain.Abstractions;
using DaData.Infrastructure.Services.DaData;
using Microsoft.Extensions.DependencyInjection;

namespace DaData.Infrastructure.InfrastructureDependencies
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IDaDataService, DaDataService>();

            return services;
        }
    }
}
