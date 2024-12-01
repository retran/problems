public class Solution
{
    private void CollectNodes(TreeNode root, Dictionary<TreeNode, TreeNode> parentMap, int startValue, int destValue, ref TreeNode startNode, ref TreeNode destNode)
    {
        if (root == null)
        {
            return;
        }

        if (root.val == startValue)
        {
            startNode = root;
        }

        if (root.val == destValue)
        {
            destNode = root;
        }

        if (root.left != null)
        {
            parentMap[root.left] = root;
            CollectNodes(root.left, parentMap, startValue, destValue, ref startNode, ref destNode);
        }

        if (root.right != null)
        {
            parentMap[root.right] = root;
            CollectNodes(root.right, parentMap, startValue, destValue, ref startNode, ref destNode);
        }
    }

    public string GetDirections(TreeNode root, int startValue, int destValue)
    {
        if (root == null)
        {
            return string.Empty;
        }

        var parentMap = new Dictionary<TreeNode, TreeNode>();
        TreeNode startNode = null;
        TreeNode destNode = null;

        CollectNodes(root, parentMap, startValue, destValue, ref startNode, ref destNode);

        if (startNode == null || destNode == null)
        {
            return string.Empty;
        }

        Dictionary<TreeNode, int> marks = new Dictionary<TreeNode, int>();
        marks[startNode] = 0;

        var queue = new Queue<TreeNode>();
        queue.Enqueue(startNode);

        int count = 0;
        while (queue.Count > 0)
        {
            count++;
            int i = queue.Count - 1;
            while (i >= 0) 
            {
                var node = queue.Dequeue();
                if (node == destNode)
                {
                    var path = new StringBuilder();
                    while (node != startNode)
                    {
                        var left = node.left;
                        var right = node.right;

                        if (parentMap.TryGetValue(node, out var nodePrnt) && marks.ContainsKey(nodePrnt) && marks[nodePrnt] == marks[node] - 1)
                        {
                            if (nodePrnt.left == node)
                            {
                                path.Insert(0, "L");
                            }
                            else
                            {
                                path.Insert(0, "R");
                            }
                            node = nodePrnt;
                            continue;
                        }

                        if (left != null && marks.ContainsKey(left) && marks[left] == marks[node] - 1)
                        {
                            path.Insert(0, "U");
                            node = left;
                            continue;
                        }

                        if (right != null && marks.ContainsKey(right) && marks[right] == marks[node] - 1)
                        {
                            path.Insert(0, "U");
                            node = right;
                            continue;
                        }
                    }

                    return path.ToString();
                }

                if (node.left != null && !marks.ContainsKey(node.left))
                {
                    marks[node.left] = count;
                    queue.Enqueue(node.left);
                }

                if (node.right != null && !marks.ContainsKey(node.right))
                {
                    marks[node.right] = count;
                    queue.Enqueue(node.right);
                }

                if (parentMap.TryGetValue(node, out var nodeParent) && !marks.ContainsKey(nodeParent))
                {
                    marks[nodeParent] = count;
                    queue.Enqueue(nodeParent);
                }

                i--;
            }
        }

        return string.Empty;
    }
}