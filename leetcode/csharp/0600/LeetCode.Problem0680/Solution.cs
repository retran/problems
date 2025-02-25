public class Solution
{
    public bool ValidPalindromeImpl(string s, int left, int right, int deletes) 
    {
        if (s.Length < 2)
        {
            return true;
        }

        while (left < right) 
        {
            if (s[left] == s[right]) 
            {
                left++;
                right--;
                continue;
            }

            if (deletes == 0)
            {
                return false;
            }
            else
            {
                return ValidPalindromeImpl(s, left + 1, right, deletes - 1)
                    || ValidPalindromeImpl(s, left, right - 1, deletes - 1);
            }
        }

        return true;
    }

    public bool ValidPalindrome(string s)
    {
        return ValidPalindromeImpl(s, 0, s.Length - 1, 1);
    }
}