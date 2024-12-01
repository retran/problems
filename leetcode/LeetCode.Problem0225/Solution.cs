public class MyStack
{
    private Queue<int> _first;
    private Queue<int> _second;

    public MyStack()
    {
        _first = new Queue<int>();
        _second = new Queue<int>();
    }

    public void Push(int x)
    {
        _first.Enqueue(x);
    }

    public int Pop()
    {
        while (_first.Count > 1)
        {
            _second.Enqueue(_first.Dequeue());
        }

        int top = _first.Dequeue();

        (_first, _second) = (_second, _first);

        return top;
    }

    public int Top()
    {
        while (_first.Count > 1)
        {
            _second.Enqueue(_first.Dequeue());
        }

        int top = _first.Dequeue();

        _second.Enqueue(top);

        (_first, _second) = (_second, _first);

        return top;
    }

    public bool Empty()
    {
        return _first.Count == 0;
    }
}
