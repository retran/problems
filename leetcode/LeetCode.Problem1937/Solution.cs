public class Solution
{
    public long MaxPoints(int[][] points)
    {
        int rows = points.Length;
        int columns = points[0].Length;

        long[] prevRow = new long[columns];
        long[] currentRow = new long[columns];

        for (int j = 0; j < columns; j++)
        {
            currentRow[j] = points[0][j];
        }

        long[] left = new long[columns];
        long[] right = new long[columns];

        for (int i = 1; i < rows; i++)
        {
            var tmp = prevRow;
            prevRow = currentRow;
            currentRow = tmp;

            left[0] = prevRow[0];
            right[columns - 1] = prevRow[columns - 1];

            for (int j = 1; j < columns; j++)
            {
                left[j] = Math.Max(prevRow[j], left[j - 1] - 1);
            }

            for (int j = columns - 2; j >= 0; j--)
            {
                right[j] = Math.Max(prevRow[j], right[j + 1] - 1);
            }

            for (int j = 0; j < columns; j++)
            {
                currentRow[j] = Math.Max(left[j], right[j]) + points[i][j];
            }
        }

        long max = currentRow[0];
        for (int j = 1; j < columns; j++)
        {
            max = Math.Max(max, currentRow[j]);
        }

        return max;
    }
}