using System;
using System.Collections.Generic;

public class Solution {
    public int KthSmallest(int[][] matrix, int k) {
        int n = matrix.Length;
        PriorityQueue<(int val, int row, int col), int> minHeap = new PriorityQueue<(int val, int row, int col), int>();

        for (int r = 0; r < n && r < k; r++) {
            minHeap.Enqueue((matrix[r][0], r, 0), matrix[r][0]);
        }

        int count = 0;
        int result = 0;

        while (count < k) {
            var (value, row, col) = minHeap.Dequeue();
            result = value;
            count++;

            if (col + 1 < n) {
                minHeap.Enqueue((matrix[row][col + 1], row, col + 1), matrix[row][col + 1]);
            }
        }

        return result;
    }
}
