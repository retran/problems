public class MyHashMap {
    private List<(int key, int value)>[] buckets;
    private int bucketSize = 1000;

    /** Initialize your data structure here. */
    public MyHashMap() {
        buckets = new List<(int key, int value)>[bucketSize];
        for (int i = 0; i < bucketSize; i++) {
            buckets[i] = new List<(int key, int value)>();
        }
    }
    
    /** Adds or updates the key-value pair in the map. */
    public void Put(int key, int value) {
        int bucketIndex = GetBucketIndex(key);
        var bucket = buckets[bucketIndex];
        for (int i = 0; i < bucket.Count; i++) {
            if (bucket[i].key == key) {
                bucket[i] = (key, value); // Update the value if the key already exists
                return;
            }
        }
        bucket.Add((key, value)); // Add a new key-value pair if the key doesn't exist
    }
    
    /** Returns the value to which the specified key is mapped, or -1 if this map contains no mapping for the key. */
    public int Get(int key) {
        int bucketIndex = GetBucketIndex(key);
        var bucket = buckets[bucketIndex];
        foreach (var pair in bucket) {
            if (pair.key == key) {
                return pair.value;
            }
        }
        return -1; // Key not found
    }
    
    /** Removes the mapping of the specified key if it exists. */
    public void Remove(int key) {
        int bucketIndex = GetBucketIndex(key);
        var bucket = buckets[bucketIndex];
        for (int i = 0; i < bucket.Count; i++) {
            if (bucket[i].key == key) {
                bucket.RemoveAt(i);
                return;
            }
        }
    }
    
    /** Hash function to find the appropriate bucket index */
    private int GetBucketIndex(int key) {
        return key % bucketSize;
    }
}
