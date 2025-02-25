public class Solution
{
    public int[] Intersect(int[] nums1, int[] nums2)
    {
        var frequencies1 = new Dictionary<int, int>();
        var frequencies2 = new Dictionary<int, int>();

        for (int i = 0; i < nums1.Length; i++)
        {
            if (!frequencies1.TryGetValue(nums1[i], out var frequency))
            {
                frequency = 1;
            }
            else
            {
                frequency++;
            }
            frequencies1[nums1[i]] = frequency;
        }

        for (int i = 0; i < nums2.Length; i++)
        {
            if (!frequencies2.TryGetValue(nums2[i], out var frequency))
            {
                frequency = 1;
            }
            else
            {
                frequency++;
            }
            frequencies2[nums2[i]] = frequency;
        }

        var intersection = new List<int>();

        for (int i = 0; i < nums1.Length; i++)
        {
            int frequency = 0;
            if (frequencies1.TryGetValue(nums1[i], out var frequency1) 
                && frequencies2.TryGetValue(nums1[i], out var frequency2))
            {
                frequency = Math.Min(frequency1, frequency2);
            }

            while (frequency > 0)
            {
                intersection.Add(nums1[i]);
                frequency--;
            }

            frequencies1[nums1[i]] = 0;
            frequencies2[nums1[i]] = 0;
        }

        return intersection.ToArray();
    }
}