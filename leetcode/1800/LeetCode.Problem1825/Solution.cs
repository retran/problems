public class MKAverage
{
    private class TreeNode
    {
        public int Value;
        public TreeNode? Left, Right;
        public int Count;
    }

    private readonly int _m;
    private readonly int _k;
    private TreeNode? _root = null;
    private Queue<int> _queue = new Queue<int>();

    public MKAverage(int m, int k)
    {
        _m = m;
        _k = k;
    }

    public void AddElement(int num)
    {
        _queue.Enqueue(num);
        _root = Insert(_root, num);

        if (_queue.Count > _m)
        {
            int toRemove = _queue.Dequeue();
            _root = Remove(_root, toRemove);
        }
    }

    private TreeNode Insert(TreeNode? root, int num)
    {
        if (root == null)
            return new TreeNode { Value = num, Count = 1 };

        if (num == root.Value)
        {
            root.Count++;
            return root;
        }
        else if (num < root.Value)
            root.Left = Insert(root.Left, num);
        else
            root.Right = Insert(root.Right, num);

        return root;
    }

    private TreeNode? Remove(TreeNode? root, int num)
    {
        if (root == null)
            return null;

        if (num < root.Value)
            root.Left = Remove(root.Left, num);
        else if (num > root.Value)
            root.Right = Remove(root.Right, num);
        else
        {
            if (root.Count > 1)
            {
                root.Count--;
            }
            else
            {
                if (root.Left == null)
                    return root.Right;
                if (root.Right == null)
                    return root.Left;

                TreeNode succ = root.Right;
                while (succ.Left != null)
                    succ = succ.Left;

                root.Value = succ.Value;
                root.Right = Remove(root.Right, succ.Value);
            }
        }
        return root;
    }

    public int CalculateMKAverage()
    {
        if (_queue.Count < _m)
            return -1;

        int remainingSkip = _k;
        int remainingTake = _m - 2 * _k;
        long sum = 0;

        var stack = new Stack<TreeNode>();
        TreeNode? node = _root;

        while (node != null || stack.Count > 0)
        {
            while (node != null)
            {
                stack.Push(node);
                node = node.Left;
            }

            node = stack.Pop();
            int count = node.Count;

            if (remainingSkip > 0)
            {
                if (remainingSkip >= count)
                {
                    remainingSkip -= count;
                }
                else
                {
                    int leftAfterSkip = count - remainingSkip;
                    int toTake = Math.Min(leftAfterSkip, remainingTake);
                    sum += (long)node.Value * toTake;
                    remainingTake -= toTake;
                    remainingSkip = 0;
                }
            }
            else
            {
                if (remainingTake >= count)
                {
                    sum += (long)node.Value * count;
                    remainingTake -= count;
                }
                else
                {
                    sum += (long)node.Value * remainingTake;
                    remainingTake = 0;
                }
            }

            if (remainingTake <= 0)
                break;

            node = node.Right;
        }

        return (int)(sum / (_m - 2 * _k));
    }
}
