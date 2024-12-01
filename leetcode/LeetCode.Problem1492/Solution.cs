public class Solution
{
    public int KthFactor(int n, int k)
    {
        bool[] factors = new bool[n + 1];

        var upperBound = Math.Ceiling(Math.Sqrt(n));

        for (int i = 1; i <= upperBound; i++)
        {
            if (n % i == 0)
            {
                factors[i] = true;
                factors[n / i] = true;
            }
        }

        for (int i = 1; i <= n; i++)
        {
            if (factors[i])
            {
                k--;
                if (k == 0)
                {
                    return i;
                }
            }
        }

        return -1;
    }
}