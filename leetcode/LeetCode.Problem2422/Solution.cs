public class Solution
{
    public int MinimumOperations(int[] nums)
    {
        int i = 0;
        int j = nums.Length - 1;

        int count = 0;

        while (i != j && j >= 0 && i < nums.Length)
        {
            if (nums[i] == nums[j])
            {
                i++;
                j--;
                continue;
            }
            if (nums[i] > nums[j])
            {
                nums[j - 1] = nums[j - 1] + nums[j];
                j--;
                count++;
            }
            else
            {
                nums[i + 1] = nums[i + 1] + nums[i];
                i++;
                count++;
            }
        }

        return count;
    }
}