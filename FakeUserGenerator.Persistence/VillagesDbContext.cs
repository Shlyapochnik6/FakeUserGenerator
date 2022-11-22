using FakeUserGenerator.Application.Common.Interfaces;
using FakeUserGenerator.Domain;
using Microsoft.EntityFrameworkCore;

namespace FakeUserGenerator.Persistence;

public class VillagesDbContext : DbContext, IVillagesDbContext
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<Village> Villages { get; set; }
    
    public VillagesDbContext(DbContextOptions<VillagesDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=villages;Username=postgres;Password=sa");
    }
}