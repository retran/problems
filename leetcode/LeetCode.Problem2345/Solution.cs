public class Solution
{
    public int VisibleMountains(int[][] peaks)
    {
        List<int[]> intervals = new List<int[]>();

        for (int i = 0; i < peaks.Length; i++)
        {
            int x = peaks[i][0];
            int y = peaks[i][1];
            int[] interval = new int[] { x - y, x + y };
            intervals.Add(interval);
        }

        intervals = intervals.OrderBy(interval => interval[0]).ToList();

        var stack = new Stack<int[]>();

        for (int i = 0; i < intervals.Count; i++)
        {
            if (stack.Count == 0)
            {
                stack.Push(intervals[i]);
                continue;
            }

            var current = intervals[i];
            var prev = stack.Pop();

            if (prev[0] == current[0] && current[1] == prev[1])
            {
                stack.Push(prev);
                stack.Push(current);
            }
            else if (prev[0] <= current[0] && current[1] <= prev[1])
            {
                stack.Push(prev);
            }
            else if (current[0] <= prev[0] && prev[1] <= current[1])
            {
                stack.Push(current);
            }
            else
            {
                if (prev[0] < current[0])
                {
                    stack.Push(prev);
                    stack.Push(current);
                }
                else
                {
                    stack.Push(current);
                    stack.Push(prev);
                }
            }
        }

        int count = 0;

        while (stack.Count > 0)
        {
            bool flag = false;
            var current = stack.Pop();
            while (stack.Count > 0 && stack.Peek()[0] == current[0] && stack.Peek()[1] == current[1])
            {
                flag = true;
                stack.Pop();
            }

            if (!flag)
            {
                count++;
            }
        }

        return count;
    }
}