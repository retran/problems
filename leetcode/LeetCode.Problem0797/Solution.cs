public class Solution
{
    public IList<IList<int>> AllPathsSourceTarget(int[][] graph)
    {
        int n = graph.Length;
        int source = 0;
        int destination = n - 1;

        var routes = new List<IList<int>>();

        var stack = new Stack<(int, List<int>)>();
        stack.Push((source, new List<int>() { source }));

        while (stack.Count > 0)
        {
            var (current, route) = stack.Pop();

            foreach (var next in graph[current])
            {
                if (route.Contains(next))
                {
                    continue;
                }

                var newRoute = new List<int>(route);
                newRoute.Add(next);

                if (next == destination)
                {
                    routes.Add(newRoute);
                    continue;
                }

                stack.Push((next, newRoute));
            }
        }

        return routes;
    }
}
