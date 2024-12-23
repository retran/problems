public class Solution
{
    public int[] SortArrayByParity(int[] nums)
    {
        int i = 0;
        int j = nums.Length - 1;

        while (i < j)
        {
            if (nums[i] % 2 == 0)
            {
                i++;
                continue;
            }

            if (nums[j] % 2 == 1)
            {
                j--;
                continue;
            }

            int tmp = nums[i];
            nums[i] = nums[j];
            nums[j] = tmp;
            i++;
            j--;
        }

        return nums;
    }
}