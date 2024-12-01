// https://leetcode.com/problems/longest-subarray-of-1s-after-deleting-one-element
// same as Problem1004 with k == 1

public class Solution
{
    public int LongestSubarray(int[] nums)
    {
        int i = 0;
        int j = 0;
        int currentLength = 0;
        int maxLength = 0;
        bool hadZero = false;

        while (j < nums.Length)
        {
            if (nums[j] == 1)
            {
                j++;
                currentLength++;
            }
            else
            {
                if (!hadZero)
                {
                    hadZero = true;
                    currentLength++;
                    j++;
                }
                else
                {
                    if (nums[i] == 0)
                    {
                        hadZero = false;
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

        return maxLength - 1;
    }
}