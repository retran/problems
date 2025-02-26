public class Solution
{
    public int HammingDistance(int x, int y)
    {
        int differentBits = 0;

        for (int i = 0; i < 32; i++)
        {
            if (((x >> i) & 1) != ((y >> i) & 1))
            {
                differentBits++;
            }
        }

        return differentBits;
    }
}