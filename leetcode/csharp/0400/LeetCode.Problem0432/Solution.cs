public class AllOne
{
    private class FrequencyBucket
    {
        public int Frequency { get; }

        private readonly HashSet<string> _keys;

        public FrequencyBucket(int frequency)
        {
            Frequency = frequency;
            _keys = new HashSet<string>();
        }

        public void AddKey(string key) => _keys.Add(key);

        public void RemoveKey(string key) => _keys.Remove(key);

        public int KeyCount => _keys.Count;

        public string GetAnyKey() => _keys.First();
    }

    private readonly LinkedList<FrequencyBucket> _buckets = new LinkedList<FrequencyBucket>();

    private readonly Dictionary<string, LinkedListNode<FrequencyBucket>> _keyToBucket =
        new Dictionary<string, LinkedListNode<FrequencyBucket>>();

    public AllOne()
    {
    }

    public void Inc(string key)
    {
        if (!_keyToBucket.TryGetValue(key, out LinkedListNode<FrequencyBucket>? currentNode))
        {
            if (_buckets.First == null || _buckets.First.Value.Frequency != 1)
            {
                currentNode = _buckets.AddFirst(new FrequencyBucket(1));
            }
            else
            {
                currentNode = _buckets.First;
            }

            currentNode.Value.AddKey(key);
            _keyToBucket[key] = currentNode;
        }
        else
        {
            int newFrequency = currentNode.Value.Frequency + 1;
            currentNode.Value.RemoveKey(key);

            LinkedListNode<FrequencyBucket>? nextNode = currentNode.Next;
            if (nextNode == null || nextNode.Value.Frequency != newFrequency)
            {
                nextNode = _buckets.AddAfter(currentNode, new FrequencyBucket(newFrequency));
            }

            nextNode.Value.AddKey(key);
            _keyToBucket[key] = nextNode;

            if (currentNode.Value.KeyCount == 0)
            {
                _buckets.Remove(currentNode);
            }
        }
    }

    public void Dec(string key)
    {
        if (!_keyToBucket.TryGetValue(key, out LinkedListNode<FrequencyBucket>? currentNode))
        {
            return;
        }

        int currentFrequency = currentNode.Value.Frequency;
        currentNode.Value.RemoveKey(key);

        if (currentFrequency == 1)
        {
            _keyToBucket.Remove(key);
        }
        else
        {
            int newFrequency = currentFrequency - 1;
            LinkedListNode<FrequencyBucket>? prevNode = currentNode.Previous;
            if (prevNode == null || prevNode.Value.Frequency != newFrequency)
            {
                prevNode = _buckets.AddBefore(currentNode, new FrequencyBucket(newFrequency));
            }

            prevNode.Value.AddKey(key);
            _keyToBucket[key] = prevNode;
        }

        if (currentNode.Value.KeyCount == 0)
        {
            _buckets.Remove(currentNode);
        }
    }

    public string GetMaxKey()
    {
        return _buckets.Count == 0 ? string.Empty : _buckets.Last!.Value.GetAnyKey();
    }

    public string GetMinKey()
    {
        return _buckets.Count == 0 ? string.Empty : _buckets.First!.Value.GetAnyKey();
    }
}
