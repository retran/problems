public class Solution
{
    private const int MOD = 1000000007;
    private long[] fullMemo;
    private long[] partialMemo;

    public int NumTilings(int n)
    {
        fullMemo = new long[n + 1];
        partialMemo = new long[n + 1];

        for (int i = 0; i <= n; i++)
        {
            fullMemo[i] = -1;
            partialMemo[i] = -1;
        }

        return (int)Full(n);
    }

    private long Full(int n)
    {
        if (n == 0) return 0;
        if (n == 1) return 1;
        if (n == 2) return 2;

        if (fullMemo[n] != -1)
        {
            return fullMemo[n];
        }

        long result = (Full(n - 1) + Full(n - 2) + 2 * Partial(n - 1)) % MOD;
        fullMemo[n] = result;
        return result;
    }

    private long Partial(int n)
    {
        if (n == 0) return 0;
        if (n == 1) return 0;
        if (n == 2) return 1;

        if (partialMemo[n] != -1)
        {
            return partialMemo[n];
        }

        long result = (Partial(n - 1) + Full(n - 2)) % MOD;
        partialMemo[n] = result;
        return result;
    }
}
