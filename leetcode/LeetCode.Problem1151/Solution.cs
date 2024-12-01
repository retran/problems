public class Solution
{
    public int MinSwaps(int[] data)
    {
        int k = 0;
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] == 1)
            {
                k++;
            }
        }

        int zeroes = 0;

        for (int i = 0; i < k; i++)
        {
            if (data[i] == 0)
            {
                zeroes++;
            }
        }

        int minZeroes = zeroes;

        int j = k;
        while (j < data.Length)
        {
            if (data[j - k] == 0)
            {
                zeroes--;
            }

            if (data[j] == 0)
            {
                zeroes++;
            }

            if (zeroes < minZeroes)
            {
                minZeroes = zeroes;
            }

            j++;
        }

        return minZeroes;
    }
}