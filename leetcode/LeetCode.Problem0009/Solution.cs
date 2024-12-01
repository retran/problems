public class Solution {
    public bool IsPalindrome(int x) {
        if (x < 0) 
        {
            return false;
        }

        int reverse = 0;
        int tmp = x;

        while (tmp != 0) {
            int remainder = tmp % 10;
            tmp = tmp / 10;
            reverse = reverse * 10 + remainder;
        }

        return reverse == x;
    }
}