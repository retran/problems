internal class Program
{
    private record Vector(long X, long Y);

    private record Problem(char[,] Map, Vector Start, Vector Finish);

    private enum Direction : int
    {
        East = 0,
        South = 1,
        West = 2,
        North = 3
    }

    private static readonly Dictionary<Direction, Vector> Directions = new()
    {
        { Direction.North, new Vector(0, -1) },
        { Direction.South, new Vector(0, 1) },
        { Direction.West, new Vector(-1, 0) },
        { Direction.East, new Vector(1, 0) }
    };

    public static void Main(string[] args)
    {
        try
        {
            var tasks = new[]
            {
                ("input_01.txt", "output_01_01.txt", "output_02_01.txt"),
                ("input_02.txt", "output_01_02.txt", "output_02_02.txt"),
                ("input_03.txt", "output_01_03.txt", "output_02_03.txt")
            };

            foreach (var (input, outputPart1, outputPart2) in tasks)
            {
                Solve(input, outputPart1, outputPart2);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void Solve(string inputFilePath, string outputFilePathForPart1, string outputFilePathForPart2)
    {
        if (string.IsNullOrWhiteSpace(inputFilePath) ||
            string.IsNullOrWhiteSpace(outputFilePathForPart1) ||
            string.IsNullOrWhiteSpace(outputFilePathForPart2))
        {
            throw new ArgumentException("File paths cannot be null or whitespace.");
        }

        var problem = ParseInput(inputFilePath);
        var (score, uniqueVisitedCount) = FindShortestPath(problem);

        File.WriteAllText(outputFilePathForPart1, score.ToString());
        File.WriteAllText(outputFilePathForPart2, uniqueVisitedCount.ToString());
    }

    private static (long, int) FindShortestPath(Problem problem)
    {
        var queue = new Queue<(Vector, Direction, long, ISet<Vector>)>();

        long[,,] minScores = new long[problem.Map.GetLength(0), problem.Map.GetLength(1), 4];
        for (int i = 0; i < minScores.GetLength(0); i++)
        {
            for (int j = 0; j < minScores.GetLength(1); j++)
            {
                for (int k = 0; k < minScores.GetLength(2); k++)
                {
                    minScores[i, j, k] = long.MaxValue;
                }
            }
        }

        foreach (var direction in Enum.GetValues<Direction>())
        {
            queue.Enqueue((problem.Start, direction, 1000, new HashSet<Vector>())); // Initial rotation cost
        }

        long minScore = long.MaxValue;
        var minPaths = new List<ISet<Vector>>();

        while (queue.Count > 0)
        {
            var (current, currentDirection, currentScore, currentVisited) = queue.Dequeue();

            if (currentScore > minScores[current.X, current.Y, (int)currentDirection])
                continue;

            minScores[current.X, current.Y, (int)currentDirection] = currentScore;
            
            currentVisited = new HashSet<Vector>(currentVisited)
            {
                current
            };

            if (current == problem.Finish)
            {
                if (currentScore < minScore)
                {
                    minScore = currentScore;
                    minPaths = new List<ISet<Vector>> { currentVisited };
                }

                if (currentScore == minScore)
                {
                    minPaths.Add(currentVisited);
                }
                continue;
            }

            foreach (var (direction, directionVector) in Directions)
            {
                var newPosition = new Vector(current.X + directionVector.X, current.Y + directionVector.Y);

                if (newPosition.X < 0 || newPosition.X >= problem.Map.GetLength(0) ||
                    newPosition.Y < 0 || newPosition.Y >= problem.Map.GetLength(1) ||
                    problem.Map[newPosition.X, newPosition.Y] == '#')
                {
                    continue;
                }

                var directionDiff = Math.Min(
                    Math.Abs((int)currentDirection - (int)direction),
                    4 - Math.Abs((int)currentDirection - (int)direction)
                );

                long newScore = currentScore + directionDiff * 1000 + 1;
                queue.Enqueue((newPosition, direction, newScore, currentVisited));
            }
        }

        var allBestPaths = new HashSet<Vector>();
        foreach (var path in minPaths)
        {
            allBestPaths.UnionWith(path);
        }

        return (minScore, allBestPaths.Count);
    }

    private static Problem ParseInput(string inputFilePath)
    {
        var mapLines = File.ReadAllLines(inputFilePath);
        char[,] map = new char[mapLines.Length, mapLines[0].Length];
        Vector start = new(0, 0);
        Vector finish = new(0, 0);

        for (int i = 0; i < mapLines.Length; i++)
        {
            for (int j = 0; j < mapLines[i].Length; j++)
            {
                if (mapLines[i][j] == 'S')
                {
                    start = new Vector(j, i);
                    map[j, i] = '.';
                }
                else if (mapLines[i][j] == 'E')
                {
                    finish = new Vector(j, i);
                    map[j, i] = '.';
                }
                else
                {
                    map[j, i] = mapLines[i][j];
                }
            }
        }

        return new Problem(map, start, finish);
    }
}
