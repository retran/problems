public class Solution
{
    public string ToLowerCase(string s)
    {
        char[] chars = s.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] >= 'A' && chars[i] <= 'Z')
            {
                chars[i] = (char)(chars[i] + 32);
            }
        }

        return new string(chars);
    }
}