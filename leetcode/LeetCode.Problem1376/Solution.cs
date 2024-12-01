public class Solution
{
    public int NumOfMinutes(int n, int headID, int[] manager, int[] informTime)
    {
        var reports = new Dictionary<int, List<int>>();

        for (int i = 0; i < manager.Length; i++)
        {
            if (!reports.TryGetValue(manager[i], out var list))
            {
                list = new List<int>() { i };
                reports[manager[i]] = list;
            }
            else
            {
                list.Add(i);
            }
        }

        var queue = new Queue<(int id, int time)>();

        queue.Enqueue((headID, 0));

        int maxTime = 0;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current.time > maxTime)
            {
                maxTime = current.time;
            }

            var nextTime = current.time + informTime[current.id];

            if (reports.TryGetValue(current.id, out var currentReports))
            {
                foreach (var id in currentReports)
                {
                    queue.Enqueue((id, nextTime));
                }
            }
        }

        return maxTime;
    }
}