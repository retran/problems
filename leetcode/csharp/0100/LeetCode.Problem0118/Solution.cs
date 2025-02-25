public class Solution
{
    public IList<IList<int>> Generate(int numRows)
    {
        var result = new List<IList<int>>();
        if (numRows == 0)
        {
            return result;
        }

        if (numRows >= 1)
        {
            result.Add(new List<int> {1});
        }

        if (numRows >= 2)
        {
            result.Add(new List<int> {1, 1});
        }

        for (int i = 2; i < numRows; i++)
        {
            var row = new List<int>();
            row.Add(1);
            for (int j = 1; j < i; j++)
            {
                row.Add(result[i - 1][j - 1] + result[i - 1][j]);
            }

            row.Add(1);
            result.Add(row);
        }

        return result;
    }
}