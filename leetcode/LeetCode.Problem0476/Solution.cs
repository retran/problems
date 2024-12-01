public class Solution
{
    public int FindComplement(int num)
    {
        int complement = 0;
        int val = 1;
        while (num > 0)
        {
            if ((num & 1) == 0)
            {
                complement += val;
            }

            num = num >> 1;
            val = val << 1;
        }

        return complement;
    }
}