public class Solution
{
    public string LongestCommonPrefix(string[] strs)
    {
        if (strs.Length == 0)
        {
            return string.Empty;
        }

        if (strs.Length == 1)
        {
            return strs[0];
        }

        var indexOfPrefix = 0;
        var lengthOfPrefix = int.MaxValue;
        var prefix = string.Empty;

        for (int i = 0; i < strs.Length; i++)
        {
            var str = strs[i];
            if (str.Length < lengthOfPrefix)
            {
                lengthOfPrefix = str.Length;
                prefix = str;
                indexOfPrefix = i;
            }
        }

        if (lengthOfPrefix == 0)
        {
            return string.Empty;
        }

        for (var i = 0; i < strs.Length; i++)
        {
            if (i == indexOfPrefix)
            {
                continue;
            }

            var j = 0;
            while (j < lengthOfPrefix && j < strs[i].Length && prefix[j] == strs[i][j])
            {
                j++;
            }

            lengthOfPrefix = j;
        }

        return prefix.Substring(0, lengthOfPrefix);
    }
}