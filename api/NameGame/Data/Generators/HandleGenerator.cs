using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace NameGame.Data.Generators;

public class HandleGenerator : ValueGenerator<string>
{
    public override bool GeneratesTemporaryValues => false;

    public override string Next(EntityEntry entry)
    {
        var length = 4;
        var random = new Random();

        return new string([.. Enumerable
            .Range(0, length)
            .Select(_ => (char)random.Next('a','z' +1))]);
    }
}