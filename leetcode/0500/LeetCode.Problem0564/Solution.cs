public class Solution
{
    public long CreatePalindrome(long n)
    {
        var chars = n.ToString().ToCharArray();
        long left = 0;
        long right = chars.Length - 1;

        while (left < right)
        {
            chars[right] = chars[left];
            left++;
            right--;
        }

        return long.Parse(new string(chars));
    }

    public long SearchPreviousPalindrome(long num)
    {
        long left = 0;
        long right = num;

        var answer = num;
        while (left <= right)
        {
            var mid = left + (right - left) / 2;
            var palindrome = CreatePalindrome(mid);

            if (palindrome < num)
            {
                left = mid + 1;
                answer = palindrome;
            }
            else
            {
                right = mid - 1;
            }

        }

        return answer;
    }

    public long SearchNextPalindrome(long num)
    {
        long left = num;
        long right = long.MaxValue;

        var answer = num;
        while (left <= right)
        {
            var mid = left + (right - left) / 2;
            var palindrome = CreatePalindrome(mid);

            if (palindrome > num)
            {
                right = mid - 1;
                answer = palindrome;
            }
            else
            {
                left = mid + 1;
            }
        }

        return answer;
    }

    public string NearestPalindromic(string n)
    {
        var number = long.Parse(n);
        var next = SearchPreviousPalindrome(number);
        var prev = SearchNextPalindrome(number);

        var nextDiff = Math.Abs(number - next);
        var prevDiff = Math.Abs(number - prev);

        if (nextDiff > prevDiff)
        {
            return prev.ToString();
        } 
        else
        {
            return next.ToString();
        }
    }

    public static void Main(string[] args)
    {
        var solution = new Solution();
        Console.WriteLine(solution.NearestPalindromic("123"));
    }
}