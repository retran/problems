public class Solution
{
    public Node FindRoot(List<Node> tree)
    {
        int acc = 0;

        foreach (var node in tree)
        {
            acc = acc ^ node.val;
            foreach (var child in node.children)
            {
                acc = acc ^ child.val;
            }
        }

        foreach (var node in tree)
        {
            if (node.val == acc)
            {
                return node;
            }
        }

        return null;
    }
}