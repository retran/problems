public class Solution
{
    private void GetLeafs(TreeNode root, List<int> leafs)
    {
        if (root == null)
        {
            return;
        }

        if (root.left == null && root.right == null)
        {
            leafs.Add(root.val);
        }

        GetLeafs(root.left, leafs);
        GetLeafs(root.right, leafs);
    }
    
    public bool LeafSimilar(TreeNode root1, TreeNode root2)
    {
        var leafs1 = new List<int>();
        var leafs2 = new List<int>();

        GetLeafs(root1, leafs1);
        GetLeafs(root2, leafs2);

        if (leafs1.Count != leafs2.Count)
        {
            return false;
        }

        for (int i = 0; i < leafs1.Count; i++)
        {
            if (leafs1[i] != leafs2[i])
            {
                return false;
            }
        }

        return true;
    }
}