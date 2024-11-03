public class Solution
{
    public int ArraySign(int[] nums)
    {
        int count = 0;
        foreach (var number in nums)
        {
            if (number == 0)
            {
                return 0;
            }

            if (number < 0)
            {
                count++;
            }
        }

        return count % 2 == 0 ? 1 : -1;
    }
}