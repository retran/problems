public class KthLargest
{
    private readonly int k;
    private readonly PriorityQueue<int, int> minHeap;

    public KthLargest(int k, int[] nums)
    {
        this.k = k;
        minHeap = new PriorityQueue<int, int>();
        foreach (var num in nums)
        {
            Add(num);
        }
    }

    public int Add(int val)
    {
        minHeap.Enqueue(val, val);

        if (minHeap.Count > k)
        {
            minHeap.Dequeue();
        }

        return minHeap.Peek();
    }
}
