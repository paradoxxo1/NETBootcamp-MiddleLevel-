using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TodoCleanArchitecture.Application.Service;
using TodoCleanArchitecture.Domain.Repositories;
using TodoCleanArchitecture.Infrastructure.Context;
using TodoCleanArchitecture.Infrastructure.Repositories;
using TodoCleanArchitecture.Infrastructure.Services;

namespace TodoCleanArchitecture.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.TryAddScoped<ApplicationDbContext>();
        services.TryAddScoped<ITodoRepository, TodoRepository>();
        services.TryAddScoped<ICacheService, MemoryCacheService>();
        return services;
    }
}
