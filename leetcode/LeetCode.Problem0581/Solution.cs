public class Solution
{
    public int FindUnsortedSubarray(int[] nums)
    {
        Stack<int> stack = new Stack<int>();
        int left = nums.Length, right = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            while (stack.Count != 0 && nums[stack.Peek()] > nums[i])
            {
                left = Math.Min(left, stack.Pop());
            }
            stack.Push(i);
        }

        stack.Clear();

        for (int i = nums.Length - 1; i >= 0; i--)
        {
            while (stack.Count != 0 && nums[stack.Peek()] < nums[i])
            {
                right = Math.Max(right, stack.Pop());
            }
            stack.Push(i);
        }

        return right - left > 0 ? right - left + 1 : 0;
    }
}
