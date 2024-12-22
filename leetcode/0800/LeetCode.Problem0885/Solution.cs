public class Solution
{
    public int[][] SpiralMatrixIII(int rows, int cols, int rStart, int cStart)
    {
        int totalCells = rows * cols;
        int[][] result = new int[totalCells][];
        int count = 0;

        int[] dr = new int[] { 0, 1, 0, -1 };
        int[] dc = new int[] { 1, 0, -1, 0 };

        int steps = 1;
        int dirIndex = 0;
        int r = rStart, c = cStart;

        if (IsWithinGrid(r, c, rows, cols))
        {
            result[count++] = new int[] { r, c };
        }

        while (count < totalCells)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < steps; j++)
                {
                    r += dr[dirIndex];
                    c += dc[dirIndex];

                    if (IsWithinGrid(r, c, rows, cols))
                    {
                        result[count++] = new int[] { r, c };
                        if (count == totalCells)
                            return result;
                    }
                }
                dirIndex = (dirIndex + 1) % 4;

                if (i % 2 == 1)
                {
                    steps++;
                }
            }
        }

        return result;
    }

    private bool IsWithinGrid(int r, int c, int rows, int cols)
    {
        return r >= 0 && r < rows && c >= 0 && c < cols;
    }
}
