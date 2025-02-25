public class Solution
{
    private record Entry(int Value, int Frequency);

    public int[] FindXSum(int[] nums, int k, int x)
    {
        int n = nums.Length - k + 1;
        var result = new int[n];
        for (int i = 0; i < n; i++)
        {
            result[i] = FindXSum(nums, x, i, k);
        }
        return result;
    }

    private static int FindXSum(int[] nums, int x, int from, int length)
    {
        var frequencies = new Dictionary<int, int>();

        for (int i = from; i < from + length && i < nums.Length; i++)
        {
            if (!frequencies.TryGetValue(nums[i], out var count))
            {
                count = 0;
            }
            frequencies[nums[i]] = count + 1;
        }

        var pq = new PriorityQueue<Entry, Entry>(Comparer<Entry>.Create((a, b) =>
        {
            if (a.Frequency == b.Frequency)
            {
                return a.Value.CompareTo(b.Value);
            }

            return a.Frequency.CompareTo(b.Frequency);
        }));

        foreach (var kvp in frequencies)
        {
            var entry = new Entry(kvp.Key, kvp.Value);
            pq.Enqueue(entry, entry);
            if (pq.Count > x)
            {
                pq.Dequeue();
            }
        }

        var sum = 0;
        while (pq.Count > 0)
        {
            var entry = pq.Dequeue();
            sum += entry.Value * entry.Frequency;
        }
        return sum;
    }
}