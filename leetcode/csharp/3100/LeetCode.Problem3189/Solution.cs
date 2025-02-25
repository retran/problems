using System;

public class Solution {
    public int MinMoves(int[][] rooks) {
        int n = rooks.Length;
        int[] rows = new int[n];
        int[] cols = new int[n];
        
        for (int i = 0; i < n; i++) {
            rows[i] = rooks[i][0];
            cols[i] = rooks[i][1];
        }
        
        Array.Sort(rows);
        Array.Sort(cols);
        
        int minMoves = 0;
        for (int i = 0; i < n; i++) {
            minMoves += Math.Abs(rows[i] - i) + Math.Abs(cols[i] - i);
        }
        
        return minMoves;
    }
}
