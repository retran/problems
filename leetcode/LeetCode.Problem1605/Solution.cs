public class Solution
{
    public int[][] RestoreMatrix(int[] rowSum, int[] colSum)
    {
        int n = rowSum.Length;
        int m = colSum.Length;
        int[][] result = new int[n][];

        for (int i = 0; i < n; i++)
        {
            result[i] = new int[m];
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                result[i][j] = Math.Min(rowSum[i], colSum[j]);
                rowSum[i] -= result[i][j];
                colSum[j] -= result[i][j];
            }
        }

        return result;
    }
}