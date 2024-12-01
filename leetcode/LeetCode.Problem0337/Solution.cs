public class Solution
{
    private Dictionary<TreeNode, int> _cache = new Dictionary<TreeNode, int>();

    public int Rob(TreeNode root)
    {
        if (root == null)
        {
            return 0;
        }

        if (_cache.TryGetValue(root, out var cached))
        {
            return cached;
        }

        int maxAmountIfRobCurrentHouse = root.val 
            + Rob(root.left?.left)
            + Rob(root.left?.right)
            + Rob(root.right?.left)
            + Rob(root.right?.right);

        int maxAmountIfDoNotRobCurrentHouse = Rob(root.left) + Rob(root.right);

        var max = Math.Max(maxAmountIfRobCurrentHouse, maxAmountIfDoNotRobCurrentHouse);
        _cache[root] = max;
        return max;
    }
}