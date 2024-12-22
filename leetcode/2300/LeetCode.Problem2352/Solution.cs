using System.Reflection;

public class Solution
{
    public int EqualPairs(int[][] grid)
    {
        int n = grid.Length;

        long a = 31;
        long mod = 10000007;

        long[] rowHashes = new long[n];
        long[] columnHashes = new long[n];

        int count = 0;

        for (int i = 0; i < n; i++)
        {
            long rowHash = 0;
            long columnHash = 0;
            for (int j = 0; j < n; j++)
            {
                rowHash = (rowHash * a % mod + grid[i][j]) % mod;
                columnHash = (columnHash * a % mod + grid[j][i]) % mod;
            }
            rowHashes[i] = rowHash;
            columnHashes[i] = columnHash;
        }

        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
            {
                if (rowHashes[i] != columnHashes[j])
                {
                    continue;
                }

                bool equals = true;
                for (int k = 0; k < n; k++)
                {
                    if (grid[i][k] != grid[k][j])
                    {
                        equals = false;
                        break;
                    }
                }

                if (equals)
                {
                    count++;
                }
            }

        return count;
    }
}