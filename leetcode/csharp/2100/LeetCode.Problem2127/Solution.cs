public class Solution {
    public int MaximumInvitations(int[] favorite) {
        int n = favorite.Length;
        
        List<int>[] rev = new List<int>[n];
        int[] inDegree = new int[n];
        for (int i = 0; i < n; i++) {
            rev[i] = new List<int>();
        }
        for (int i = 0; i < n; i++) {
            int j = favorite[i];
            rev[j].Add(i);
            inDegree[j]++;
        }
        
        int[] dp = new int[n];
        int[] inDegCopy = new int[n];
        Array.Copy(inDegree, inDegCopy, n);
        Queue<int> queue = new Queue<int>();
        for (int i = 0; i < n; i++) {
            if (inDegCopy[i] == 0)
                queue.Enqueue(i);
        }
        while (queue.Count > 0) {
            int cur = queue.Dequeue();
            int j = favorite[cur];
            dp[j] = Math.Max(dp[j], dp[cur] + 1);
            inDegCopy[j]--; 
            if (inDegCopy[j] == 0)
                queue.Enqueue(j);
        }
        
        int twoCycleSum = 0;
        for (int i = 0; i < n; i++) {
            int j = favorite[i];
            if (favorite[j] == i && i < j) {
                twoCycleSum += dp[i] + dp[j] + 2;
            }
        }
        
        bool[] visited = new bool[n];
        int cycleMax = 0;
        for (int i = 0; i < n; i++) {
            if (inDegCopy[i] > 0 && !visited[i]) {
                int cur = i, cnt = 0;
                while (!visited[cur]) {
                    visited[cur] = true;
                    cur = favorite[cur];
                    cnt++;
                }
                cycleMax = Math.Max(cycleMax, cnt);
            }
        }
        
        return Math.Max(cycleMax, twoCycleSum);
    }
}
