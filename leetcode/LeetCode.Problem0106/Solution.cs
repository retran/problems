public class Solution
{
    public TreeNode BuildTree(int[] inorder, int[] postorder)
    {
        return BuildTree(postorder, inorder, postorder.Length - 1, 0, inorder.Length - 1);
    }

    private TreeNode BuildTree(int[] postorder, int[] inorder, int postStart, int inStart, int inEnd)
    {
        if (postStart < 0 || inStart > inEnd)
        {
            return null;
        }

        var root = new TreeNode(postorder[postStart]);
        int inIndex = 0;
        for (int i = inStart; i <= inEnd; i++)
        {
            if (inorder[i] == root.val)
            {
                inIndex = i;
            }
        }

        root.left = BuildTree(postorder, inorder, postStart - (inEnd - inIndex) - 1, inStart, inIndex - 1);
        root.right = BuildTree(postorder, inorder, postStart - 1, inIndex + 1, inEnd);

        return root;
    }
}