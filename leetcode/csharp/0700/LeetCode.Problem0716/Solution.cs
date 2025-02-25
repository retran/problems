public class MaxStack
{
    private class Node
    {
        private static int _nextId = 0;

        public int Value { get; }
        public int Id { get; }
        public LinkedListNode<Node>? ListNode { get; set; } = null;

        public Node(int value)
        {
            Value = value;
            Id = _nextId++;
        }
    }

    private class NodeComparer : IComparer<Node>
    {
        public int Compare(Node? a, Node? b)
        {
            if (a == null || b == null)
                throw new ArgumentException("Compared nodes cannot be null.");

            int cmp = a.Value.CompareTo(b.Value);
            if (cmp != 0)
                return cmp;

            return a.Id.CompareTo(b.Id);
        }
    }

    private readonly LinkedList<Node> _stack = new LinkedList<Node>();
    private readonly SortedSet<Node> _bst = new SortedSet<Node>(new NodeComparer());

    public MaxStack()
    {
    }

    public void Push(int x)
    {
        var node = new Node(x);
        var listNode = _stack.AddLast(node);
        node.ListNode = listNode;
        _bst.Add(node);
    }

    public int Pop()
    {
        if (_stack.Count == 0)
            throw new InvalidOperationException("Stack is empty.");

        var listNode = _stack.Last;
        Node node = listNode!.Value;
        _stack.RemoveLast();
        _bst.Remove(node);
        return node.Value;
    }

    public int Top()
    {
        if (_stack.Count == 0)
            throw new InvalidOperationException("Stack is empty.");

        return _stack.Last!.Value.Value;
    }

    public int PeekMax()
    {
        if (_bst.Count == 0)
            throw new InvalidOperationException("Stack is empty.");

        return _bst.Max!.Value;
    }

    public int PopMax()
    {
        if (_bst.Count == 0)
            throw new InvalidOperationException("Stack is empty.");

        Node maxNode = _bst.Max!;
        _bst.Remove(maxNode);
        _stack.Remove(maxNode.ListNode!);
        return maxNode.Value;
    }
}
