using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NameGame.Data.Entities;
using NameGame.Data.Generators;
using NameGame.Models.Enums;

namespace NameGame.Data.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<GameEntity>
{
    public void Configure(EntityTypeBuilder<GameEntity> builder)
    {
        builder.HasKey(g => g.Id);

        builder
            .Property(g => g.Id)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<IdGenerator>()
            .HasMaxLength(32);

        builder
            .Property(g => g.Handle)
            .HasMaxLength(64);

        builder
            .Property(g => g.Answer)
            .HasMaxLength(512);

        var statusConverter = new ValueConverter<GameStatus, string>(
            v => v.ToString(),
            v => (GameStatus)Enum.Parse(typeof(GameStatus), v));

        builder
            .Property(g => g.Status)
            .HasConversion(statusConverter)
            .HasMaxLength(32);

        builder
            .HasMany(g => g.Guesses)
            .WithOne(g => g.Game)
            .HasForeignKey(g => g.GameId);
    }
}