public class Solution
{
    public IList<int> PreorderTraversal(TreeNode root)
    {
        var traversal = new List<int>();

        if (root == null)
        {
            return traversal;
        }

        var stack = new Stack<TreeNode>();
        var current = root;

        while (stack.Count > 0 || current != null)
        {
            if (current != null)
            {
                stack.Push(current);
                traversal.Add(current.val);
                current = current.left;
            }
            else
            {
                TreeNode poppedNode = stack.Pop();
                current = poppedNode.right;
            }
        }

        return traversal;
    }
}