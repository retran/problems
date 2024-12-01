public class Solution
{
    private class Entry
    {
        public readonly int Value1;
        public readonly int Value2;

        public readonly int Index;

        public Entry(int value1, int value2, int index)
        {
            Value1 = value1;
            Value2 = value2;
            Index = index;
        }
    }

    public long MaxScore(int[] nums1, int[] nums2, int k)
    {
        var entries = new List<Entry>();

        for (int i = 0; i < nums1.Length; i++)
        {
            var entry = new Entry(nums1[i], nums2[i], i);
            entries.Add(entry);
        }

        entries = entries.OrderByDescending(v => v.Value2).ToList();

        System.Console.WriteLine(string.Join(",\t", entries.Select(e => e.Value1)));
        System.Console.WriteLine(string.Join(",\t", entries.Select(e => e.Value2)));

        var pq = new PriorityQueue<Entry, int>();

        long currentSum = 0;

        for (int i = 0; i < k; i++)
        {
            currentSum += entries[i].Value1;
            pq.Enqueue(entries[i], entries[i].Value1);
        }

        long maxScore = currentSum * entries[k - 1].Value2;

        for (int i = k; i < entries.Count; i++)
        {
            var min = pq.Dequeue().Value1;

            currentSum -= min;
            currentSum += entries[i].Value1;
            var score = currentSum * entries[i].Value2;
            pq.Enqueue(entries[i], entries[i].Value1);

            if (score > maxScore)
            {
                maxScore = score;
            }
        }

        return maxScore;
    }
}