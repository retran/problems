public class Solution
{
    public int DiagonalSum(int[][] mat)
    {
        bool excludeCenter = mat.Length % 2 == 1;
        int center = mat.Length / 2;

        int sum = 0;
        for (int i = 0; i < mat.Length; i++)
        {
            sum += mat[i][i];

            if (!excludeCenter || i != center)
            {
                sum += mat[i][mat[0].Length - i - 1];
            }
        }

        return sum;
    }
}