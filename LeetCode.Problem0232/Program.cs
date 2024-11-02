using System.Diagnostics;

var queue = new MyQueue();

queue.Push(1);
queue.Push(2);
queue.Push(3);

Debug.Assert(queue.Pop() == 1);
Debug.Assert(queue.Peek() == 2);
Debug.Assert(!queue.Empty());
Debug.Assert(queue.Pop() == 2);
queue.Push(4);
Debug.Assert(queue.Peek() == 3);
Debug.Assert(queue.Pop() == 3);
Debug.Assert(queue.Pop() == 4);
Debug.Assert(queue.Empty());

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