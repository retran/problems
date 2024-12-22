public class Solution
{
    public IList<int> LuckyNumbers(int[][] matrix)
    {
        var minimums = new int[matrix.Length];
        var maximums = new int[matrix[0].Length];

        for (int i = 0; i < matrix.Length; i++)
        {
            minimums[i] = matrix[i].Min();
        }

        for (int j = 0; j < matrix[0].Length; j++)
        {
            maximums[j] = matrix.Select(row => row[j]).Max();
        }

        return minimums.Intersect(maximums).ToList();
    }
}