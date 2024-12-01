public class Solution
{
    public int FindMaxConsecutiveOnes(int[] nums)
    {
        int prevCount = 0;
        int count = 0;

        bool hasZero = false;
        
        int max = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == 1)
            {
                count++;
            }
            else
            {
                hasZero = true;
                max = Math.Max(prevCount + count + 1, max);
                prevCount = count;
                count = 0;
            }
        }

        max = Math.Max(prevCount + count + (hasZero ? 1 : 0), max);

        return max;
    }
}