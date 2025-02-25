public class MyHashSet
{
    private List<int>[] buckets;
    private int bucketSize = 1000;

    /** Initialize your data structure here. */
    public MyHashSet()
    {
        buckets = new List<int>[bucketSize];
        for (int i = 0; i < bucketSize; i++)
        {
            buckets[i] = new List<int>();
        }
    }

    /** Adds the key to the set. */
    public void Add(int key)
    {
        int bucketIndex = GetBucketIndex(key);
        if (!buckets[bucketIndex].Contains(key))
        {
            buckets[bucketIndex].Add(key);
        }
    }

    /** Removes the key from the set. */
    public void Remove(int key)
    {
        int bucketIndex = GetBucketIndex(key);
        if (buckets[bucketIndex].Contains(key))
        {
            buckets[bucketIndex].Remove(key);
        }
    }

    /** Returns true if this set contains the specified key. */
    public bool Contains(int key)
    {
        int bucketIndex = GetBucketIndex(key);
        return buckets[bucketIndex].Contains(key);
    }

    /** Hash function to find the appropriate bucket index */
    private int GetBucketIndex(int key)
    {
        return key % bucketSize;
    }
}