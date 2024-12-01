public class Solution
{
    public IList<int> PostorderTraversal(TreeNode root)
    {
        var traversal = new List<int>();

        if (root == null)
        {
            return traversal;
        }

        var stack = new Stack<TreeNode>();
        var current = root;

        while (current != null || stack.Count > 0)
        {
            while (current != null)
            {
                if (current.right != null)
                {
                    stack.Push(current.right);
                }
                stack.Push(current);
                current = current.left;
            }

            current = stack.Pop();
            if (stack.Count > 0 && current.right == stack.Peek())
            {
                var right = stack.Pop();
                stack.Push(current);
                current = right;
            }
            else
            {
                traversal.Add(current.val);
                current = null;
            }
        }

        return traversal;
    }
}