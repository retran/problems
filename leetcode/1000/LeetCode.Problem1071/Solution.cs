public class Solution
{
    public string GcdOfStrings(string str1, string str2)
    {
        if (str1.Length < str2.Length)
        {
            return GcdOfStrings(str2, str1);
        }

        if (str2.Length == 0)
        {
            return str1;
        }

        if (str1.StartsWith(str2))
        {
            return GcdOfStrings(str1.Substring(str2.Length), str2);
        }

        return "";
    }
}