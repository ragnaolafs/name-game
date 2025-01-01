using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NameGame.Data.Generators;
using NameGame.Models;

namespace NameGame.Data.Configurations;

public class GuessConfiguration : IEntityTypeConfiguration<GuessEntity>
{
    public void Configure(EntityTypeBuilder<GuessEntity> builder)
    {
        builder.HasKey(g => g.Id);

        builder
            .Property(g => g.Id)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<IdGenerator>()
            .HasMaxLength(32);

        builder
            .Property(g => g.GameId)
            .HasMaxLength(32);

        builder
            .Property(g => g.User)
            .HasMaxLength(256);

        builder
            .Property(g => g.Guess)
            .HasMaxLength(256);

        builder.HasIndex(g => g.Score);
    }
}