public class Solution
{
    public int[] ProductExceptSelf(int[] nums)
    {
        int[] output = new int[nums.Length];

        output[0] = 1;
        for (int i = 1; i < nums.Length; i++)
        {
            output[i] = output[i - 1] * nums[i - 1];
        }

        int production = 1;
        for (int i = nums.Length - 2; i >= 0; i--)
        {
            production *= nums[i + 1];
            output[i] = output[i] * production;
        }

        return output;
    }
}