using Microsoft.EntityFrameworkCore;
using NameGame.Data.Configurations;
using NameGame.Data.Entities;
using NameGame.Data.Interfaces;
using NameGame.Models;

namespace NameGame.Data.Contexts;

public class NameGameDbContext(
    DbContextOptions<NameGameDbContext> options)
    : DbContext(options)
{
    public DbSet<GameEntity> Games { get; set; }

    public DbSet<GuessEntity> Guesses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(GameConfiguration).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        var addedOrModified = this.ChangeTracker
            .Entries()
            .Where(e => e.State is EntityState.Added or EntityState.Modified)
            .Where(e => e is ITimeStamps);

        foreach (var entity in addedOrModified)
        {
            ((ITimeStamps)entity).UpdatedAt = DateTime.Now;

            if (entity.State is EntityState.Added)
            {
                ((ITimeStamps)entity).CreatedAt = DateTime.Now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}