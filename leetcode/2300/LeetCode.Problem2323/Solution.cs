public class Solution
{
    public int MinimumTime(int[] jobs, int[] workers)
    {
        Array.Sort(jobs);
        Array.Sort(workers);

        int max = 0;
        for (int i = 0; i < jobs.Length; i++)
        {
            int days = jobs[i] / workers[i];

            if (jobs[i] % workers[i] != 0)
            {
                days++;
            }

            if (days > max)
            {
                max = days;
            }
        }

        return max;
    }
}