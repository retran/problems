public class Solution
{
    public int[] SingleNumber(int[] nums)
    {
        int accumulator = 0;

        foreach (var num in nums)
        {
            accumulator = accumulator ^ num;
        }

        int diff = accumulator & (-accumulator);

        int x = 0;

        foreach (var num in nums)
        {
            if ((num & diff) != 0)
            {
                x = x ^ num;
            }
        }

        int y = accumulator ^ x;

        return [x, y];
    }
}