public class Solution
{
    const long a = 31;
    const long m = 1_000_000_007;

    private class RollingHash
    {
        private long hash;
        private long p;

        public RollingHash(string initial)
        {
            hash = 0;
            p = 1;

            for (int i = 0; i < initial.Length - 1; i++)
            {
                p = (p * a) % m;
            }

            foreach (var c in initial)
            {
                hash = (hash * a + c) % m;
            }
        }

        public void Next(char remove, char add)
        {
            hash = (hash - remove * p % m + m) % m;
            hash = (hash * a + add) % m;
        }

        public long GetHash()
        {
            return hash;
        }
    }

    public string AddBoldTag(string s, string[] words)
    {
        var bold = new bool[s.Length];

        for (int j = 0; j < words.Length; j++)
        {
            var word = words[j];

            if (word.Length > s.Length)
            {
                continue;
            }

            long hash = new RollingHash(word).GetHash();

            var rh = new RollingHash(s.Substring(0, word.Length));

            int k = word.Length - 1;
            while (k < s.Length)
            {
                if (rh.GetHash() == hash)
                {
                    var substring = s.Substring(k - word.Length + 1, word.Length);
                    if (word == substring)
                    {
                        for (int l = k - word.Length + 1; l < k + 1 && l < s.Length; l++)
                        {
                            bold[l] = true;
                        }
                    }
                }

                k++;
                if (k < s.Length)
                {
                    rh.Next(s[k - word.Length], s[k]);
                }
            }
        }

        StringBuilder sb = new StringBuilder();
        for (int k = 0; k < s.Length; k++)
        {
            if (bold[k] && (k == 0 || !bold[k - 1]))
            {
                sb.Append("<b>");
            }
            sb.Append(s[k]);
            if (bold[k] && (k == s.Length - 1 || !bold[k + 1]))
            {
                sb.Append("</b>");
            }
        }
        return sb.ToString();
    }
}