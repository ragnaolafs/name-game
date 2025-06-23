using NameGame.Models.Results;

namespace NameGame.Application.Score;

public static class GuessScoreCalculator
{
    public static ScoreResult CalculateScore(string guess, string answer)
    {
        int distance = CalculateLevenshteinDistance(guess.ToLower(), answer.ToLower());

        int maxLength = Math.Max(guess.Length, answer.Length);

        var distanceScore = 1.0 - (double)distance / maxLength;

        // indexes of matching characters
        var hintMatrix = GetHintMatrix(guess, answer);

        var hintScore = (double)hintMatrix.Count / (answer.Length - answer.Count(char.IsWhiteSpace));

        // the distance score should be 70% of the total score
        // the hint score should be 30% of the total score
        double score = distanceScore * 0.7 + hintScore * 0.3;

        return new ScoreResult(score, hintMatrix);
    }

    public static int CalculateLevenshteinDistance(
        string source,
        string target)
    {
        if (string.IsNullOrEmpty(source))
        {
            return target?.Length ?? 0;
        }

        if (string.IsNullOrEmpty(target))
        {
            return source.Length;
        }

        int[,] distance = new int[source.Length + 1, target.Length + 1];

        // Initialize the distance matrix
        for (int i = 0; i <= source.Length; i++)
        {
            distance[i, 0] = i;
        }

        for (int j = 0; j <= target.Length; j++)
        {
            distance[0, j] = j;
        }

        // Compute distances
        for (int i = 1; i <= source.Length; i++)
        {
            for (int j = 1; j <= target.Length; j++)
            {
                int cost = (source[i - 1] == target[j - 1]) ? 0 : 1;

                distance[i, j] = Math.Min(
                    Math.Min(
                        distance[i - 1, j] + 1,         // Deletion
                        distance[i, j - 1] + 1),        // Insertion
                        distance[i - 1, j - 1] + cost   // Substitution
                );
            }
        }

        return distance[source.Length, target.Length];
    }

    private static List<int> GetHintMatrix(string guess, string answer)
    {
        var guessWords = guess.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var answerWords = answer.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var wordHints = answerWords.Zip(guessWords, (a, g) =>
        {
            var matchesIndeces = new List<int>();

            for (int i = 0; i < a.Length && i < g.Length; i++)
            {
                if (char.ToLower(a[i]) == char.ToLower(g[i]))
                {
                    matchesIndeces.Add(i);
                }
            }

            return matchesIndeces;
        })
        .ToList();

        var hintMatrix = new List<int>();

        for (int i = 0; i < wordHints.Count; i++)
        {
            var offset = i == 0 ? 0 : guessWords[i - 1].Length + 1; // +1 for the space

            var offsetHint = wordHints[i].Select(index => index + offset);

            hintMatrix.AddRange(offsetHint);
        }

        return hintMatrix;
    }
}