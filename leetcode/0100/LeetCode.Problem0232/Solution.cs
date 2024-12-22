public class MyQueue
{
    private Stack<int> _first;
    private Stack<int> _second;

    public MyQueue()
    {
        _first = new Stack<int>();
        _second = new Stack<int>();
    }

    public void Push(int x)
    {
        _first.Push(x);
    }

    public int Pop()
    {
        if (_second.Count > 0)
        {
            return _second.Pop();
        }

        while (_first.Count > 0)
        {
            _second.Push(_first.Pop());
        }

        return _second.Pop();
    }

    public int Peek()
    {
        if (_second.Count > 0)
        {
            return _second.Peek();
        }

        while (_first.Count > 0)
        {
            _second.Push(_first.Pop());
        }

        return _second.Peek();
    }

    public bool Empty()
    {
        return _first.Count == 0 && _second.Count == 0;
    }
}