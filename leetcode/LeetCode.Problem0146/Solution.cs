public class LRUCache
{
    private class ListNode
    {
        public int Key;
        public int Value;
        public ListNode Next;

        public ListNode Prev;
    }

    private int _capacity;
    private ListNode _head;
    private ListNode _tail;
    private Dictionary<int, ListNode> _cache;

    public LRUCache(int capacity)
    {
        _capacity = capacity;
        _head = new ListNode();
        _tail = null;
        _head = null;
        _cache = new Dictionary<int, ListNode>();
    }

    private void MoveToHead(ListNode node)
    {
        if (node == _head)
        {
            return;
        }

        if (node == _tail)
        {
            _tail = _tail.Prev;
            _tail.Next = null;
        }
        else
        {
            node.Prev.Next = node.Next;
            node.Next.Prev = node.Prev;
        }

        node.Prev = null;
        node.Next = _head;
        _head.Prev = node;
        _head = node;
    }

    private void AddToHead(ListNode node)
    {
        if (_head == null)
        {
            _head = node;
            _tail = node;
            return;
        }

        node.Next = _head;
        _head.Prev = node;
        _head = node;
    }

    private void RemoveTail()
    {
        if (_tail == null)
        {
            return;
        }

        if (_tail == _head)
        {
            _head = null;
            _tail = null;
            return;
        }

        _tail.Prev.Next = null;
        _tail = _tail.Prev;
    }

    public int Get(int key)
    {
        if (!_cache.ContainsKey(key))
        {
            return -1;
        }

        var node = _cache[key];
        MoveToHead(node);
        return node.Value;
    }

    public void Put(int key, int value)
    {
        if (_cache.ContainsKey(key))
        {
            var node = _cache[key];
            node.Value = value;
            MoveToHead(node);
        }
        else
        {
            var node = new ListNode { Key = key, Value = value };
            if (_cache.Count == _capacity)
            {
                _cache.Remove(_tail.Key);
                RemoveTail();
            }

            AddToHead(node);
            _cache.Add(key, node);
        }
    }
}

/**
 * Your LRUCache object will be instantiated and called as such:
 * LRUCache obj = new LRUCache(capacity);
 * int param_1 = obj.Get(key);
 * obj.Put(key,value);
 */