public class Solution
{
    private class BinaryIndexTree
    {
        private int _size;
        private int[] _array;

        public BinaryIndexTree(int size)
        {
            _size = size;
            _array = new int[size + 1];
        }

        public void Update(int index, int delta)
        {
            while (index <= _size)
            {
                _array[index] += delta;
                index += index & -index;
            }
        }

        public int Query(int index)
        {
            int sum = 0;
            while (index > 0)
            {
                sum += _array[index];
                index -= index & -index;
            }
            return sum;
        }
    }

    public int KBigIndices(int[] nums, int k)
    {
        BinaryIndexTree left = new(nums.Length);
        BinaryIndexTree right = new(nums.Length);

        for (int i = 0; i < nums.Length; i++)
        {
            right.Update(nums[i], 1);
        }

        int count = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            left.Update(nums[i], 1);
            right.Update(nums[i], -1);

            int leftCount = left.Query(nums[i] - 1);
            int rightCount = right.Query(nums[i] - 1);

            if (leftCount >= k && rightCount >= k)
            {
                count++;
            }
        }

        return count;
    }
}