using System;
using System.Collections.Generic;

public class Solution {
    public int ConnectSticks(int[] sticks) {
        PriorityQueue<int, int> minHeap = new PriorityQueue<int, int>();
        foreach (int stick in sticks) {
            minHeap.Enqueue(stick, stick);
        }

        int totalCost = 0;

        while (minHeap.Count > 1) {
            int first = minHeap.Dequeue();
            int second = minHeap.Dequeue();
            int cost = first + second;
            totalCost += cost;
            minHeap.Enqueue(cost, cost);
        }

        return totalCost;
    }
}
