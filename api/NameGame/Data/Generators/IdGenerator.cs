using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace NameGame.Data.Generators;

public class IdGenerator : ValueGenerator<string>
{
    public override bool GeneratesTemporaryValues => false;

    public override string Next(EntityEntry entry)
    {
        var guid = Guid.NewGuid();

        var base16 = BitConverter.ToString(guid.ToByteArray())
            .ToLower()
            .Replace("-", "");

        return base16;
    }
}