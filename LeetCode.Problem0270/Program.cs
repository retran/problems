using System.Diagnostics;

var solution = new Solution();

var tree = new TreeNode(4, 
    new TreeNode(2,
        new TreeNode(1),
        new TreeNode(3)),
    new TreeNode(5));

Debug.Assert(solution.ClosestValue(tree, 3.714286) == 4);

public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

public class Solution
{
    public int ClosestValue(TreeNode root, double target)
    {
        double diff = double.MaxValue;
        int closest = int.MaxValue;

        var current = root;
        while (true)
        {
            var newDiff = Math.Abs(target - current.val);
            if (newDiff < diff)
            {
                diff = newDiff;
                closest = current.val;
            }

            if (Math.Abs(diff - newDiff) < double.Epsilon)
            {
                if (closest > current.val)
                {
                    closest = current.val;
                }
            }

            if (current.left != null && target < current.val)
            {
                current = current.left;
            }
            else if (current.right != null && target >= current.val)
            {
                current = current.right;
            }
            else
            {
                break;
            }
        }

        return closest;
    }
}