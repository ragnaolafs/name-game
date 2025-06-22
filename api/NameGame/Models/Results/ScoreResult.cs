namespace NameGame.Models.Results;

public record ScoreResult(
    double Score,
    List<int> HintsIndices);