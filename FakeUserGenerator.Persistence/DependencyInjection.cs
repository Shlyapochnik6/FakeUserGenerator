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
        var connectionString = "";
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            connectionString = configuration["ProductionDbConnection"];
        else
            connectionString = configuration["DbConnection"];
        services.AddDbContext<VillagesDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        services.AddScoped<IVillagesDbContext, VillagesDbContext>();
        return services;
    }
}