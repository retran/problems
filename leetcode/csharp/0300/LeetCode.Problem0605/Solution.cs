public class Solution
{
    public bool CanPlaceFlowers(int[] flowerbed, int n)
    {
        if (n == 0)
        {
            return true;
        }

        int count = 0;
        for (int i = 0; i < flowerbed.Length; i++)
        {
            if (flowerbed[i] == 0)
            {
                int prev = i == 0 ? 0 : flowerbed[i - 1];
                int next = i == flowerbed.Length - 1 ? 0 : flowerbed[i + 1];
                if (prev == 0 && next == 0)
                {
                    flowerbed[i] = 1;
                    n--;
                    if (n == 0) 
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}