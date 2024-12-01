public class Solution
{
    public int[] CountBits(int n)
    {
        var result = new int[n + 1];
        result[0] = 0;
        for (int i = 1; i <= n; i++)
        {
            result[i] = result[i >> 1] + (i & 1);
        }
        return result;
    }
}