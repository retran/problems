using System;
using System.Collections.Generic;

public class Solution {
    public int[][] KClosest(int[][] points, int k) {
        // Max-Heap to store the k closest points
        PriorityQueue<(int distance, int[] point), int> maxHeap = new PriorityQueue<(int distance, int[] point), int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));

        // Step 1: Add the first k points to the max-heap.
        for (int i = 0; i < points.Length; i++) {
            int distance = points[i][0] * points[i][0] + points[i][1] * points[i][1];
            maxHeap.Enqueue((distance, points[i]), distance);

            // If heap size exceeds k, remove the farthest point (root of the heap)
            if (maxHeap.Count > k) {
                maxHeap.Dequeue();
            }
        }

        // Step 2: Extract the k closest points from the heap.
        int[][] result = new int[k][];
        for (int i = 0; i < k; i++) {
            result[i] = maxHeap.Dequeue().point;
        }

        return result;
    }
}
