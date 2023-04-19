using JWTAuthenticatonDemo.Application;
using JWTAuthenticatonDemo.Infrastructure;

namespace JWTAuthenticatonDemo.API
{
    public static class ServiceExtension
    {
        public static IServiceCollection AppConfigurations (this IServiceCollection services, IConfiguration config)
        {
            services.AddApplicationLayer();
            services.AddInfrastructure(config);
            return services;
        }
    }
}
