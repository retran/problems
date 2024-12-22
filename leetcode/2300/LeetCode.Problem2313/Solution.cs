public class Solution
{
    private Dictionary<(TreeNode, bool), int> _cache = new();

    public int MinimumFlips(TreeNode root, bool result)
    {
        if (root == null)
        {
            return int.MaxValue;
        }

        if (_cache.TryGetValue((root, result), out var cached))
        {
            return cached;
        }

        if (root.left == null && root.right == null)
        {
            bool value = root.val == 1;

            return value == result ? 0 : 1;
        }

        int minFlips = 0;
        switch (root.val)
        {
            case 2: // OR
                if (result) // true
                {
                    minFlips = Math.Min(
                        MinimumFlips(root.left, true),
                        MinimumFlips(root.right, true));
                }
                else // false
                {
                    minFlips = MinimumFlips(root.left, false)
                             + MinimumFlips(root.right, false);
                }
                break;
            case 3: // AND
                if (result) // true
                {
                    minFlips = MinimumFlips(root.left, true)
                             + MinimumFlips(root.right, true);
                }
                else // false
                {
                    minFlips = Math.Min(
                        MinimumFlips(root.left, false),
                        MinimumFlips(root.right, false));
                }
                break;
            case 4: // XOR
                if (result) // true
                {
                    minFlips = Math.Min(
                        MinimumFlips(root.left, true) + MinimumFlips(root.right, false),
                        MinimumFlips(root.left, false) + MinimumFlips(root.right, true));
                }
                else // false
                {
                    minFlips = Math.Min(
                        MinimumFlips(root.left, false) + MinimumFlips(root.right, false),
                        MinimumFlips(root.left, true) + MinimumFlips(root.right, true));
                }
                break;
            case 5: // NOT
                if (root.left != null)
                {
                    minFlips = MinimumFlips(root.left, !result);
                }
                else if (root.right != null)
                {
                    minFlips = MinimumFlips(root.right, !result);
                }
                break;
            default:
                throw new InvalidOperationException();
        }

        _cache[(root, result)] = minFlips;
        return minFlips;
    }
}