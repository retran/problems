public class Solution
{
    public string Convert(string s, int numRows)
    {
        if (numRows == 1) 
        {
            return s;
        }

        if (numRows > s.Length)
        {
            return s;
        }

        var offset = 2 * numRows - 2;

        var result = new StringBuilder(s.Length);

        for (var i = 0; i < numRows; i++)
        {
            var j = i;
            while (j < s.Length)
            {
                result.Append(s[j]);
                if (i != 0 && i != numRows - 1)
                {
                    var next = j + offset - 2 * i;
                    if (next < s.Length) result.Append(s[next]);
                }

                j += offset;
            }
        }

        return result.ToString();
    }
}