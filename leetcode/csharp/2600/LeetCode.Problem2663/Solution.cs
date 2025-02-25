using System.Text;

public class Solution
{
    private bool HasPalindrome(int[] numbers, int index)
    {
        if (index > 0 && numbers[index] == numbers[index - 1])
        {
            return true;
        }

        if (index > 1 && numbers[index] == numbers[index - 2])
        {
            return true;
        }

        return false;
    }

    public string SmallestBeautifulString(string s, int k)
    {
        var numbers = new int[s.Length];
        for (int i = 0; i < s.Length; i++)
        {
            numbers[i] = s[i] - 'a';
        }

        for (int i = numbers.Length - 1; i >= 0; i--)
        {
            numbers[i] += 1;
            while (HasPalindrome(numbers, i))
            {
                numbers[i] += 1;
            }

            if (numbers[i] < k)
            {
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    numbers[j] = 0;
                    while (HasPalindrome(numbers, j))
                    {
                        numbers[j] += 1;
                    }
                }

                return ToString(numbers);
            }
        }

        return "";
    }

    private static string ToString(int[] numbers)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < numbers.Length; i++)
        {
            sb.Append((char)('a' + numbers[i]));
        }
        return sb.ToString();
    }

    static void Main(string[] args)
    {
        var solution = new Solution();
//        Console.WriteLine(solution.SmallestBeautifulString("abcz", 26));
        Console.WriteLine(solution.SmallestBeautifulString("dc", 4));
    }
}