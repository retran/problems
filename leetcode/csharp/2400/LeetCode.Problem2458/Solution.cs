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
    Dictionary<int, int> depth = new Dictionary<int, int>();
    Dictionary<int, int> height = new Dictionary<int, int>();
    Dictionary<int, int> up = new Dictionary<int, int>();

    public int[] TreeQueries(TreeNode root, int[] queries)
    {
        ComputeHeights(root, 0);
        DfsUp(root, -1);

        int[] ans = new int[queries.Length];
        for (int i = 0; i < queries.Length; i++)
        {
            ans[i] = Math.Max(up[queries[i]], 0);
        }
        return ans;
    }

    int ComputeHeights(TreeNode node, int currentHeight)
    {
        if (node == null)
            return -1;
        depth[node.val] = currentHeight;
        int leftHeight = ComputeHeights(node.left, currentHeight + 1);
        int rightHeight = ComputeHeights(node.right, currentHeight + 1);
        int h = Math.Max(leftHeight, rightHeight) + 1;
        height[node.val] = h;
        return h;
    }

    void DfsUp(TreeNode node, int currentUp)
    {
        up[node.val] = currentUp;
        if (node.left != null)
        {
            int candidateDepth = -1;
            if (node.right != null)
                candidateDepth = depth[node.val] + 1 + height[node.right.val];
            int newUp = Math.Max(currentUp, Math.Max(depth[node.val], candidateDepth));
            DfsUp(node.left, newUp);
        }

        if (node.right != null)
        {
            int candidateDepth = -1;
            if (node.left != null)
                candidateDepth = depth[node.val] + 1 + height[node.left.val];
            int newUp = Math.Max(currentUp, Math.Max(depth[node.val], candidateDepth));
            DfsUp(node.right, newUp);
        }
    }
}