public class Solution
{
    public int MinSwaps(int[] nums)
    {
        int count = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == 1)
            {
                count++;
            }
        }

        int currentCountOfZeroes = 0;
        for (int i = 0; i < count; i++)
        {
            if (nums[i] == 0)
            {
                currentCountOfZeroes++;
            }
        }

        int minSwaps = currentCountOfZeroes;

        int j = 1;
        while (j < nums.Length)
        {
            int prev = j - 1;
            int next = j + count - 1;
            if (next >= nums.Length)
            {
                next = next - nums.Length;
            }

            if (nums[prev] == 0)
            {
                currentCountOfZeroes--;
            }

            if (nums[next] == 0)
            {
                currentCountOfZeroes++;
            }

            if (currentCountOfZeroes < minSwaps)
            {
                minSwaps = currentCountOfZeroes;
            }

            j++;
        }

        return minSwaps;
    }
}