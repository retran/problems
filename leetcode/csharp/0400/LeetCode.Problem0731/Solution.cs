public class MyCalendarTwo
{
    private class Interval
    {
        public int Start { get; private set; }
        public int End { get; private set; }

        public Interval(int start, int end)
        {
            Start = start;
            End = end;
        }

        public bool IsOverlap(Interval other)
        {
            return (this.Start < other.End && this.End > other.Start)
                || (other.Start < this.End && other.End > this.Start);
        }
    }

    private IList<Interval> _events = new List<Interval>();
    private IList<Interval> _overlaps = new List<Interval>();

    public MyCalendarTwo()
    {

    }

    public bool Book(int start, int end)
    {
        var interval = new Interval(start, end);
        foreach (var overlap in _overlaps)
        {
            if (overlap.IsOverlap(interval))
            {
                return false;
            }
        }

        foreach (var ev in _events)
        {
            if (interval.IsOverlap(ev))
            {
                var overlap = new Interval(Math.Max(ev.Start, interval.Start), 
                    Math.Min(ev.End, interval.End));
                _overlaps.Add(overlap);
            }
        }

        _events.Add(interval);
        return true;
    }
}