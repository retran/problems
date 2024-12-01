public class Solution
{
    public Node Connect(Node root)
    {
        if (root == null)
        {
            return root;
        }

        var queue = new Queue<Node>();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            var size = queue.Count;
            Node prev = null;
            for (int i = 0; i < size; i++)
            {
                var current = queue.Dequeue();
                if (prev != null)
                {
                    prev.next = current;
                }

                prev = current;
                if (current.left != null)
                {
                    queue.Enqueue(current.left);
                }

                if (current.right != null)
                {
                    queue.Enqueue(current.right);
                }
            }
        }

        return root;
    }
}