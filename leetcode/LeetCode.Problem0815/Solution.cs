public class Solution
{
    public int NumBusesToDestination(int[][] routes, int source, int target)
    {
        var originalRoutes = new HashSet<string>();
        var stopToRouteMap = new Dictionary<int, List<int>>();

        for (int i = 0; i < routes.Length; i++)
        {
            var key = string.Join(",", routes[i].OrderBy(v => v));
            if (!originalRoutes.Add(key))
            {
                continue;
            }

            for (int j = 0; j < routes[i].Length; j++)
            {
                int stop = routes[i][j];
                if (!stopToRouteMap.TryGetValue(stop, out var list))
                {
                    list = new List<int>();
                }

                list.Add(i);
                stopToRouteMap[stop] = list;
            }
        }

        var originalStops = new HashSet<string>();
        var keys = stopToRouteMap.Keys.ToArray();
        foreach (var key in keys)
        {
            if (key != source && key != target && stopToRouteMap.TryGetValue(key, out var stopRoutes))
            {
                if (stopRoutes.Count == 1)
                {
                    stopToRouteMap.Remove(key);
                    continue;
                }

                string hashKey = string.Join(",", stopRoutes.OrderBy(v => v));
                if (!originalStops.Add(hashKey))
                {
                    stopToRouteMap.Remove(key);
                }
            }
        }

        var visited = new HashSet<int>();
        var queue = new Queue<(int, int)>();

        queue.Enqueue((source, 0));
        visited.Add(source);

        while (queue.Count > 0)
        {
            var (currentStop, currentBusCount) = queue.Dequeue();

            if (currentStop == target)
            {
                return currentBusCount;
            }

            visited.Add(currentStop);

            if (stopToRouteMap.TryGetValue(currentStop, out var currentRoutes))
            {
                foreach (var currentRoute in currentRoutes)
                {
                    var stops = routes[currentRoute];
                    foreach (var newStop in stops)
                    {
                        if (!visited.Contains(newStop)
                            && stopToRouteMap.TryGetValue(newStop, out var newRoutes))
                        {
                            queue.Enqueue((newStop, currentBusCount + 1));
                            visited.Add(newStop);
                        }
                    }
                }
            }
        }

        return -1;
    }
}