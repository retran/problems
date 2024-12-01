public class Codec
{
    public string serialize(TreeNode root)
    {
        if (root == null)
        {
            return "null";
        }

        var sb = new StringBuilder();
        var queue = new Queue<TreeNode>();

        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current == null)
            {
                sb.Append("null,");
            }
            else
            {
                sb.Append(current.val);
                sb.Append(",");
                queue.Enqueue(current.left);
                queue.Enqueue(current.right);
            }
        }

        sb.Remove(sb.Length - 1, 1);

        return sb.ToString();
    }

    public TreeNode deserialize(string data)
    {
        var values = data.Split(',');

        if (values[0] == "null")
        {
            return null;
        }

        var root = new TreeNode(int.Parse(values[0]));
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        int i = 1;
        while (queue.Count > 0 && i < values.Length)
        {
            var current = queue.Dequeue();

            if (values[i] != "null")
            {
                var leftNode = new TreeNode(int.Parse(values[i]));
                current.left = leftNode;
                queue.Enqueue(leftNode);
            }
            i++;

            if (i < values.Length && values[i] != "null")
            {
                var rightNode = new TreeNode(int.Parse(values[i]));
                current.right = rightNode;
                queue.Enqueue(rightNode);
            }
            i++;
        }

        return root;
    }
}
