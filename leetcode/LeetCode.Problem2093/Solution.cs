using System;
using System.Collections.Generic;

public class Solution
{
    public int MinimumCost(int n, int[][] highways, int discounts)
    {
        var connections = new int[n, n];
        var costs = new int[n, discounts + 1];

        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                connections[i, j] = -1;

        for (int i = 0; i < n; i++)
            for (int j = 0; j <= discounts; j++)
                costs[i, j] = int.MaxValue;

        foreach (var highway in highways)
        {
            connections[highway[0], highway[1]] = highway[2];
            connections[highway[1], highway[0]] = highway[2];
        }

        var pq = new PriorityQueue<(int cost, int node, int remainingDiscounts), int>();
        pq.Enqueue((0, 0, discounts), 0);

        costs[0, discounts] = 0;

        while (pq.Count > 0)
        {
            var (currentCost, currentNode, remainingDiscounts) = pq.Dequeue();

            if (currentNode == n - 1)
            {
                return currentCost;
            }

            for (int i = 0; i < n; i++)
            {
                if (i == currentNode || connections[currentNode, i] == -1)
                {
                    continue;
                }

                int fullCost = connections[currentNode, i];
                int discountedCost = fullCost / 2;

                if (costs[i, remainingDiscounts] > currentCost + fullCost)
                {
                    costs[i, remainingDiscounts] = currentCost + fullCost;
                    pq.Enqueue((costs[i, remainingDiscounts], i, remainingDiscounts), costs[i, remainingDiscounts]);
                }

                if (remainingDiscounts > 0 && costs[i, remainingDiscounts - 1] > currentCost + discountedCost)
                {
                    costs[i, remainingDiscounts - 1] = currentCost + discountedCost;
                    pq.Enqueue((costs[i, remainingDiscounts - 1], i, remainingDiscounts - 1), costs[i, remainingDiscounts - 1]);
                }
            }
        }

        return -1;
    }
}
