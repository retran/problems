public class RandomizedSet {
    private Dictionary<int, int> _map = new();

    private List<int> _list = new();

    private Random _random = new();
    
    public bool Insert(int val) {
        if (_map.ContainsKey(val))
        {
            return false;
        }
        
        _list.Add(val);
        _map[val] = _list.Count - 1;
        return true;
    }
    
    public bool Remove(int val) {
        if (_map.TryGetValue(val, out var index))
        {
            var last = _list[_list.Count - 1];
            _list[index] = last;
            _map[last] = index;
            _list.RemoveAt(_list.Count - 1);
            _map.Remove(val);
            return true;
        }

        return false;
    }
    
    public int GetRandom() {
        return _list[_random.Next(_list.Count)];
    }
}