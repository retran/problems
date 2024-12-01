public class Solution
{
    private class Heap
    {
        private int _capacity;
        private int[] _heap;
        private int _size;

        public Heap(int[] data, int capacity)
        {
            _capacity = capacity;

            if (_capacity < data.Length)
            {
                _capacity = data.Length;
            }

            _heap = new int[_capacity];

            Array.Copy(data, _heap, data.Length);
            _size = data.Length;

            Heapify();
        }

        public void Insert(int value)
        {
            _heap[_size] = value;
            _size++;
            int index = _size - 1;
            int parent = (index - 1) / 2;

            while (index > 0 && _heap[parent] < _heap[index])
            {
                int temp = _heap[index];
                _heap[index] = _heap[parent];
                _heap[parent] = temp;
                index = parent;
                parent = (index - 1) / 2;
            }
        }

        private void Heapify() 
        {
            for (int i = _size / 2 - 1; i >= 0; i--)
            {
                int index = i;

                while (true)
                {
                    int left = 2 * index + 1;
                    int right = 2 * index + 2;
                    int largest = index;

                    if (left < _size && _heap[left] > _heap[largest])
                    {
                        largest = left;
                    }

                    if (right < _size && _heap[right] > _heap[largest])
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
            }
        }

        public int Pop()
        {
            int result = _heap[0];
            _size--;
            _heap[0] = _heap[_size];
            int index = 0;

            while (true)
            {
                int left = 2 * index + 1;
                int right = 2 * index + 2;
                int largest = index;

                if (left < _size && _heap[left] > _heap[largest])
                {
                    largest = left;
                }

                if (right < _size && _heap[right] > _heap[largest])
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

    public int FindKthLargest(int[] nums, int k)
    {
        var heap = new Heap(nums, nums.Length);

        for (int i = 0; i < k - 1; i++)
        {
            heap.Pop();
        }

        return heap.Pop();
    }
}