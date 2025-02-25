public class Solution
{
    private readonly Dictionary<(int, int), int> _cache = new Dictionary<(int, int), int>();

    public int MaxCoinsImpl(int[] scores, int left, int right)
    {
        if (right < left)
        {
            return 0;
        }

        if (_cache.TryGetValue((left, right), out var cached))
        {
            return cached;
        }

        var max = 0;
        for (int i = left; i <= right; i++)
        {
            int score = scores[i];
            int leftScore = left == 0 ? 1 : scores[left - 1];
            int rightScore = right == scores.Length - 1 ? 1 : scores[right + 1];
            int gain = score * leftScore * rightScore;
            int remaining = MaxCoinsImpl(scores, left, i - 1) + MaxCoinsImpl(scores, i + 1, right);
            max = Math.Max(max, gain + remaining);
        }

        _cache[(left, right)] = max;

        return max;
    }

    public int MaxCoins(int[] nums)
    {
        return MaxCoinsImpl(nums, 0, nums.Length - 1);
    }
}