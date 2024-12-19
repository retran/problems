internal class Program
{
    private record Vector(int X, int Y);

    private static readonly Vector[] Directions =
    {
        new Vector(0, 1),
        new Vector(1, 0),
        new Vector(0, -1),
        new Vector(-1, 0)
    };

    public static void Main(string[] args)
    {
        try
        {
            var partFiles = new[]
            {
                ("input_01.txt", "output_01_01.txt", "output_02_01.txt", 12, 7, 7),
                ("input_02.txt", "output_01_02.txt", "output_02_02.txt", 1024, 71, 71)
            };

            foreach (var (inputFile, outputFileForPart1, outputFileForPart2, steps, width, height) in partFiles)
            {
                Solve(inputFile, outputFileForPart1, outputFileForPart2, steps, width, height);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void Solve(
        string inputFile,
        string outputFileForPart1,
        string outputFileForPart2,
        int steps,
        int width,
        int height)
    {
        if (string.IsNullOrWhiteSpace(inputFile)
            || string.IsNullOrWhiteSpace(outputFileForPart1)
            || string.IsNullOrWhiteSpace(outputFileForPart2))
        {
            throw new ArgumentException("File paths cannot be null or whitespace.");
        }

        var obstacles = ParseInput(inputFile);

        var start = new Vector(0, 0);
        var finish = new Vector(width - 1, height - 1);

        var minSteps = FindPath(obstacles, start, finish, steps, width, height);
        File.WriteAllText(outputFileForPart1, minSteps.ToString());

        var cutoffObstacle = FindCutOffObstacle(obstacles, start, finish, steps, width, height);
        File.WriteAllText(outputFileForPart2, $"{cutoffObstacle.X},{cutoffObstacle.Y}");
    }

    private static Vector FindCutOffObstacle(Vector[] obstacles, Vector start, Vector finish, int steps, int width, int height)
    {
        int left = steps;
        int right = obstacles.Length - 1;

        while (left < right)
        {
            int mid = left + (right - left) / 2;
            if (FindPath(obstacles, start, finish, mid + 1, width, height) != -1)
            {
                left = mid + 1;
            }
            else
            {
                right = mid;
            }
        }

        return obstacles[left];
    }

    private static long FindPath(Vector[] obstacles, Vector start, Vector finish, int steps, int width, int height)
    {
        var obstacleGrid = new bool[width, height];

        for (int i = 0; i < obstacles.Length && i < steps; i++)
        {
            var obs = obstacles[i];
            obstacleGrid[obs.X, obs.Y] = true;
        }

        if (obstacleGrid[start.X, start.Y] || obstacleGrid[finish.X, finish.Y])
        {
            return -1;
        }

        var queue = new Queue<(Vector Position, int Distance)>();
        queue.Enqueue((start, 0));

        var visited = new bool[width, height];
        visited[start.X, start.Y] = true;

        while (queue.Count > 0)
        {
            var (current, currentSteps) = queue.Dequeue();

            if (current == finish)
            {
                return currentSteps;
            }

            foreach (var direction in Directions)
            {
                var next = new Vector(current.X + direction.X, current.Y + direction.Y);

                if (next.X < 0 || next.X >= width || next.Y < 0 || next.Y >= height)
                    continue;

                if (!obstacleGrid[next.X, next.Y] && !visited[next.X, next.Y])
                {
                    visited[next.X, next.Y] = true;
                    queue.Enqueue((next, currentSteps + 1));
                }
            }
        }

        return -1;
    }

    private static Vector[] ParseInput(string file)
    {
        if (!File.Exists(file))
        {
            throw new FileNotFoundException("Input file not found.", file);
        }

        var lines = File.ReadAllLines(file);
        return lines.Select(line =>
        {
            var parts = line.Split(',');
            if (parts.Length != 2)
                throw new FormatException("Each line must contain exactly two integers separated by a comma.");

            if (!int.TryParse(parts[0], out var x) || !int.TryParse(parts[1], out var y))
                throw new FormatException("Unable to parse coordinates from input line.");

            return new Vector(x, y);
        }).ToArray();
    }
}
