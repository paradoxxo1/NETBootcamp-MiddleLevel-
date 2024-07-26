using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
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
        });
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}
