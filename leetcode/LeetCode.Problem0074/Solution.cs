public class Solution
{
    public bool SearchMatrix(int[][] matrix, int target)
    {
        int row = 0;
        int col = matrix[0].Length - 1;
        while (row < matrix.Length && col >= 0)
        {
            if (matrix[row][col] == target)
            {
                return true;
            }
            if (matrix[row][col] < target)
            {
                row++;
            }
            else
            {
                col--;
            }
        }
        return false;
    }
}