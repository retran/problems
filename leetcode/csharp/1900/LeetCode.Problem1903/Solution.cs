public class Solution
{
    private int CharToInt(char c)
    {
        return c - '0';
    }

    public string LargestOddNumber(string num)
    {
        for (int i = num.Length - 1; i >= 0; i--)
        {
            if (CharToInt(num[i]) % 2 == 1)
            {
                return num.Substring(0, i + 1);
            }
        }

        return string.Empty;
    }
}