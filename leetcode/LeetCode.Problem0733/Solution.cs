public class Solution
{
    private (int row, int column)[] _directions = new[] {
        (-1, 0),
        (1, 0),
        (0, -1),
        (0, 1)
    };

    public int[][] FloodFill(int[][] image, int sr, int sc, int color)
    {
        if (image.Length == 0 || image[0].Length == 0)
        {
            return image;
        }

        if (sr < 0 || sr >= image.Length || sc < 0 || sc >= image[0].Length)
        {
            return image;
        }

        int initialColor = image[sr][sc];

        if (initialColor == color)
        {
            return image;
        }

        var queue = new Queue<(int row, int column)>();
        queue.Enqueue((sr, sc));

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            image[current.row][current.column] = color;

            foreach (var direction in _directions)
            {
                var next = (row: current.row + direction.row, column: current.column + direction.column);
                if (next.row >= 0 && next.row < image.Length 
                    && next.column >= 0 && next.column < image[0].Length
                    && image[next.row][next.column] == initialColor)
                    {
                        queue.Enqueue(next);
                    }
            }
        }

        return image;
    }
}