public class Solution
{
    private LinkedList<char> TypeValue(string value)
    {
        var list = new LinkedList<char>();
        foreach (var c in value)
        {
            if (c == '#')
            {
                if (list.Count > 0)
                {
                    list.RemoveLast();
                }
            }
            else
            {
                list.AddLast(c);
            }
        }
        return list;
    }

    public bool BackspaceCompare(string s, string t)
    {
        var first = TypeValue(s);
        var second = TypeValue(t);

        return first.SequenceEqual(second);
    }
}