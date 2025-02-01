using System.Text;

public class RollingHash
{
    private const long mod = 1000000007;
    private const long baseVal = 31;
    private readonly bool isForward;
    private long hash;
    private long power;
    
    public RollingHash(bool isForward)
    {
        this.isForward = isForward;
        hash = 0;
        power = 1;
    }
    
    public void Add(char c)
    {
        int val = c - 'a' + 1;
        if (isForward)
            hash = (hash * baseVal + val) % mod;
        else
            hash = (hash + val * power) % mod;
        power = (power * baseVal) % mod;
    }
    
    public long GetHash() => hash;
}

public class Solution
{
    public string ShortestPalindrome(string s)
    {
        int n = s.Length;
        var forwardHash = new RollingHash(true);
        var backwardHash = new RollingHash(false);
        int longestPalPrefix = -1;
        for (int i = 0; i < n; i++)
        {
            forwardHash.Add(s[i]);
            backwardHash.Add(s[i]);
            if (forwardHash.GetHash() == backwardHash.GetHash())
                longestPalPrefix = i;
        }
        var sb = new StringBuilder();
        for (int i = n - 1; i > longestPalPrefix; i--)
            sb.Append(s[i]);
        sb.Append(s);
        return sb.ToString();
    }
}
