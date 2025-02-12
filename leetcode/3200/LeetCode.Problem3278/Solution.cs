public class Solution
{
    public int MaxValue(int[] nums, int k)
    {
        int n = nums.Length;
        if (2 * k > n) return 0;

        List<HashSet<int>?> prefixDP = new List<HashSet<int>?>(n + 1);
        HashSet<int>[] cur = new HashSet<int>[k + 1];
        for (int j = 0; j <= k; j++)
        {
            cur[j] = new HashSet<int>();
        }
        cur[0].Add(0);
        prefixDP.Add(new HashSet<int>(cur[k]));

        for (int i = 0; i < n; i++)
        {
            HashSet<int>[] newdp = new HashSet<int>[k + 1];
            for (int j = 0; j <= k; j++)
            {
                newdp[j] = new HashSet<int>(cur[j]);
            }
            for (int j = k; j >= 1; j--)
            {
                foreach (int x in cur[j - 1])
                {
                    int candidate = x | nums[i];
                    newdp[j].Add(candidate);
                }
            }
            cur = newdp;
            prefixDP.Add(new HashSet<int>(cur[k]));
        }

        List<HashSet<int>?> suffixDP = new List<HashSet<int>?>(n + 1);
        for (int i = 0; i <= n; i++)
        {
            suffixDP.Add(null);
        }

        HashSet<int>[] curS = new HashSet<int>[k + 1];
        for (int j = 0; j <= k; j++)
        {
            curS[j] = new HashSet<int>();
        }

        curS[0].Add(0);
        suffixDP[n] = new HashSet<int>(curS[k]);
        for (int i = n - 1; i >= 0; i--)
        {
            HashSet<int>[] newdpS = new HashSet<int>[k + 1];
            for (int j = 0; j <= k; j++)
            {
                newdpS[j] = new HashSet<int>(curS[j]);
            }
            for (int j = k; j >= 1; j--)
            {
                foreach (int x in curS[j - 1])
                {
                    int candidate = x | nums[i];
                    newdpS[j].Add(candidate);
                }
            }
            curS = newdpS;
            suffixDP[i] = new HashSet<int>(curS[k]);
        }

        int answer = 0;
        for (int i = k; i <= n - k; i++)
        {
            HashSet<int> setA = prefixDP[i]!;
            HashSet<int> setB = suffixDP[i]!;
            if (setA.Count == 0 || setB.Count == 0) continue;
            foreach (int a in setA)
            {
                foreach (int b in setB)
                {
                    answer = Math.Max(answer, a ^ b);
                }
            }
        }

        return answer;
    }
}
