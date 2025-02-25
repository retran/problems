public class Solution
{
    public int MaxPower(string s)
    {
        int currentChar = s[0];
        int currentLength = 1;
        int maxLength = 1;
        int i = 1;

        while (i < s.Length)
        {
            if (s[i] == currentChar)
            {
                currentLength++;
                if (maxLength < currentLength)
                {
                    maxLength = currentLength;
                }
            }
            else
            {
                currentChar = s[i];
                currentLength = 1;
            }
            i++;
        }

        return maxLength;
    }
}