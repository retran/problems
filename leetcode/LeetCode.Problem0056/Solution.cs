public class Solution
{
    public int[][] Merge(int[][] intervals)
    {
        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

        var mergedIntervals = new Stack<int[]>();
        mergedIntervals.Push(intervals[0]);

        for (int i = 1; i < intervals.Length; i++)
        {
            var top = mergedIntervals.Peek();
            if (top[1] >= intervals[i][0])
            {
                top[1] = Math.Max(top[1], intervals[i][1]);
            }
            else
            {
                mergedIntervals.Push(intervals[i]);
            }
        }

        return mergedIntervals.ToArray();
    }
}