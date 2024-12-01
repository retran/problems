public class Solution
{
    public int MySqrt(int x)
    {
        if (x == 0)
        {
            return 0;
        }

        long left = 1;
        long right = x;

        while (left <= right)
        {
            long mid = left + (right - left) / 2;
            long squareOfMid = mid * mid;

            if (squareOfMid <= x && (mid + 1) * (mid + 1) > x)
            {
                return (int)mid;
            }
            else if (squareOfMid > x)
            {
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
            }
        }

        return (int)right;
    }
}