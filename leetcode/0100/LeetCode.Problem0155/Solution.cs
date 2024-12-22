public class MinStack
{
    private int[] _stack;
    private int _head;

    private int[] _minStack;
    private int _minHead;

    public MinStack()
    {
        _stack = new int[16];
        _minStack = new int[16];
        _head = -1;
        _minHead = -1;
    }

    public void Push(int val)
    {
        if (_head == _stack.Length - 1) {
            Array.Resize(ref _stack, _stack.Length * 2);
        }

        _stack[++_head] = val;

        if (_minHead == -1 || val <= _minStack[_minHead]) {
            if (_minHead == _minStack.Length - 1) {
                Array.Resize(ref _minStack, _minStack.Length * 2);
            }

            _minStack[++_minHead] = val;
        }
    }

    public void Pop()
    {
        if (_head >= 0) {
            _head--;
        }
        if (_minHead >= 0 && _stack[_head + 1] == _minStack[_minHead]) {
            _minHead--;
        }
    }

    public int Top()
    {
        if (_head >= 0) {
            return _stack[_head];
        }

        return -1;
    }

    public int GetMin()
    {
        if (_minHead >= 0) {
            return _minStack[_minHead];
        }

        return -1;
    }
}

/**
 * Your MinStack object will be instantiated and called as such:
 * MinStack obj = new MinStack();
 * obj.Push(val);
 * obj.Pop();
 * int param_3 = obj.Top();
 * int param_4 = obj.GetMin();
 */