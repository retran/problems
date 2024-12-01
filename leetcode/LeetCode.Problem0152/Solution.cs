public class Solution
{
    public int MaxProduct(int[] nums)
    {
        if (nums.Length == 0)
        {
            return 0;
        }

        int max = nums[0];
        int min = nums[0];
        int answer = nums[0];

        for (int i = 1; i < nums.Length; i++)
        {
            int current = nums[i];
            
            // If current is negative, swap max and min
            if (current < 0)
            {
                int temp = max;
                max = min;
                min = temp;
            }

            max = Math.Max(current, max * current);
            min = Math.Min(current, min * current);

            answer = Math.Max(answer, max);
        }

        return answer;
    }
}
