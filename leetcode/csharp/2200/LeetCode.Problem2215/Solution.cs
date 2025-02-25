public class Solution
{
    public IList<IList<int>> FindDifference(int[] nums1, int[] nums2)
    {
        var set1 = new HashSet<int>(nums1);
        var set2 = new HashSet<int>(nums2);

        var result = new List<IList<int>>();

        var result1 = new List<int>();
        foreach (var num in set1)
        {
            if (!set2.Contains(num))
            {
                result1.Add(num);
            }
        }

        var result2 = new List<int>();
        foreach (var num in set2)
        {
            if (!set1.Contains(num))
            {
                result2.Add(num);
            }
        }

        result.Add(result1);
        result.Add(result2);

        return result;
    }
}