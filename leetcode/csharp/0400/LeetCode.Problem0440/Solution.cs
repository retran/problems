public class Solution
{
    public int FindKthNumber(int n, int k)
    {
        int current = 1;
        k--;

        while (k > 0)
        {
            long steps = CountSteps(n, current, current + 1);
            if (steps <= k)
            {
                k -= (int)steps;
                current++;
            }
            else
            {
                current *= 10;
                k--;
            }
        }

        return current;
    }

    private long CountSteps(int n, long from, long to)
    {
        long steps = 0;
        while (from <= n)
        {
            steps += Math.Min(n + 1, to) - from;
            from *= 10;
            to *= 10;
        }
        return steps;
    }
}
