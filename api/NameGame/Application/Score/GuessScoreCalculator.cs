namespace NameGame.Application.Score;

public static class GuessScoreCalculator
{
    public static double CalculateScore(string guess, string answer)
    {
        int distance = CalculateLevenshteinDistance(guess.ToLower(), answer.ToLower());
        int maxLength = Math.Max(guess.Length, answer.Length);

        if (maxLength == 0)
        {
            return 1.0; // Both strings are empty, perfect match
        }

        return 1.0 - (double)distance / maxLength;
    }

    public static int CalculateLevenshteinDistance(string source, string target)
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
}