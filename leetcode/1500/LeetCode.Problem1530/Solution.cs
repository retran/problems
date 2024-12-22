public class Solution
{
    private IEnumerable<TreeNode> GetLeafsAndMapParents(TreeNode root, IDictionary<TreeNode, TreeNode> parentMap)
    {
        if (root == null)
        {
            yield break;
        }

        if (root.left == null && root.right == null)
        {
            yield return root;
        }

        if (root.left != null)
        {
            parentMap[root.left] = root;
        }

        if (root.right != null)
        {
            parentMap[root.right] = root;
        }

        foreach (var leaf in GetLeafsAndMapParents(root.left, parentMap))
        {
            yield return leaf;
        }

        foreach (var leaf in GetLeafsAndMapParents(root.right, parentMap))
        {
            yield return leaf;
        }
    }

    private int CountReachableLeafs(TreeNode start, int distance, IDictionary<TreeNode, TreeNode> parentMap) 
    {
        int count = 0;
        HashSet<TreeNode> visited = new HashSet<TreeNode>();
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(start);
        int currentDistance = 0;
        while (queue.Count > 0 && currentDistance <= distance)
        {
            int levelSize = queue.Count;
            for (int i = 0; i < levelSize; i++)
            {
                TreeNode current = queue.Dequeue();
                visited.Add(current);

                if (current.left == null && current.right == null && current != start)
                {
                    count++;
                }

                if (current.left != null && !visited.Contains(current.left))
                {
                    queue.Enqueue(current.left);
                }

                if (current.right != null && !visited.Contains(current.right))
                {
                    queue.Enqueue(current.right);
                }

                if (parentMap.TryGetValue(current, out TreeNode parent) && !visited.Contains(parent))
                {
                    queue.Enqueue(parent);
                }
            }
            currentDistance++;
        }
        return count;
    }

    public int CountPairs(TreeNode root, int distance)
    {
        var parentMap = new Dictionary<TreeNode, TreeNode>();
        var leafs = GetLeafsAndMapParents(root, parentMap).ToArray();

        int count = 0;

        for (int i = 0; i < leafs.Length; i++)
        {
            count += CountReachableLeafs(leafs[i], distance, parentMap);
        }

        return count / 2;
    }
}