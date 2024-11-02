using System.Diagnostics;

var stack = new MyStack();

stack.Push(2);
stack.Push(3);
stack.Push(4);
Debug.Assert(stack.Pop() == 4);
Debug.Assert(stack.Top() == 3);
Debug.Assert(!stack.Empty());
stack.Push(5);
Debug.Assert(stack.Pop() == 5);
Debug.Assert(stack.Top() == 3);
Debug.Assert(stack.Pop() == 3);
Debug.Assert(stack.Top() == 2);
Debug.Assert(stack.Pop() == 2);
Debug.Assert(stack.Empty());

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

/**
 * Your MyStack object will be instantiated and called as such:
 * MyStack obj = new MyStack();
 * obj.Push(x);
 * int param_2 = obj.Pop();
 * int param_3 = obj.Top();
 * bool param_4 = obj.Empty();
 */