public class Solution
{
    public int DominantIndex(int[] nums)
    {
        int max = 0;
        int maxIndex = 0;
        int secondMax = 0;

        bool found = false;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] > max)
            {
                secondMax = max;
                max = nums[i];
                maxIndex = i;
            }
            else if (nums[i] > secondMax)
            {
                secondMax = nums[i];
            }
        }

        return max >= secondMax * 2 ? maxIndex : -1;
    }
}