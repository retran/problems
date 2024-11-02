using System.Diagnostics;

var solution = new Solution();

Debug.Assert(solution.IsPerfectSquare(808201));
Debug.Assert(!solution.IsPerfectSquare(-1));
Debug.Assert(solution.IsPerfectSquare(0));
Debug.Assert(solution.IsPerfectSquare(1));
Debug.Assert(solution.IsPerfectSquare(9));
Debug.Assert(solution.IsPerfectSquare(16));
Debug.Assert(solution.IsPerfectSquare(25));
Debug.Assert(!solution.IsPerfectSquare(21));
Debug.Assert(!solution.IsPerfectSquare(45));
Debug.Assert(!solution.IsPerfectSquare(5));
Debug.Assert(!solution.IsPerfectSquare(18));

public class Solution
{
    public bool IsPerfectSquare(int num)
    {
        if (num < 0)
        {
            return false;
        }

        if (num < 2)
        {
            return true;
        }

        long left = 2;
        long right = num / 2;

        while (left <= right)
        {
            long mid = left + (right - left) / 2;
            long square = mid * mid;
            if (square == num)
            {
                return true;
            }
            else if (square > num)
            {
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
            }
        }

        return false;
    }
}