public class Heap
{
    private int _capacity;
    private int[] _heap;
    private int _size;

    private bool _min;

    public Heap(int capacity, bool min)
    {
        _capacity = capacity;
        _heap = new int[_capacity];
        _size = 0;
        _min = min;
    }

    public int Count => _size;

    public int Peek()
    {
        return _heap[0];
    }

    public void Push(int value)
    {
        if (_size == _capacity)
        {
            _capacity *= 2;
            int[] newHeap = new int[_capacity];
            Array.Copy(_heap, newHeap, _size);
            _heap = newHeap;
        }

        _heap[_size] = value;
        _size++;
        int index = _size - 1;
        int parent = (index - 1) / 2;

        while (index > 0 && (!_min && _heap[parent] < _heap[index])
            || (_min && _heap[parent] > _heap[index]))
        {
            int temp = _heap[index];
            _heap[index] = _heap[parent];
            _heap[parent] = temp;
            index = parent;
            parent = (index - 1) / 2;
        }
    }

    public int Pop()
    {
        if (_size == 0) 
        {
            throw new InvalidOperationException("Heap is empty");
        }

        int result = _heap[0];
        _size--;
        _heap[0] = _heap[_size];
        int index = 0;

        while (true)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            int largest = index;

            if (left < _size && ((!_min && _heap[left] > _heap[largest])
                || (_min && _heap[left] < _heap[largest])))
            {
                largest = left;
            }

            if (right < _size && ((!_min && _heap[right] > _heap[largest])
                || (_min && _heap[right] < _heap[largest])))
            {
                largest = right;
            }

            if (largest == index)
            {
                break;
            }

            int temp = _heap[index];
            _heap[index] = _heap[largest];
            _heap[largest] = temp;
            index = largest;
        }

        return result;
    }
}

public class MedianFinder
{
    private Heap _maxHeap = new Heap(1024, false); // Max-heap for smaller half
    private Heap _minHeap = new Heap(1024, true);  // Min-heap for larger half

    public void AddNum(int num)
    {
        if (_maxHeap.Count == 0 || num <= _maxHeap.Peek())
        {
            _maxHeap.Push(num);
        }
        else
        {
            _minHeap.Push(num);
        }

        // Rebalance heaps only if there are elements to pop
        if (_maxHeap.Count > _minHeap.Count + 1 && _maxHeap.Count > 0)
        {
            _minHeap.Push(_maxHeap.Pop());
        }
        else if (_minHeap.Count > _maxHeap.Count && _minHeap.Count > 0)
        {
            _maxHeap.Push(_minHeap.Pop());
        }
    }

    public double FindMedian()
    {
        if (_maxHeap.Count == _minHeap.Count)
        {
            return (_maxHeap.Peek() + _minHeap.Peek()) / 2.0;
        }
        else
        {
            return _maxHeap.Peek(); // Max-heap has one more element
        }
    }
}

/**
 * Your MedianFinder object will be instantiated and called as such:
 * MedianFinder obj = new MedianFinder();
 * obj.AddNum(num);
 * double param_2 = obj.FindMedian();
 */