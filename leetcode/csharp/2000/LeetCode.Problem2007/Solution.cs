public class Solution
{
    public int[] FindOriginalArray(int[] changed)
    {
        if (changed.Length % 2 != 0)
        {
            return Array.Empty<int>();
        }

        Array.Sort(changed);

        var answer = new List<int>();
        var frequencies = new Dictionary<int, int>();

        foreach (var value in changed)
        {
            if (!frequencies.TryGetValue(value, out var frequency))
            {
                frequency = 0;
            }

            frequencies[value] = frequency + 1;
        }

        bool Remove(int value)
        {
            if (!frequencies.TryGetValue(value, out var frequency))
            {
                return false;
            }

            if (frequency == 1)
            {
                frequencies.Remove(value);
            }
            else
            {
                frequencies[value] = frequency - 1;
            }

            return true;
        }

        foreach (var value in changed)
        {
            if (!frequencies.ContainsKey(value))
            {
                continue;
            }

            var doubled = value * 2;
            if (Remove(doubled))
            {
                answer.Add(value);
                Remove(value);
            }
        }

        if (frequencies.Count != 0) 
        {
            return Array.Empty<int>();
        }

        return answer.ToArray();
    }
}