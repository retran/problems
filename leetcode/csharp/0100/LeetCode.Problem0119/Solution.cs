public class Solution
{
    public IList<int> GetRow(int rowIndex)
    {
        List<int> row = new List<int>(rowIndex + 1) { 1 };

        if (rowIndex == 0)
        {
            return row;
        }

        for (int i = 1; i <= rowIndex; i++)
        {
            row.Add(1);
            for (int j = i - 1; j > 0; j--)
            {
                row[j] = row[j] + row[j - 1];
            }
        }

        return row;
    }
}
