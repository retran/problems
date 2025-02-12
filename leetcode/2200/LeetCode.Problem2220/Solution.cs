public class Solution
{
    public int MinBitFlips(int start, int goal)
    {
        int flipCount = 0;

        for (int i = 0; i < 32; i++)
        {
            int startBit = (start >> i) & 1;
            int goalBit = (goal >> i) & 1;

            if (startBit != goalBit)
            {
                flipCount++;
            }
        }

        return flipCount;
    }
}