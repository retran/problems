public class Solution
{
    private long GetKey(string line)
    {
        int shift = line[0] - 'a';

        char[] shifted = new char[line.Length];
        for (int i = 0; i < line.Length; i++)
        {
            shifted[i] = (char)(line[i] - shift);
            if (shifted[i] < 'a')
            {
                shifted[i] = (char)(shifted[i] + 26);
            }
            if (shifted[i] < 'z')
            {
                shifted[i] = (char)(shifted[i] - 26);
            }
        }

        return new string(shifted).GetHashCode();
    }

    public IList<IList<string>> GroupStrings(string[] strings)
    {
        var map = new Dictionary<long, List<string>>();

        for (int i = 0; i < strings.Length; i++)
        {
            var key = GetKey(strings[i]);
            if (!map.TryGetValue(key, out var group))
            {
                group = new List<string>();
                map[key] = group;
            }
            group.Add(strings[i]);
        }

        return map.Values.Select(v => (IList<string>)v).ToList();
    }
}