public class Solution
{
    public int Jump(int[] nums)
    {
        if (nums.Length <= 1) 
        {
            return 0;
        }

        int jumps = 0;
        int currentEnd = 0;
        int farthest = 0;

        for (int i = 0; i < nums.Length - 1; i++)
        {
            farthest = Math.Max(farthest, i + nums[i]);

            if (i == currentEnd)
            {
                jumps++;
                currentEnd = farthest;

                if (currentEnd >= nums.Length - 1)
                {
                    break;
                }
            }
        }

        return jumps;
    }
}