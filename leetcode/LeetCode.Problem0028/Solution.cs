public class Solution
{
    private const long baseValue = 31;
    private const long modulusValue = 1000000007;

    public static long BaseHash(string text, int windowSize)
    {
        long hashValue = 0;
        long basePower = 1;

        for (int i = 0; i < windowSize; i++)
        {
            hashValue = (hashValue * baseValue + text[i]) % modulusValue;
            if (i < windowSize - 1)
                basePower = (basePower * baseValue) % modulusValue;
        }

        return hashValue;
    }

    public static long NextHash(long prevHash, char oldChar, char newChar, int windowSize)
    {
        long basePower = 1;
        for (int i = 0; i < windowSize - 1; i++)
            basePower = (basePower * baseValue) % modulusValue;

        long newHash = (prevHash + modulusValue - (basePower * oldChar) % modulusValue) % modulusValue;
        newHash = (newHash * baseValue) % modulusValue;
        newHash = (newHash + newChar) % modulusValue;

        return newHash;
    }

    public int StrStr(string haystack, string needle)
    {
        if (string.IsNullOrEmpty(needle))
        {
            return 0;
        }

        if (haystack.Length < needle.Length) 
        {
            return -1;
        }

        long needleHash = BaseHash(needle, needle.Length);
        long currentHash = BaseHash(haystack, needle.Length);

        if (needleHash == currentHash && haystack.Substring(0, needle.Length) == needle)
        {
            return 0;
        }

        for (int i = 1; i <= haystack.Length - needle.Length; i++) 
        {
            currentHash = NextHash(currentHash, haystack[i - 1], haystack[i + needle.Length - 1], needle.Length);
            if (needleHash == currentHash && haystack.Substring(i, needle.Length) == needle)
            {
                return i;
            }
        }

        return -1;
    }
}