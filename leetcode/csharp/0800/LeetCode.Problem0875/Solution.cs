public class Solution
{
    public long GetTimeToEatAllBananas(int[] piles, int k)
    {
        long time = 0;
        for (int i = 0; i < piles.Length; i++)
        {
            time += (piles[i] + k - 1) / k;
        }

        return time;
    }

    public int MinEatingSpeed(int[] piles, int h)
    {
        int left = 1;
        int right = piles.Max();

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            long time = GetTimeToEatAllBananas(piles, mid);

            if (time <= h)
            {
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
            }
        }

        return left;
    }
}
