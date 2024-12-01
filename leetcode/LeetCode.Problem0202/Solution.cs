// https://leetcode.com/problems/happy-number/

public class Solution
{
    private int GetNextNumber(int n) 
    {
        int sum = 0;
        while (n > 0) 
        {
            int digit = n % 10;
            sum += digit * digit;
            n /= 10;
        }
        return sum;
    }

    public bool IsHappy(int n)
    {
        int slow = n;
        int fast = GetNextNumber(n);
        while (fast != 1 && slow != fast) 
        {
            slow = GetNextNumber(slow);
            fast = GetNextNumber(GetNextNumber(fast));
        }
        return fast == 1;
    }
}