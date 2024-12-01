public class Solution
{
    private int Map(int[] mapping, int value)
    {
        int result = 0;
        int pow = 1;

        if (value == 0)
        {
            return mapping[0];
        }

        while (value > 0)
        {
            result += mapping[value % 10] * pow;
            value = value / 10;
            pow = pow * 10;
        }
        return result;
    }

    public int[] SortJumbled(int[] mapping, int[] nums)
    {
        int[] keys = new int[nums.Length];
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i] = Map(mapping, nums[i]);
        }

        Array.Sort(keys, nums);
        return nums;
    }
}