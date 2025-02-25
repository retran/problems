public class Solution
{
    public int PartitionString(string s)
    {
        int count = 1;

        HashSet<char> seen = new HashSet<char>();

        for (int i = 0; i < s.Length; i++)
        {
            if (!seen.Add(s[i]))
            {
                count++;
                seen.Clear();
                seen.Add(s[i]);
            }
        }

        return count;
    }
}