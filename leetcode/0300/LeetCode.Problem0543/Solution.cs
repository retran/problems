public class Solution
{
    public int DiameterOfBinaryTree(TreeNode root)
    {
        int diameter = 0;
        GetDepth(root, ref diameter);
        return diameter;

        int GetDepth(TreeNode node, ref int diameter)
        {
            if (node == null) return 0;

            int leftDepth = GetDepth(node.left, ref diameter);
            int rightDepth = GetDepth(node.right, ref diameter);

            diameter = Math.Max(diameter, leftDepth + rightDepth);
            return Math.Max(leftDepth, rightDepth) + 1; // Return actual depth
        }
    }
}