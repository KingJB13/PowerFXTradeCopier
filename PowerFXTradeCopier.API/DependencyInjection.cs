using PowerFXTradeCopier.Application;
using PowerFXTradeCopier.Infrastructure;

namespace PowerFXTradeCopier.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAPIDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDependencyInjection()
                    .AddInfrastructureDependencyInjection(configuration);
            return services;
        }
    }
}
