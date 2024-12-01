public class Solution
{
    public int Candy(int[] ratings)
    {
        int[] candiesL = new int[ratings.Length];
        int[] candiesR = new int[ratings.Length];

        candiesL[0] = 1;
        for (int i = 1; i < ratings.Length; i++)
        {
            if (ratings[i] > ratings[i - 1])
            {
                candiesL[i] = candiesL[i - 1] + 1;
            }
            else
            {
                candiesL[i] = 1;
            }
        }

        candiesR[ratings.Length - 1] = 1;
        for (int i = ratings.Length - 2; i >= 0; i--)
        {
            if (ratings[i] > ratings[i + 1])
            {
                candiesR[i] = candiesR[i + 1] + 1;
            }
            else
            {
                candiesR[i] = 1;
            }
        }

        int sum = 0;
        for (int i = 0; i < ratings.Length; i++)
        {
            sum += int.Max(candiesL[i], candiesR[i]);
        }

        return sum;
    }
}