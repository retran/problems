public class Solution
{
    public int BitwiseComplement(int n)
    {
        if (n == 0)
        {
            return 1;
        }

        int mask = 1;
        int temp = n;
        while (temp > 0)
        {
            mask <<= 1;
            temp >>= 1;
        }

        return n ^ (mask - 1);
    }
}