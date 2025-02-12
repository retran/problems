public class LFUCache
{
    private record Node(int Key)
    {
        public int Frequency { get; set; } = 1;
        public int Value { get; set; }
    }

    private readonly int _capacity;
    private int _leastFrequency;
    private Dictionary<int, LinkedListNode<Node>> _keyToNodeMap;
    private Dictionary<int, LinkedList<Node>> _frequencyToNodesMap;

    public LFUCache(int capacity)
    {
        _capacity = capacity;
        _leastFrequency = 0;
        _keyToNodeMap = new Dictionary<int, LinkedListNode<Node>>();
        _frequencyToNodesMap = new Dictionary<int, LinkedList<Node>>();
    }

    public int Get(int key)
    {
        if (!_keyToNodeMap.ContainsKey(key))
            return -1;

        var node = _keyToNodeMap[key];
        int oldFrequency = node.Value.Frequency;

        _frequencyToNodesMap[oldFrequency].Remove(node);
        if (_frequencyToNodesMap[oldFrequency].Count == 0)
        {
            _frequencyToNodesMap.Remove(oldFrequency);
            if (_leastFrequency == oldFrequency)
                _leastFrequency++;
        }

        node.Value.Frequency++;

        int newFrequency = node.Value.Frequency;

        if (!_frequencyToNodesMap.ContainsKey(newFrequency))
            _frequencyToNodesMap[newFrequency] = new LinkedList<Node>();

        var newNode = _frequencyToNodesMap[newFrequency].AddLast(node.Value);
        _keyToNodeMap[key] = newNode;
        return node.Value.Value;
    }

    public void Put(int key, int value)
    {
        if (_capacity <= 0)
            return;

        if (_keyToNodeMap.ContainsKey(key))
        {
            var existingNode = _keyToNodeMap[key];
            existingNode.Value.Value = value;
            Get(key);
            return;
        }

        if (_keyToNodeMap.Count >= _capacity)
        {
            var list = _frequencyToNodesMap[_leastFrequency];
            var nodeToRemove = list.First!;
            list.RemoveFirst();
            _keyToNodeMap.Remove(nodeToRemove.Value.Key);
            if (list.Count == 0)
                _frequencyToNodesMap.Remove(_leastFrequency);
        }

        var newNodeObj = new Node(key)
        {
            Value = value
        };

        if (!_frequencyToNodesMap.ContainsKey(1))
            _frequencyToNodesMap[1] = new LinkedList<Node>();

        var newListNode = _frequencyToNodesMap[1].AddLast(newNodeObj);
        _keyToNodeMap[key] = newListNode;

        _leastFrequency = 1;
    }
}

