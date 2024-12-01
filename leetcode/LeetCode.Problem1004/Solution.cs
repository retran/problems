public class Solution
{
    public int LongestOnes(int[] nums, int k)
    {
        int i = 0;
        int j = 0;
        int currentFlips = 0;
        int currentLength = 0;
        int maxLength = 0;

        while (j < nums.Length)
        {
            if (nums[j] == 1)
            {
                j++;
                currentLength++;
            }
            else
            {
                if (currentFlips < k)
                {
                    currentFlips++;
                    currentLength++;
                    j++;
                }
                else
                {
                    if (nums[i] == 0)
                    {
                        currentFlips--;
                    }
                    i++;
                    currentLength--;
                }
            }

            if (currentLength > maxLength)
            {
                maxLength = currentLength;
            }
        }

        return maxLength;
    }
}