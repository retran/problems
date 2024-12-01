public class Solution
{
    public int FourSumCount(int[] nums1, int[] nums2, int[] nums3, int[] nums4)
    {
        Dictionary<int, int> sumCount = new Dictionary<int, int>();

        foreach (int num1 in nums1)
        {
            foreach (int num2 in nums2)
            {
                int sum = num1 + num2;
                if (sumCount.ContainsKey(sum))
                {
                    sumCount[sum]++;
                }
                else
                {
                    sumCount[sum] = 1;
                }
            }
        }

        int count = 0;
        foreach (int num3 in nums3)
        {
            foreach (int num4 in nums4)
            {
                int target = -(num3 + num4);
                if (sumCount.ContainsKey(target))
                {
                    count += sumCount[target];
                }
            }
        }

        return count;
    }
}
