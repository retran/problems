public class Solution
{
    private record Job(int Start, int End, int Profit);

    public int JobScheduling(int[] startTime, int[] endTime, int[] profit)
    {
        int n = profit.Length;
        var jobs = new Job[n];
        for (int i = 0; i < n; i++)
        {
            jobs[i] = new Job(startTime[i], endTime[i], profit[i]);
        }
        
        Array.Sort(jobs, (a, b) => a.Start.CompareTo(b.Start));
        
        int[] dp = new int[n + 1];
    
        
        for (int i = n - 1; i >= 0; i--)
        {
            int nextIndex = GetNextJobIndex(jobs, jobs[i].End);
            dp[i] = Math.Max(jobs[i].Profit + (nextIndex < n ? dp[nextIndex] : 0), dp[i + 1]);
        }
        
        return dp[0];
    }
    
    private int GetNextJobIndex(Job[] jobs, int time)
    {
        int left = 0;
        int right = jobs.Length - 1;
        int ans = jobs.Length;
        
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (jobs[mid].Start >= time)
            {
                ans = mid;
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
            }
        }
        
        return ans;
    }

        static void Main()
    {
        var solution = new Solution();
        int value = solution.JobScheduling(
            [1, 2, 2, 3],
            [2, 5, 3, 4],
            [3, 4, 1, 2]);
        Console.WriteLine(value);
    }

}
