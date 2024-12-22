public class Solution
{
    private readonly Dictionary<int, int> _cache = new();

    public int MincostTickets(int[] days, int[] costs, int dayIndex)
    {
        if (dayIndex < 0)
        {
            return 0;
        }

        if (_cache.TryGetValue(dayIndex, out var cached))
        {
            return cached;
        }

        int currentDay = days[dayIndex];
        int min = int.MaxValue;

        int option1 = MincostTickets(days, costs, dayIndex - 1) + costs[0];
        min = Math.Min(min, option1);

        int i = dayIndex;
        while (i >= 0 && days[i] > currentDay - 7)
        {
            i--;
        }

        int option2 = MincostTickets(days, costs, i) + costs[1];
        min = Math.Min(min, option2);

        i = dayIndex;
        while (i >= 0 && days[i] > currentDay - 30)
        {
            i--;
        }
        
        int option3 = MincostTickets(days, costs, i) + costs[2];
        min = Math.Min(min, option3);

        _cache[dayIndex] = min;

        return min;
    }

    public int MincostTickets(int[] days, int[] costs)
    {
        return MincostTickets(days, costs, days.Length - 1);
    }
}
