public class MovingAverage
{
    private readonly int _size;
    private readonly Queue<int> _queue;
    private int _sum = 0;

    public MovingAverage(int size)
    {
        _size = size;
        _queue = new Queue<int>(size);
    }

    public double Next(int val)
    {
        if (_queue.Count == _size)
        {
            _sum -= _queue.Dequeue();
        }

        _queue.Enqueue(val);
        _sum += val;

        return (double)_sum / _queue.Count;
    }
}

/**
 * Your MovingAverage object will be instantiated and called as such:
 * MovingAverage obj = new MovingAverage(size);
 * double param_1 = obj.Next(val);
 */