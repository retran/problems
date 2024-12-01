public class Solution
{
    public void SetZeroes(int[][] matrix) 
    {
        bool firstRowZero = false;
        bool firstColZero = false;

        for (int i = 0; i < matrix.Length; i++) 
        {
            for (int j = 0; j < matrix[0].Length; j++) 
            {
                if (matrix[i][j] == 0) 
                {
                    if (i == 0) firstRowZero = true;
                    if (j == 0) firstColZero = true;
                    matrix[i][0] = 0;
                    matrix[0][j] = 0;
                }
            }
        }

        for (int i = 1; i < matrix.Length; i++) 
        {
            for (int j = 1; j < matrix[0].Length; j++) 
            {
                if (matrix[i][0] == 0 || matrix[0][j] == 0) 
                {
                    matrix[i][j] = 0;
                }
            }
        }

        if (firstRowZero) 
        {
            for (int j = 0; j < matrix[0].Length; j++) 
            {
                matrix[0][j] = 0;
            }
        }
        if (firstColZero) 
        {
            for (int i = 0; i < matrix.Length; i++) 
            {
                matrix[i][0] = 0;
            }
        }
    }
}