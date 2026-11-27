using Microsoft.Extensions.DependencyInjection;
using PowerFXTradeCopier.Application.Common.Behaviors;
using System;
using FluentValidation;
using System.Reflection;
namespace PowerFXTradeCopier.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencyInjection(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);

            configuration.AddOpenBehavior(
                typeof(ValidationBehavior<,>)
            );
        });

        services.AddValidatorsFromAssembly(assembly);
        return services;
    }
}
