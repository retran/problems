public class Solution
{
    public uint reverseBits(uint n)
    {
        uint result = 0;
        uint num = 1;
        num <<= 31;

        while (n > 0)
        {
            result += (n & 1) * num;
            n >>= 1;
            num >>= 1;
        }

        return result;
    }
}