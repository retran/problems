public class Solution
{
    public IList<IList<int>> Permute(int[] nums)
    {
        var result = new List<IList<int>>();

        Queue<List<int>> queue = new Queue<List<int>>();
        foreach (var num in nums)
        {
            queue.Enqueue(new List<int> { num });
        }

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current.Count == nums.Length)
            {
                result.Add(current);
            }
            else
            {
                foreach (var num in nums)
                {
                    if (!current.Contains(num))
                    {
                        var next = new List<int>(current);
                        next.Add(num);
                        queue.Enqueue(next);
                    }
                }
            }
        }

        return result;
    }
}