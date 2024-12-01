public class Solution
{
    public bool ConfusingNumber(int n)
    {
        var digitsMap = new Dictionary<int, int>()
        {
            { 0, 0 },
            { 1, 1 },
            { 6, 9 },
            { 8, 8 },
            { 9, 6 }
        };

        int reversed = 0;

        int number = n;
        while (number > 0)
        {
            int digit = number % 10;
            if (!digitsMap.TryGetValue(digit, out var rotatedDigit))
            {
                return false;
            }
            reversed = reversed * 10 + rotatedDigit;
            number = number / 10;
        }

        return reversed != n;
    }
}
