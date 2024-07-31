using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TodoCleanArchitecture.Application.Behaivors;
using TodoCleanArchitecture.Domain.Abstractions;

namespace TodoCleanArchitecture.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddFluentEmail("admin@admin.com")
            .AddSmtpSender("localhost", 25);

        services.AddMemoryCache();
        services.AddMediatR(configuration =>
        {
            //configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), typeof(Entity).Assembly);

            // configuration.AddOpenBehavior(typeof(ExampleBehaivor<,>)); mediatr için Dependency

            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}
