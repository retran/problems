public class Solution
{
    private int SortArray(int[] nums, int offset)
    {
        int count = 0;
        var visited = new HashSet<int>();

        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == 0 || nums[i] == i + offset)
            {
                continue;
            }

            count++;

            int current = i;
            while (nums[current] > 0 && visited.Add(current))
            {
                current = nums[current] - offset;
                if (current == i)
                {
                    count++;
                    break;
                }
            }
        }

        return count;
    }

    public int SortArray(int[] nums)
    {
        return Math.Min(SortArray(nums, 0), SortArray(nums, 1));
    }
}