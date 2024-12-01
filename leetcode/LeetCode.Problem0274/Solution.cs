public class Solution
{
    public int HIndex(int[] citations)
    {
        int[] citationsIndex = new int[1001];
        for (int i = 0; i < citations.Length; i++)
        {
            citationsIndex[citations[i]]++;
        }

        int citationsNumber = citationsIndex[1000];
        for (int i = 999; i >= 0; i--) {
            citationsNumber += citationsIndex[i];
            if (citationsNumber >= i)
            {
                return i;
            }
        }

        return 0;
    }
}