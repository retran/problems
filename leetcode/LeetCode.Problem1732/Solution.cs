public class Solution
{
    public int LargestAltitude(int[] gain)
    {
        int max = 0;
        int current = 0;
        for (int i = 0; i < gain.Length; i++)
        {
            current += gain[i];
            if (current > max)
            {
                max = current;
            }
        }
        return max;
    }
}