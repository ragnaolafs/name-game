namespace NameGame.Data.Interfaces;

public interface ITimeStamps
{
    DateTime? CreatedAt { get; set; }

    DateTime? UpdatedAt { get; set; }
}