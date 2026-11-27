using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerFXTradeCopier.Application.Abstractions.Caching;
using PowerFXTradeCopier.Application.Abstractions.Messaging;
using PowerFXTradeCopier.Infrastructure.Caching;
using PowerFXTradeCopier.Infrastructure.Notifier;

namespace PowerFXTradeCopier.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration =
                    configuration.GetConnectionString("Redis");

                options.InstanceName = "PowerFXTradeCopier:";
            });
            services.AddSingleton<ICacheService, CacheService>();
            services.AddScoped<IRealTimeNotifier, SignalRealTimeNotifier>();
            return services;
        }
    }
}
