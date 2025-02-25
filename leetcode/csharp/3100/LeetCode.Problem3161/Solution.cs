public class Solution
{
    class TreeNode
    {
        public readonly int From;

        public readonly int To;

        public bool IsLeaf => Left == null && Right == null;

        public int Size => To - From;

        public int MaxSize { get; private set; }

        public TreeNode? Left { get; private set; } = null;

        public TreeNode? Right { get; private set; } = null;

        public TreeNode(int from, int to)
        {
            From = from;
            To = to;
            MaxSize = to - from;
        }

        public void UpdateSize()
        {
            Left?.UpdateSize();
            Right?.UpdateSize();

            MaxSize =
                IsLeaf
                    ? Size
                    : Math.Max(
                        Left != null ? Left.MaxSize : 0,
                        Right != null ? Right.MaxSize : 0);
        }

        public void Split(int value)
        {
            Left = new TreeNode(From, value);
            Right = new TreeNode(value, To);
        }
    }

    private TreeNode? GetNodeByValue(TreeNode? root, int value)
    {
        if (root == null || root.From >= value || value >= root.To)
        {
            return null;
        }

        if (root.IsLeaf)
        {
            return root;
        }

        return GetNodeByValue(root.Left, value)
            ?? GetNodeByValue(root.Right, value);
    }

    private TreeNode? GetNodeByRange(TreeNode? root, int to, int size)
    {
        if (root == null
            || root.MaxSize < size
            || to - root.From < size)
        {
            return null;
        }

        if (root.IsLeaf)
        {
            return root;
        }

        return GetNodeByRange(root.Left, to, size)
            ?? GetNodeByRange(root.Right, to, size);
    }

    private void ExecuteQuery(TreeNode root, int[] query, List<bool> answers)
    {
        switch (query[0])
        {
            case 1:
                var nodeToSplit = GetNodeByValue(root, query[1]);
                if (nodeToSplit != null)
                {
                    nodeToSplit.Split(query[1]);
                    root.UpdateSize();
                }
                break;
            case 2:
                var nodeToFit = GetNodeByRange(root, query[1], query[2]);
                answers.Add(nodeToFit != null);
                break;
            default:
                throw new InvalidOperationException();
        }
    }

    public IList<bool> GetResults(int[][] queries)
    {
        var answers = new List<bool>();
        var root = new TreeNode(0, Math.Min(50000, 3 * queries.Length));
        foreach (var query in queries)
        {
            ExecuteQuery(root, query, answers);
        }
        return answers;
    }

    public static void Main()
    {
        int[][] queries =
        [[1, 1], [1, 11], [1, 4], [1, 8], [2, 13, 7]];
        //        [[2,1,2]];
        //[[1,3],[2,4,2]];
        // [[1, 2], [2, 3, 3], [2, 3, 1], [2, 2, 2]];
        //[[1,7],[2,7,6],[1,2],[2,7,5],[2,7,6]];

        var solution = new Solution();
        var results = solution.GetResults(queries);
        System.Console.WriteLine("[" + string.Join(", ", results) + "]");
    }
}