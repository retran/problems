public class Solution
{
    private class Heap
    {
        private int _capacity;
        private int[] _heap;
        private int _size;

        public Heap(int capacity)
        {
            _capacity = capacity;
            _heap = new int[_capacity];
            _size = 0;
        }

        public int Count => _size;

        public void Push(int value)
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



    public int FindMaximizedCapital(int k, int w, int[] profits, int[] capital)
    {
        int n = profits.Length;
        (int, int)[] projects = new (int, int)[n];
        for (int l = 0; l < n; l++)
        {
            projects[l] = (capital[l], profits[l]);
        }

        Array.Sort(projects, (a, b) => a.Item1 - b.Item1);

        int i = 0;
        var heap = new Heap(profits.Length);
        while (k > 0)
        {
            while (i < n && projects[i].Item1 <= w)
            {
                heap.Push(projects[i].Item2);
                i++;
            }

            if (heap.Count == 0)
            {
                break;
            }

            w += heap.Pop();
            k--;
        }

        return w;
    }
}