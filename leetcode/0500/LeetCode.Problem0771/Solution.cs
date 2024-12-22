public class Solution
{
    public int NumJewelsInStones(string jewels, string stones)
    {
        var jewelsSet = new HashSet<char>();

        for (int i = 0; i < jewels.Length; i++)
        {
            jewelsSet.Add(jewels[i]);
        }

        int count = 0;

        for (int i = 0; i < stones.Length; i++)
        {
            if (jewelsSet.Contains(stones[i]))
            {
                count++;
            }
        }

        return count;
    }
}