public class Solution
{
    public int FindMinArrowShots(int[][] points)
    {
        Array.Sort(points, (a, b) => a[0].CompareTo(b[0]));
        List<int[]> merged = new List<int[]>();

        foreach (int[] point in points)
        {
            if (merged.Count == 0 || merged.Last()[1] < point[0])
            {
                merged.Add(point);
            }
            else
            {
                merged.Last()[0] = Math.Max(merged.Last()[0], point[0]);
                merged.Last()[1] = Math.Min(merged.Last()[1], point[1]);
            }
        }

        return merged.Count;
    }
}