using DaData.Domain.Address.Interfaces;
using DaData.Domain.Address;
using Microsoft.Extensions.DependencyInjection;

namespace DaData.Domain.DomainDependencies
{
    public static class DomainDependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<IFullAddressFactory, FullAddressFactory>();

            return services;
        }
    }
}
