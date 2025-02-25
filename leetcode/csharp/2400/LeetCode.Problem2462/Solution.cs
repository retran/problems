public class Solution
{
    public long TotalCost(int[] costs, int k, int candidates)
    {
        if (costs.Length < k)
        {
            return -1;
        }

        var leftQueue = new PriorityQueue<int, int>();
        var rightQueue = new PriorityQueue<int, int>();

        int left = candidates - 1;
        int right = costs.Length - candidates;
        if (left < right)
        {
            for (int i = 0; i < candidates; i++)
            {
                leftQueue.Enqueue(costs[i], costs[i]);
            }

            for (int i = costs.Length - candidates; i < costs.Length; i++)
            {
                rightQueue.Enqueue(costs[i], costs[i]);
            }
        }
        else
        {
            for (int i = 0; i < candidates; i++)
            {
                leftQueue.Enqueue(costs[i], costs[i]);
            }

            for (int i = candidates; i < costs.Length; i++)
            {
                leftQueue.Enqueue(costs[i], costs[i]);
            }
        }

        long totalCost = 0;

        while (k > 0)
        {
            if (rightQueue.Count == 0 || (leftQueue.Count > 0 && leftQueue.Peek() <= rightQueue.Peek()))
            {
                totalCost += leftQueue.Dequeue();
                if (left + 1 < right)
                {
                    left++;
                    leftQueue.Enqueue(costs[left], costs[left]);
                }
            }
            else
            {
                totalCost += rightQueue.Dequeue();
                if (left + 1 < right)
                {
                    right--;
                    rightQueue.Enqueue(costs[right], costs[right]);
                }
            }

            k--;
        }

        return totalCost;
    }
}