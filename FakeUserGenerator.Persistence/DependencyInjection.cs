using FakeUserGenerator.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FakeUserGenerator.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];

        services.AddDbContext<VillagesDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IVillagesDbContext, VillagesDbContext>();

        return services;
    }
}