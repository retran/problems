public class Solution
{
    private Dictionary<(int, int), int> _cache = new Dictionary<(int, int), int>();

    public int MinDifficulty(int[] jobDifficulty, int currentDay, int startingJob, int days)
    {
        if (_cache.TryGetValue((currentDay, startingJob), out var cached))
        {
            return cached;
        }

        if (currentDay == days)
        {
            if (startingJob == jobDifficulty.Length - 1)
            {
                return jobDifficulty[jobDifficulty.Length - 1];
            }

            int max = 0;
            for (int i = startingJob; i < jobDifficulty.Length; i++)
            {
                if (jobDifficulty[i] > max)
                {
                    max = jobDifficulty[i];
                }
            }

            _cache[(currentDay, startingJob)] = max;

            return max;
        }

        int min = int.MaxValue;
        for (int i = startingJob + 1; i < jobDifficulty.Length - (days - (currentDay + 1)); i++)
        {
            int max = 0;
            for (int j = startingJob; j < i; j++)
            {
                if (jobDifficulty[j] > max)
                {
                    max = jobDifficulty[j];
                }
            }

            var difficulty = max + MinDifficulty(jobDifficulty, currentDay + 1, i, days);
            if (difficulty < min)
            {
                min = difficulty;
            }
        }

        _cache[(currentDay, startingJob)] = min;

        return min;
    }


    public int MinDifficulty(int[] jobDifficulty, int d)
    {
        int n = jobDifficulty.Length;
        if (n < d) {
            return -1;
        }

        return MinDifficulty(jobDifficulty, 1, 0, d);
    }
}
