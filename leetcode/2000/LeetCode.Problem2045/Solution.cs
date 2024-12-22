public class Solution
{
    public int SecondMinimum(int n, int[][] edges, int time, int change)
    {
        var connections = BuildGraph(edges);
        var (firstDist, secondDist) = CalculateDistances(n, connections);

        int minEdgesToSecondShortest = secondDist[n] != int.MaxValue ? secondDist[n] : firstDist[n] + 2;
        return CalculateTravelTime(minEdgesToSecondShortest, time, change);
    }

    private Dictionary<int, HashSet<int>> BuildGraph(int[][] edges)
    {
        var connections = new Dictionary<int, HashSet<int>>();
        foreach (var edge in edges)
        {
            if (!connections.ContainsKey(edge[0]))
            {
                connections[edge[0]] = new HashSet<int>();
            }
            if (!connections.ContainsKey(edge[1]))
            {
                connections[edge[1]] = new HashSet<int>();
            }

            connections[edge[0]].Add(edge[1]);
            connections[edge[1]].Add(edge[0]);
        }
        return connections;
    }

    private (Dictionary<int, int> firstDist, Dictionary<int, int> secondDist) CalculateDistances(int n, Dictionary<int, HashSet<int>> connections)
    {
        var firstDist = new Dictionary<int, int>();
        var secondDist = new Dictionary<int, int>();
        for (int i = 1; i <= n; i++)
        {
            firstDist[i] = int.MaxValue;
            secondDist[i] = int.MaxValue;
        }

        var queue = new Queue<int[]>();
        queue.Enqueue(new int[] { 1, 0 });
        firstDist[1] = 0;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            int node = current[0];
            int dist = current[1];

            foreach (var next in connections[node])
            {
                int newDist = dist + 1;

                if (newDist < firstDist[next])
                {
                    secondDist[next] = firstDist[next];
                    firstDist[next] = newDist;
                    queue.Enqueue(new int[] { next, newDist });
                }
                else if (newDist > firstDist[next] && newDist < secondDist[next])
                {
                    secondDist[next] = newDist;
                    queue.Enqueue(new int[] { next, newDist });
                }
            }
        }
        return (firstDist, secondDist);
    }

    private int CalculateTravelTime(int edgesCount, int time, int change)
    {
        int timeCount = 0;

        while (edgesCount > 0)
        {
            int newTimeCount = timeCount + time;
            bool isGreenLight = (newTimeCount / change) % 2 == 0;

            if (!isGreenLight && edgesCount > 1)
            {
                newTimeCount = newTimeCount / change * change + change;
            }

            timeCount = newTimeCount;
            edgesCount--;
        }

        return timeCount;
    }
}
