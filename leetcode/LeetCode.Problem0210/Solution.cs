public class Solution
{
    public int[] FindOrder(int numCourses, int[][] prerequisites)
    {
        int count = 0;
        var graph = new Dictionary<int, List<int>>();
        var indegree = new int[numCourses];
        for (int i = 0; i < prerequisites.Length; i++)
        {
            var prerequisite = prerequisites[i];
            var course = prerequisite[0];
            var pre = prerequisite[1];

            if (!graph.ContainsKey(pre))
            {
                graph[pre] = new List<int>();
            }

            graph[pre].Add(course);
            indegree[course]++;
        }

        var queue = new Queue<int>();
        for (int i = 0; i < numCourses; i++)
        {
            if (indegree[i] == 0)
            {
                queue.Enqueue(i);
            }
        }

        var result = new List<int>();
        while (queue.Count > 0)
        {
            var course = queue.Dequeue();
            result.Add(course);
            count++;
            if (graph.ContainsKey(course))
            {
                foreach (var next in graph[course])
                {
                    indegree[next]--;
                    if (indegree[next] == 0)
                    {
                        queue.Enqueue(next);
                    }
                }
            }
        }

        if (count == numCourses)
        {
            return result.ToArray();
        }
        else
        {
            return Array.Empty<int>();
        }
    }
}