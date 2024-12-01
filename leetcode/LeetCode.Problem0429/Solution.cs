public class Solution
{
    public IList<IList<int>> LevelOrder(Node root)
    {
        var levels = new List<IList<int>>();

        if (root == null)
        {
            return levels;
        }

        var queue = new Queue<Node>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var level = new List<int>();
            int levelSize = queue.Count;

            while (levelSize > 0)
            {
                levelSize--;
                var current = queue.Dequeue();

                if (current == null)
                {
                    continue;
                }

                level.Add(current.val);

                if (current.children == null)
                {
                    continue;
                }

                foreach (var child in current.children)
                {
                    queue.Enqueue(child);
                }
            }

            levels.Add(level);
        }

        return levels;
    }
}