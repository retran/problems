public class Solution
{
    public double FrogPosition(int n, int[][] edges, int t, int target)
    {
        if (n == 1)
        {
            return target == 1 ? 1 : 0;
        }

        var tree = new Dictionary<int, ISet<int>>();

        foreach (var edge in edges)
        {
            if (!tree.ContainsKey(edge[0]))
            {
                tree[edge[0]] = new HashSet<int>();
            }
            if (!tree.ContainsKey(edge[1]))
            {
                tree[edge[1]] = new HashSet<int>();
            }
            tree[edge[0]].Add(edge[1]);
            tree[edge[1]].Add(edge[0]);
        }

        var visited = new HashSet<int>();
        var queue = new Queue<int>();
        var probabilities = new Dictionary<int, double>();
        queue.Enqueue(1);

        probabilities[1] = 1.0;
        while (queue.Count > 0 && t > 0)
        {
            var size = queue.Count;
            for (var i = 0; i < size; i++)
            {
                var node = queue.Dequeue();
                visited.Add(node);

                var children = tree[node];

                var count = node == 1 
                    ? children.Count 
                    : children.Count - 1;

                var childProbability = probabilities[node] / count;

                foreach (var child in children)
                {
                    if (!visited.Contains(child))
                    {
                        queue.Enqueue(child);
                        probabilities[child] = childProbability;
                    }
                }

                if (count > 0)
                {
                    probabilities[node] = 0;
                }
            }

            t--;
        }

        return probabilities.ContainsKey(target) 
            ? probabilities[target] 
            : 0;
    }
}