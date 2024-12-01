public class Solution
{
    public TreeNode BuildTree(int[] preorder, int[] inorder)
    {
        return BuildTree(preorder, inorder, 0, 0, inorder.Length - 1);
    }

    private TreeNode BuildTree(int[] preorder, int[] inorder, int preStart, int inStart, int inEnd)
    {
        if (preStart >= preorder.Length || inStart > inEnd)
        {
            return null;
        }

        var root = new TreeNode(preorder[preStart]);
        int inIndex = 0;
        for (int i = inStart; i <= inEnd; i++)
        {
            if (inorder[i] == root.val)
            {
                inIndex = i;
            }
        }

        root.left = BuildTree(preorder, inorder, preStart + 1, inStart, inIndex - 1);
        root.right = BuildTree(preorder, inorder, preStart + inIndex - inStart + 1, inIndex + 1, inEnd);

        return root;
    }
}