public class Solution
{
    private Dictionary<(int, int), int> _cache = new();

    public int MaxUncrossedLines(int[] nums1, int[] nums2, int i, int j)
    {
        if (i < 0 || j < 0)
        {
            return 0;
        }

        if (_cache.TryGetValue((i, j), out var cached))
        {
            return cached;
        }

        int max = 0;

        if (nums1[i] == nums2[j])
        {
            max = Math.Max(max, MaxUncrossedLines(nums1, nums2, i - 1, j - 1) + 1);
        }

        max = Math.Max(max, MaxUncrossedLines(nums1, nums2, i - 1, j - 1));

        for (int k = j - 1; k >= 0; k--)
        {
            if (nums1[i] == nums2[k])
            {
                max = Math.Max(max, MaxUncrossedLines(nums1, nums2, i - 1, k - 1) + 1);
            }
        }

        for (int k = i - 1; k >= 0; k--)
        {
            if (nums2[j] == nums1[k])
            {
                max = Math.Max(max, MaxUncrossedLines(nums1, nums2, k - 1, j - 1) + 1);
            }
        }

        _cache[(i, j)] = max;

        return max;
    }


    public int MaxUncrossedLines(int[] nums1, int[] nums2)
    {
        return MaxUncrossedLines(nums1, nums2, nums1.Length - 1, nums2.Length - 1);
    }
}