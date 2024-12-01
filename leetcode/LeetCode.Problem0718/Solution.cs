public class Solution
{
    public int FindLength(int[] nums1, int[] nums2)
    {
        int max = 0;
        int[,] dp = new int[nums1.Length + 1, nums2.Length + 1];

        for (int i = nums1.Length - 1; i >= 0; --i)
        {
            for (int j = nums2.Length - 1; j >= 0; --j)
            {
                if (nums1[i] == nums2[j])
                {
                    dp[i, j] = dp[i + 1, j + 1] + 1;
                    if (max < dp[i, j])
                    {
                        max = dp[i, j];
                    }
                }
            }
        }

        return max;
    }
}
