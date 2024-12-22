public class Solution
{
    public int MaxDepth(Node root)
    {
        int levels = 0;

        if (root == null)
        {
            return levels;
        }

        var queue = new Queue<Node>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            int levelSize = queue.Count;

            levels++;

            while (levelSize > 0)
            {
                levelSize--;
                var current = queue.Dequeue();

                if (current == null)
                {
                    continue;
                }

                if (current.children == null)
                {
                    continue;
                }

                foreach (var child in current.children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        return levels;
    }
}