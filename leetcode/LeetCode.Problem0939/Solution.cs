public class Solution
{
    public int MinAreaRect(int[][] points)
    {
        var horizontalBuckets = new Dictionary<int, ISet<int>>();

        foreach (var point in points)
        {
            if (!horizontalBuckets.TryGetValue(point[0], out var list))
            {
                list = new HashSet<int>();
                horizontalBuckets[point[0]] = list;
            }

            list.Add(point[1]);
        }

        int[] verticals = horizontalBuckets
            .Where(kv => kv.Value.Count >= 2)
            .Select(kv => kv.Key)
            .OrderBy(v => v)
            .ToArray();

        int min = int.MaxValue;
        bool foundRectangle = false;
        for (int i = 0; i < verticals.Length; i++)
        {
            for (int j = i + 1; j < verticals.Length; j++)
            {
                int x1 = verticals[i];
                int x2 = verticals[j];

                int width = x2 - x1;

                var points1 = horizontalBuckets[x1];
                var points2 = horizontalBuckets[x2];

                var intersection = points1
                    .Intersect(points2)
                    .OrderBy(v => v)
                    .ToArray();

                if (intersection.Length < 2)
                {
                    continue;
                }

                foundRectangle = true;

                int minHeight = int.MaxValue;
                for (int k = 0; k < intersection.Length - 1; k++)
                {
                    int height = intersection[k + 1] - intersection[k];
                    if (height < minHeight)
                    {
                        minHeight = height;
                    }
                }

                var area = minHeight * width;

                if (area < min)
                {
                    min = area;
                }
            }
        }

        if (foundRectangle)
        {
            return min;
        }
        else
        {
            return 0;
        }
    }
}