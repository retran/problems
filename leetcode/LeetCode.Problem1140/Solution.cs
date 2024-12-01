public class Solution {
    public int StoneGameII(int[] piles) {
        int n = piles.Length;
        int[,] dp = new int[n, n + 1]; // dp[i][m] where n+1 to handle bounds easily
        int[] suffixSum = new int[n];
        
        suffixSum[n - 1] = piles[n - 1];
        for (int i = n - 2; i >= 0; i--) {
            suffixSum[i] = piles[i] + suffixSum[i + 1];
        }
        
        for (int i = n - 1; i >= 0; i--) {
            for (int m = 1; m <= n; m++) {
                if (i + 2 * m >= n) {
                    dp[i, m] = suffixSum[i]; // take all remaining stones
                } else {
                    for (int x = 1; x <= 2 * m; x++) {
                        dp[i, m] = Math.Max(dp[i, m], suffixSum[i] - dp[i + x, Math.Max(m, x)]);
                    }
                }
            }
        }
        
        return dp[0, 1];
    }
}
