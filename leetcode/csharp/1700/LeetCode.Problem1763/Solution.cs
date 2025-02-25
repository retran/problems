public class Solution
{
    public bool IsNice(string s, int left, int right)
    {
        bool[,] flags = new bool[2, 26];

        for (int i = left; i <= right; i++)
        {
            if (s[i] >= 'a' && s[i] <= 'z')
            {
                flags[0, s[i] - 'a'] = true;
            }
            else
            {
                flags[1, s[i] - 'A'] = true;
            }
        }

        bool isNice = true;
        for (int i = 0; i < 26; i++)
        {
            if (flags[0, i] != flags[1, i])
            {
                isNice = false;
                break;
            }
        }

        return isNice;
    }

    public string LongestNiceSubstring(string s)
    {
        for (int length = s.Length; length > 1; length--)
        {
            for (int start = 0; start <= s.Length - length; start++)
            {
                if (IsNice(s, start, start + length - 1))
                {
                    return s.Substring(start, length);
                }
            }
        }

        return string.Empty;
    }

    static void Main() 
    {
        var solution = new Solution();
        string s = "Bb";
        string result = solution.LongestNiceSubstring(s);
        System.Console.WriteLine(result);

    }
}