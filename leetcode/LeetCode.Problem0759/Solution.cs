public class Solution
{
    public IList<Interval> EmployeeFreeTime(IList<IList<Interval>> schedule)
    {
        var queue = new PriorityQueue<Interval, int>();

        foreach (var employeeSchedule in schedule)
        {
            foreach (var interval in employeeSchedule)
            {
                queue.Enqueue(interval, interval.start);
            }
        }

        var freeIntervals = new List<Interval>();

        var currentEnd = queue.Dequeue().end;
        while (queue.Count > 0)
        {
            var next = queue.Dequeue();

            if (currentEnd >= next.start)
            {
                currentEnd = Math.Max(currentEnd, next.end);
            }
            else
            {
                freeIntervals.Add(new Interval(currentEnd, next.start));
                currentEnd = next.end;
            }
        }

        return freeIntervals;
    }
}