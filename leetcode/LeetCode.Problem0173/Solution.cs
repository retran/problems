public class BSTIterator
{
    private Stack<TreeNode> _stack = new Stack<TreeNode>();
    private TreeNode _current;

    public BSTIterator(TreeNode root)
    {
        _current = root;
    }

    public int Next()
    {
        while (_current != null) {
            _stack.Push(_current);
            _current = _current.left;
        }

        if (_current == null && _stack.Count > 0)
        {
            var popped = _stack.Pop();
            _current = popped.right;
            return popped.val;
        }

        return -1;
    }

    public bool HasNext()
    {
        return _current != null || _stack.Count > 0;
    }
}