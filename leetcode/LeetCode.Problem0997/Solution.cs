public class Solution
{
    public int FindJudge(int n, int[][] trust)
    {
        int[] inbound = new int[n];
        int[] outbound = new int[n];

        foreach (var relation in trust)
        {
            inbound[relation[1] - 1]++;
            outbound[relation[0] - 1]++;
        }

        for (int i = 0; i < n; i++)
        {
            if (outbound[i] == 0 && inbound[i] == n - 1)
            {
                return i + 1;
            }
        }

        return -1;
    }
}