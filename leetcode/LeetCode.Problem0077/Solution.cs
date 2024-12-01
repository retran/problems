public class Solution
{
    public IList<IList<int>> Combine(int n, int k)
    {
        var result = new List<IList<int>>();

        Queue<List<int>> queue = new Queue<List<int>>();
        for (int i = 1; i <= n; i++)
        {
            queue.Enqueue(new List<int> { i });
        }
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current.Count == k)
            {
                result.Add(current);
            }
            else
            {
                for (int i = current[current.Count - 1] + 1; i <= n; i++)
                {
                    var next = new List<int>(current);
                    next.Add(i);
                    queue.Enqueue(next);
                }
            }
        }

        return result;
    }
}