public class Solution
{
    private readonly Dictionary<(int, char), int> _cache = new(); 
    private readonly char[] _allowedChars = ['a', 'e', 'i', 'o', 'u'];

    public int CountVowelPermutation(int n, char lastCharacter)
    {
        if (n == 0)
        {
            return 1;
        }

        if (_cache.TryGetValue((n, lastCharacter), out var cached))
        {
            return cached;
        }

        int count = 0;
        switch (lastCharacter)
        {
            case ' ':
                foreach (char character in _allowedChars)
                {
                    count = (count + CountVowelPermutation(n - 1, character)) % 1000000007;
                }
                break;
            case 'a':
                count = (count + CountVowelPermutation(n - 1, 'e')) % 1000000007;
                count = (count + CountVowelPermutation(n - 1, 'u')) % 1000000007;
                count = (count + CountVowelPermutation(n - 1, 'i')) % 1000000007;
                break;
            case 'e':
                count = (count + CountVowelPermutation(n - 1, 'a')) % 1000000007;
                count = (count + CountVowelPermutation(n - 1, 'i')) % 1000000007;
                break;
            case 'i':
                count = (count + CountVowelPermutation(n - 1, 'e')) % 1000000007;
                count = (count + CountVowelPermutation(n - 1, 'o')) % 1000000007;
                break;
            case 'o':
                count = (count + CountVowelPermutation(n - 1, 'i')) % 1000000007;
                break;
            case 'u':
                count = (count + CountVowelPermutation(n - 1, 'o')) % 1000000007;
                count = (count + CountVowelPermutation(n - 1, 'i')) % 1000000007;

                break;
        }

        _cache[(n, lastCharacter)] = count;
        return count;
    }

    public int CountVowelPermutation(int n)
    {
        return CountVowelPermutation(n, ' ');
    }
}