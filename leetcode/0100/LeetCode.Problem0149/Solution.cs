// [[3,3],[1,4],[1,1],[2,1],[2,2]]

public class Solution
{
    public (double, double) GetLine(int[] pointA, int[] pointB)
    {
        if (pointA[0] == pointB[0])
        {
            return (int.MaxValue, pointA[0]);
        }

        if (pointB[0] < pointA[0])
        {
            return GetLine(pointB, pointA);
        }

        double k = (double) (pointA[1] - pointB[1]) / (pointA[0] - pointB[0]);
        double y0 = pointA[1] - k * pointA[0];

        return (k, y0);
    }

    public int MaxPoints(int[][] points)
    {
        if (points.Count() < 3)
        {
            return points.Count();
        }

        var pointCount = new Dictionary<(double, double), HashSet<int[]>>();

        for (int i = 0; i < points.Length; i++)
        {
            for (int j = 0; j < points.Length; j++) 
            {
                if (i == j)
                {
                    continue;
                }

                var line = GetLine(points[i], points[j]);
                
                System.Console.WriteLine($"{points[i][0]} {points[i][1]} {points[j][0]} {points[j][1]} {line.Item1} {line.Item2}");

                if (pointCount.ContainsKey(line))
                {
                    pointCount[line].Add(points[i]);
                    pointCount[line].Add(points[j]);
                }
                else
                {
                    pointCount.Add(line, new HashSet<int[]>{points[i], points[j]});
                }
            }
        }

        return pointCount.Values.Max(v => v.Count());
    }
}