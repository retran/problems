public class Solution {
    public int[][] MatrixBlockSum(int[][] mat, int k) 
    {
        int m = mat.Length;
        int n = mat[0].Length;
        int[][] prefixSum = new int[m + 1][];
        for (int i = 0; i <= m; i++) 
        {
            prefixSum[i] = new int[n + 1];
        }
        
        // Build prefix sum array
        for (int i = 1; i <= m; i++) {
            for (int j = 1; j <= n; j++) {
                prefixSum[i][j] = mat[i-1][j-1] + prefixSum[i-1][j] + prefixSum[i][j-1] - prefixSum[i-1][j-1];
            }
        }

        int[][] result = new int[m][];
        for (int i = 0; i < m; i++) {
            result[i] = new int[n];
            for (int j = 0; j < n; j++) {
                int r1 = Math.Max(0, i - k);
                int c1 = Math.Max(0, j - k);
                int r2 = Math.Min(m - 1, i + k);
                int c2 = Math.Min(n - 1, j + k);
                
                result[i][j] = prefixSum[r2 + 1][c2 + 1]
                                - (r1 > 0 ? prefixSum[r1][c2 + 1] : 0)
                                - (c1 > 0 ? prefixSum[r2 + 1][c1] : 0)
                                + (r1 > 0 && c1 > 0 ? prefixSum[r1][c1] : 0);
            }
        }

        return result;
    }
}