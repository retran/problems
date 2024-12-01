public class Solution
{
    public int MaxOperations(int[] nums, int k)
    {
        if (nums.Length < 2)
        {
            return 0;
        }

        int count = 0;
        var map = new Dictionary<int, int>();

        for (int i = 0; i < nums.Length; i++)
        {
            int complement = k - nums[i];
            if (map.TryGetValue(complement, out var complementCount) 
                && complementCount > 0)
            {
                map[complement]--;
                count++;
            }
            else
            {
                if (!map.TryGetValue(nums[i], out var numCount))
                {
                    numCount = 0;
                }
                map[nums[i]] = numCount + 1;
            }
        }

        return count;
    }
}