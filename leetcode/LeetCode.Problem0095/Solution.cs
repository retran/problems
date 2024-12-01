public class Solution
{
    public IList<TreeNode> GenerateTrees(int n)
    {
        if (n == 0)
        {
            return new List<TreeNode>();
        }

        return GenerateTreesInRange(1, n);
    }

    private IList<TreeNode> GenerateTreesInRange(int start, int end)
    {
        var allTrees = new List<TreeNode>();

        if (start > end)
        {
            allTrees.Add(null);
            return allTrees;
        }

        for (int i = start; i <= end; i++)
        {
            var leftTrees = GenerateTreesInRange(start, i - 1);
            var rightTrees = GenerateTreesInRange(i + 1, end);

            foreach (var left in leftTrees)
            {
                foreach (var right in rightTrees)
                {
                    var currentTree = new TreeNode(i);
                    currentTree.left = left;
                    currentTree.right = right;
                    allTrees.Add(currentTree);
                }
            }
        }

        return allTrees;
    }
}
