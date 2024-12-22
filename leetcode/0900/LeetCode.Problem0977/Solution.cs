public class Solution
{
    public int[] SortedSquares(int[] nums)
    {
        int n = nums.Length;
        int[] squares = new int[n];
        int left = 0;
        int right = n - 1;
        int k = n - 1;

        while (k >= 0)
        {
            int leftSquare = nums[left] * nums[left];
            int rightSquare = nums[right] * nums[right];

            if (leftSquare > rightSquare)
            {
                squares[k] = leftSquare;
                left++;
            }
            else
            {
                squares[k] = rightSquare;
                right--;
            }
            k--;
        }

        return squares;
    }
}
