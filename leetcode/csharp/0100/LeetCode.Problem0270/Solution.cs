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