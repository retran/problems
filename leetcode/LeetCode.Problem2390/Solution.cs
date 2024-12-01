public class Solution
{
    public string RemoveStars(string s)
    {
        var chars = s.ToCharArray();
        int i = 0;
        int j = 0;

        while (j < s.Length)
        {
            if (chars[j] != '*')
            {
                chars[i] = chars[j];
                i++;
            }
            else
            {
                i--;
            }
            j++;
        }

        return new string(chars).Substring(0, i);
    }
}