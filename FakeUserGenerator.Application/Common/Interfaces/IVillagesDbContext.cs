using FakeUserGenerator.Domain;
using Microsoft.EntityFrameworkCore;

namespace FakeUserGenerator.Application.Common.Interfaces;

public interface IVillagesDbContext
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<Village> Villages { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}