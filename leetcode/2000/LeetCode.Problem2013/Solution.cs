public class DetectSquares
{
    private record Point(int X, int Y);

    private IDictionary<int, IList<Point>> pointsByY = new Dictionary<int, IList<Point>>();
    private IDictionary<Point, int> countByPoint = new Dictionary<Point, int>();

    public DetectSquares() { }

    public void Add(int[] point)
    {
        var p = ToPoint(point);
        EnsureExists(pointsByY, p.Y).Add(p);

        if (!countByPoint.TryGetValue(p, out var count))
        {
            count = 0;
        }

        countByPoint[p] = count + 1;
    }

    private int CountSquares(Point firstPoint, Point secondPoint)
    {
        if (firstPoint.X > secondPoint.X)
        {
            return CountSquares(secondPoint, firstPoint);
        }

        int size = Math.Abs(secondPoint.X - firstPoint.X);

        int count = 0;
        foreach (var side in new[] { -size, size })
        {
            var thirdPoint = new Point(firstPoint.X, firstPoint.Y + side);
            var fourthPoint = new Point(secondPoint.X, secondPoint.Y + side);

            if (!countByPoint.TryGetValue(thirdPoint, out var thirdCount) || thirdCount == 0)
            {
                continue;
            }

            if (!countByPoint.TryGetValue(fourthPoint, out var fourthCount) || fourthCount == 0)
            {
                continue;
            }

            count += thirdCount * fourthCount;
        }

        return count;
    }

    private IList<Point> EnsureExists(IDictionary<int, IList<Point>> pointsByValue, int value)
    {
        if (!pointsByValue.TryGetValue(value, out var points))
        {
            points = new List<Point>();
            pointsByValue[value] = points;
        }
        return points;
    }

    public int Count(int[] point)
    {
        var p = ToPoint(point);

        var points = EnsureExists(pointsByY, p.Y);

        int count = 0;
        foreach (var secondPoint in points)
        {
            if (secondPoint.X == p.X)
            {
                continue;
            }

            count += CountSquares(p, secondPoint);
        }

        return count;
    }

    private Point ToPoint(int[] point) => new Point(point[0], point[1]);

    static void Main()
    {
        var ds = new DetectSquares();
        ds.Add([3, 10]);
        ds.Add([11, 2]);
        ds.Add([3, 2]);
        Console.WriteLine(ds.Count([11, 10]));
        Console.WriteLine(ds.Count([14, 8]));
        ds.Add([11, 2]);
        Console.WriteLine(ds.Count([11, 10]));
    }
}
