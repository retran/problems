public class Solution
{
    public int RangeSum(int[] nums, int n, int left, int right)
    {
        var heap = new PriorityQueue<int, int>();

        var prefixSums = new int[n];
        prefixSums[0] = nums[0];

        for (int i = 1; i < n; i++)
        {
            prefixSums[i] = (prefixSums[i - 1] + nums[i]) % 1000000007;
        }

        for (int i = 0; i < n; i++)
            for (int j = i; j < n; j++)
            {
                int sum = i == 0 
                    ? prefixSums[j] 
                    : (prefixSums[j] - prefixSums[i - 1]) % 1000000007;

                heap.Enqueue(sum, sum);
            }

        int count = left - 1;
        while (count > 0)
        {
            count--;
            heap.Dequeue();
        }

        count = right - left + 1;
        int answer = 0;
        while (count > 0)
        {
            count--;
            int val = heap.Dequeue();
            System.Console.WriteLine(val);
            answer = (answer + val) % 1000000007;
        }
        return answer;
    }
}