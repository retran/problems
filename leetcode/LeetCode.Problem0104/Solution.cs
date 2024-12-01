public class Solution
{
    public int MaxDepth(TreeNode root)
    {
        int max = 0;
        var stack = new Stack<(TreeNode, int)>();
        stack.Push((root, 1));
        while (stack.Count > 0)
        {
            var (node, depth) = stack.Pop();
            if (node == null)
            {
                continue;
            }

            max = Math.Max(max, depth);
            stack.Push((node.left, depth + 1));
            stack.Push((node.right, depth + 1));
        }
        return max;
    }
}