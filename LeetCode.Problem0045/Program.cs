public class Solution
{
    public int Jump(int[] nums)
    {
        int[] jumps = new int[nums.Length];

        for (int i = 1; i < jumps.Length - 1; i++)
        {
            if (nums[i] == 0)
            {
                continue;
            }

            for (int j = i + 1; j <= i + nums[i] && j < jumps.Length; j++)
            {
                if (jumps[j] == 0)
                {
                    jumps[j] = jumps[i] + 1;
                }
                else
                {
                    jumps[j] = Math.Min(jumps[j], jumps[i] + 1);
                }
            }
        }

        return jumps[nums.Length - 1];
    }
}