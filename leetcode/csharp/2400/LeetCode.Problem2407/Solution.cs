public class Solution
{
    public int LengthOfLIS(int[] nums, int k)
    {
        var set = new SortedSet<int>();
        foreach (var x in nums)
        {
            set.Add(x);
        }

        int m = set.Count;
        int[] values = new int[m];
        int index = 0;
        foreach (var x in set)
        {
            values[index++] = x;
        }

        SegmentTree tree = new SegmentTree(m);
        int answer = 0;

        foreach (var num in nums)
        {
            int L = num - k;
            int R = num - 1;

            int left = LowerBound(values, L);
            int right = UpperBound(values, R) - 1;

            int best = 0;
            if (left < m && right >= left)
                best = tree.Query(left, right);

            int dp = best + 1;

            int pos = LowerBound(values, num);
            tree.Update(pos, dp);
            answer = Math.Max(answer, dp);
        }
        return answer;
    }

    private int LowerBound(int[] values, int target)
    {
        int left = 0, right = values.Length;
        while (left < right)
        {
            int mid = left + (right - left) / 2;
            if (values[mid] < target)
                left = mid + 1;
            else
                right = mid;
        }
        return left;
    }

    private int UpperBound(int[] values, int target)
    {
        int left = 0, right = values.Length;
        while (left < right)
        {
            int mid = left + (right - left) / 2;
            if (values[mid] <= target)
                left = mid + 1;
            else
                right = mid;
        }
        return left;
    }


    public class SegmentTree
    {
        private int _size;
        private int[] _values;

        public SegmentTree(int size)
        {
            _size = size;
            _values = new int[2 * _size];
        }

        public void Update(int index, int value)
        {
            index += _size;
            _values[index] = Math.Max(_values[index], value);
            for (index /= 2; index > 0; index /= 2)
            {
                _values[index] = Math.Max(_values[2 * index], _values[2 * index + 1]);
            }
        }

        public int Query(int left, int right)
        {
            int res = 0;
            left += _size; right += _size;
            while (left <= right)
            {
                if ((left & 1) == 1)
                {
                    res = Math.Max(res, _values[left]);
                    left++;
                }
                if ((right & 1) == 0)
                {
                    res = Math.Max(res, _values[right]);
                    right--;
                }
                left /= 2;
                right /= 2;
            }
            return res;
        }
    }
}