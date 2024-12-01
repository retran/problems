public class Solution {
    public int StrangePrinter(string s) {
        int n = s.Length;
        if (n == 0) return 0;

        int[,] dp = new int[n, n];

        for (int i = 0; i < n; i++) {
            dp[i, i] = 1; // Single character case
        }

        for (int len = 2; len <= n; len++) {
            for (int i = 0; i <= n - len; i++) {
                int j = i + len - 1;
                dp[i, j] = dp[i, j - 1] + 1;

                for (int k = i; k < j; k++) {
                    int total = dp[i, k] + dp[k + 1, j];
                    if (s[k] == s[j]) {
                        total--;
                    }
                    dp[i, j] = Math.Min(dp[i, j], total);
                }
            }
        }

        return dp[0, n - 1];
    }
}
