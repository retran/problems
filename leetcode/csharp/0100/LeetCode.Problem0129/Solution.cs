public class Solution
{
    public int Traverse(TreeNode node, int sum)
    {
        if (node == null)
        {
            return 0;
        }

        sum = sum * 10 + node.val;

        if (node.left == null && node.right == null)
        {
            return sum;
        }

        return Traverse(node.left, sum) + Traverse(node.right, sum);
    }

    public int SumNumbers(TreeNode root)
    {
        return Traverse(root, 0);
    }
}