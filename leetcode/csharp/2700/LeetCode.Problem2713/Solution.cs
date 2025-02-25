public class Solution
{
    public int MaxIncreasingCells(int[][] mat)
    {
        int n = mat.Length;
        int m = mat[0].Length;

        var cells = new List<(int value, int i, int j)>();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                cells.Add((mat[i][j], i, j));
            }
        }
        
        cells.Sort((a, b) => a.value.CompareTo(b.value));
        
        int[] rowMax = new int[n];
        int[] colMax = new int[m];
        int result = 0;
        int idx = 0;
        
        while (idx < cells.Count)
        {
            int start = idx;
            var group = new List<(int i, int j, int dp)>();
            
            while (idx < cells.Count && cells[idx].value == cells[start].value)
            {
                var (val, i, j) = cells[idx];
                int dp = Math.Max(rowMax[i], colMax[j]) + 1;
                group.Add((i, j, dp));
                result = Math.Max(result, dp);
                idx++;
            }
            
            foreach (var (i, j, dp) in group)
            {
                rowMax[i] = Math.Max(rowMax[i], dp);
                colMax[j] = Math.Max(colMax[j], dp);
            }
        }
        
        return result;
    }
}