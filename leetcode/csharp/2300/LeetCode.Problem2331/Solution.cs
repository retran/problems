public class TreeNode
{
    public int val;
    public TreeNode? left;
    public TreeNode? right;
    public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

public class Solution
{
    public bool EvaluateTree(TreeNode root)
    {
        bool isLeaf = root.left == null || root.right == null;

        if (isLeaf)
        {
            return root.val == 1;
        }

        switch (root.val)
        {
            case 2:
                return EvaluateTree(root.left!) || EvaluateTree(root.right!);
            case 3:
                return EvaluateTree(root.left!) && EvaluateTree(root.right!);
            default:
                throw new System.Exception("Invalid value");
        }
    }
}