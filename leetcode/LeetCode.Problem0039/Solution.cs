public class Solution
{
    public IList<IList<int>> CombinationSum(int[] candidates, int target)
    {
        var result = new List<IList<int>>();

        Queue<List<int>> queue = new Queue<List<int>>();
        foreach (var candidate in candidates)
        {
            queue.Enqueue(new List<int> { candidate });
        }

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var sum = current.Sum();
            if (sum == target)
            {
                result.Add(current);
            }
            else if (sum < target)
            {
                foreach (var candidate in candidates.Where(c => c >= current[current.Count - 1]))
                {
                    var next = new List<int>(current);
                    next.Add(candidate);
                    queue.Enqueue(next);
                }
            }
        }

        return result;
    }
}