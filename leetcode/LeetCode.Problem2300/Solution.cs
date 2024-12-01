public class Solution
{
    public int[] SuccessfulPairs(int[] spells, int[] potions, long success)
    {
        var successfulPairs = new int[spells.Length];
        Array.Sort(potions);

        int maxPotion = potions[potions.Length - 1];

        for (int i = 0; i < spells.Length; i++)
        {
            long minPotion = (long)Math.Ceiling(success / (double)spells[i]);
            if (minPotion > maxPotion)
            {
                continue;
            }

            int left = 0;
            int right = potions.Length;
            while (left < right)
            {
                int mid = left + (right - left) / 2;
                if (potions[mid] < minPotion)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid;
                }
            }

            successfulPairs[i] = potions.Length - left;
        }

        return successfulPairs;
    }
}