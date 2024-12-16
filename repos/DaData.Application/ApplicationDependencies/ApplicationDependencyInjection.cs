using Microsoft.Extensions.DependencyInjection;

namespace DaData.Application.ApplicationDependencies
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblies(typeof(ApplicationDependencyInjection).Assembly);
            });

            return services;
        }
    }
}
