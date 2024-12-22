public class Solution
{
    public IList<TreeNode> FindDuplicateSubtrees(TreeNode root)
    {
        var result = new List<TreeNode>();
        var map = new Dictionary<string, int>();
        Traverse(root, map, result);
        return result;
    }

    private string Traverse(TreeNode node, Dictionary<string, int> map, List<TreeNode> result)
    {
        if (node == null) 
        {
            return "#";
        }

        string serialized = node.val 
            + "," 
            + Traverse(node.left, map, result) 
            + "," 
            + Traverse(node.right, map, result);

        if (map.ContainsKey(serialized))
        {
            map[serialized]++;
        }
        else
        {
            map[serialized] = 1;
        }

        if (map[serialized] == 2)
        {
            result.Add(node);
        }

        return serialized;
    }
}
