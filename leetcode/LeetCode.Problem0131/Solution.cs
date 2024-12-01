public class Solution
{
    private bool[,] PrecomputePalindromes(string s)
    {
        int n = s.Length;
        var dp = new bool[n, n];

        for (int i = 0; i < n; i++)
        {
            dp[i, i] = true;
        }

        for (int length = 2; length <= n; length++)
        {
            for (int i = 0; i <= n - length; i++)
            {
                int j = i + length - 1;
                if (s[i] == s[j])
                {
                    dp[i, j] = length == 2 || dp[i + 1, j - 1];
                }
            }
        }

        return dp;
    }

    private void Backtrack(string s, int start, List<string> currentPartition, List<IList<string>> solutions, bool[,] dp)
    {
        if (start == s.Length)
        {
            solutions.Add(new List<string>(currentPartition));
            return;
        }

        for (int end = start; end < s.Length; end++)
        {
            if (dp[start, end])
            {
                currentPartition.Add(s.Substring(start, end - start + 1));
                Backtrack(s, end + 1, currentPartition, solutions, dp);
                currentPartition.RemoveAt(currentPartition.Count - 1); // Backtrack
            }
        }
    }

    public IList<IList<string>> Partition(string s)
    {
        var result = new List<IList<string>>();
        if (string.IsNullOrEmpty(s))
        {
            return result;
        }

        bool[,] dp = PrecomputePalindromes(s);
        Backtrack(s, 0, new List<string>(), result, dp);

        return result;
    }
}
