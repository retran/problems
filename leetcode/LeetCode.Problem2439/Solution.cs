public class Solution
{
    public int MinimizeArrayValue(int[] nums)
    {
        long answer = 0, sum = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            sum += nums[i];
            int value = (int)Math.Ceiling(sum / (i + 1.0));
            answer = Math.Max(answer, value);
        }
        return (int) answer;
    }
}