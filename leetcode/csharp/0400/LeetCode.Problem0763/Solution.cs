public class Solution
{
    public IList<int> PartitionLabels(string s)
    {
        var last = new Dictionary<char, int>();

        for (int i = 0; i < s.Length; i++)
        {
            last[s[i]] = i;
        }

        var answer = new List<int>();
        int first = 0;
        int j = 0;
        for (int i = 0; i < s.Length; i++)
        {
            j = Math.Max(j, last[s[i]]);
            if (j == i)
            {
                answer.Add(j - first + 1);
                first = j + 1;
            }
        }

        return answer;
    }
}