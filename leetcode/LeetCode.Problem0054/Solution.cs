// https://leetcode.com/problems/

public class Solution
{
    public IList<int> SpiralOrder(int[][] matrix)
    {
                var result = new List<int>();
        int nmin = 0;
        int mmin = 0;
        int nmax = matrix.Length - 1;
        int mmax = matrix[0].Length - 1;

        while (nmin <= nmax && mmin <= mmax)
        {
            // Traverse right
            for (int j = mmin; j <= mmax; j++) 
            {
                result.Add(matrix[nmin][j]);
            }
            nmin++;

            // Traverse down
            for (int i = nmin; i <= nmax; i++)
            {
                result.Add(matrix[i][mmax]);
            }
            mmax--;

            if (nmin <= nmax) 
            {
                // Traverse left
                for (int j = mmax; j >= mmin; j--) 
                {
                    result.Add(matrix[nmax][j]);
                }
                nmax--;
            }

            if (mmin <= mmax) 
            {
                // Traverse up
                for (int i = nmax; i >= nmin; i--) 
                {
                    result.Add(matrix[i][mmin]);
                }
                mmin++;
            }
        }

        return result;
    }
}