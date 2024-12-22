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
            int size = queue.Count;

            Node prevNode = null;
            while (size > 0)
            {
                var node = queue.Dequeue();
                if (prevNode != null)
                {
                    prevNode.next = node;
                }

                if (node.left != null)
                {
                    queue.Enqueue(node.left);
                }

                if (node.right != null)
                {
                    queue.Enqueue(node.right);
                }

                prevNode = node;
                size--;
            }
        }

        return root;
    }
}