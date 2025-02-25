public class SnapshotArray
{

    private class Change
    {
        public int SnapshotId { get; private set; }
        public int Value { get; set; }

        public Change(int snapshotId, int value)
        {
            SnapshotId = snapshotId;
            Value = value;
        }
    }

    private readonly Dictionary<int, IList<Change>> _changes = new Dictionary<int, IList<Change>>();

    private int _currentSnapshotId = 0;

    public SnapshotArray(int length)
    {

    }

    public void Set(int index, int val)
    {
        if (!_changes.TryGetValue(index, out var changes))
        {
            changes = new List<Change>();
            _changes[index] = changes;
        }

        if (changes.Count == 0 || changes.Last().SnapshotId != _currentSnapshotId)
        {
            changes.Add(new Change(_currentSnapshotId, val));
        }
        else
        {
            changes.Last().Value = val;
        }
    }

    public int Snap()
    {
        _currentSnapshotId++;
        return _currentSnapshotId - 1;
    }

    public int Get(int index, int snap_id)
    {
        if (!_changes.TryGetValue(index, out var changes))
        {
            return 0;
        }

        if (snap_id < changes.First().SnapshotId)
        {
            return 0;
        }

        if (snap_id >= changes.Last().SnapshotId)
        {
            return changes.Last().Value;
        }

        int left = 0; // [0; 2]
        int right = changes.Count - 1;
        int last = -1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (changes[mid].SnapshotId == snap_id)
            {
                return changes[mid].Value;
            }
            else if (changes[mid].SnapshotId < snap_id)
            {
                last = mid;
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return changes[last].Value;
    }
}