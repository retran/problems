
public class Solution
{
    public (TreeNode node, int distance) FindLCA(TreeNode root, int p, int q, int distanceFromRoot)
    {
        if (root == null)
        {
            return (null, -1);
        }

        if (root.val == p || root.val == q)
        {
            return (root, distanceFromRoot);
        }

        var left = FindLCA(root.left, p, q, distanceFromRoot + 1);
        var right = FindLCA(root.right, p, q, distanceFromRoot + 1);

        if (left.node != null && right.node != null)
        {
            return (root, distanceFromRoot);
        }

        if (left.node == null)
        {
            return (right.node, right.distance);
        }
        else
        {
            return (left.node, left.distance);
        }
    }

    private (TreeNode node, int distance) FindDescendant(TreeNode root, int value, int distanceFromRoot)
    {
        if (root == null)
        {
            return (null, -1);
        }

        if (root.val == value)
        {
            return (root, distanceFromRoot);
        }

        var left = FindDescendant(root.left, value, distanceFromRoot + 1);
        if (left.node != null)
        {
            return (left.node, left.distance);
        }
        
        var right = FindDescendant(root.right, value, distanceFromRoot + 1);
        if (right.node != null)
        {
            return (right.node, right.distance);
        }

        return (null, -1);
    }

    public int FindDistance(TreeNode root, int p, int q)
    {
        var lca = FindLCA(root, p, q, 0);

        var pNode = FindDescendant(lca.node, p, lca.distance);
        var qNode = FindDescendant(lca.node, q, lca.distance);

        return pNode.distance + qNode.distance - 2 * lca.distance;
    }
}