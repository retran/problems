using System.Text;

internal class Program
{
    enum EntityType
    {
        Robot,
        Box,
        Wall
    }

    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    private record Vector(long X, long Y);

    private record Entity(EntityType Type, Vector Size)
    {
        public Vector Position { get; set; } = new Vector(0, 0);
    }

    private record Problem(Entity Robot, Entity[] Obstacles, Direction[] Commands);

    private static readonly Dictionary<Direction, Vector> Directions = new Dictionary<Direction, Vector>
    {
        { Direction.Up, new Vector(0, -1) },
        { Direction.Down, new Vector(0, 1) },
        { Direction.Left, new Vector(-1, 0) },
        { Direction.Right, new Vector(1, 0) }
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
        if (string.IsNullOrWhiteSpace(inputFilePath) 
            || string.IsNullOrWhiteSpace(outputFilePathForPart1)
            || string.IsNullOrWhiteSpace(outputFilePathForPart2))
        {
            throw new ArgumentException("File paths cannot be null or whitespace.");
        }

        var (problem1, problem2) = ParseInput(inputFilePath);

        Simulate(problem1);
        long score1 = ComputeScore(problem1);
        File.WriteAllText(outputFilePathForPart1, score1.ToString());

        Simulate(problem2);
        long score2 = ComputeScore(problem2);
        File.WriteAllText(outputFilePathForPart2, score2.ToString());
    }

    private static long ComputeScore(Problem problem)
    {
        long score = 0;
        foreach (var obstacle in problem.Obstacles)
        {
            if (obstacle.Type == EntityType.Box)
            {
                score += obstacle.Position.Y * 100 + obstacle.Position.X;
            }
        }
        return score;
    }

    private static void Simulate(Problem problem)
    {
        foreach (var command in problem.Commands)
        {
            var direction = Directions[command];
            var newPosition = new Vector(problem.Robot.Position.X + direction.X, problem.Robot.Position.Y + direction.Y);

            List<(Entity, Vector)> toMove = [(problem.Robot, newPosition)];

            var queue = new Queue<(Entity, Vector)>();
            queue.Enqueue((problem.Robot, newPosition));

            var visited = new HashSet<Entity>();

            bool cantMove = false;

            while (queue.Count > 0)
            {
                var (current, position) = queue.Dequeue();

                visited.Add(current);

                foreach (var obstacle in problem.Obstacles)
                {
                    if (visited.Contains(obstacle))
                    {
                        continue;
                    }

                    if (IsCollide(position, current.Size, obstacle.Position, obstacle.Size))
                    {
                        switch (obstacle.Type)
                        {
                            case EntityType.Wall:
                                cantMove = true;
                                break;
                            case EntityType.Box:
                                var newObstaclePosition = new Vector(obstacle.Position.X + direction.X, obstacle.Position.Y + direction.Y);
                                queue.Enqueue((obstacle, newObstaclePosition));
                                toMove.Add((obstacle, newObstaclePosition));
                                break;
                            default:
                                break;
                        }
                    }

                    if (cantMove)
                    {
                        break;
                    }
                }

                if (cantMove)
                {
                    break;
                }
            }

            if (!cantMove)
            {
                foreach (var (entity, position) in toMove)
                {
                    entity.Position = position;
                }
            }   
        }
    }

    private static bool IsCollide(Vector positionA, Vector sizeA, Vector positionB, Vector sizeB)
    {
        float leftA = positionA.X;
        float rightA = positionA.X + sizeA.X;
        float topA = positionA.Y;
        float bottomA = positionA.Y + sizeA.Y;

        float leftB = positionB.X;
        float rightB = positionB.X + sizeB.X;
        float topB = positionB.Y;
        float bottomB = positionB.Y + sizeB.Y;

        if (rightA <= leftB || leftA >= rightB || bottomA <= topB || topA >= bottomB)
        {
            return false;
        }

        return true;
    }

    private static (Problem, Problem) ParseInput(string inputFilePath)
    {
        var mapLines = new List<string>();
        var sb = new StringBuilder();

        using (var reader = new StreamReader(inputFilePath))
        {
            string line;
            while ((line = reader.ReadLine()!) != string.Empty)
            {
                mapLines.Add(line);
            }

            while (!reader.EndOfStream)
            {
                sb.Append(reader.ReadLine()!);
            }
        }

        var commands = sb.ToString().Select(c => c switch
            {
                '^' => Direction.Up,
                'v' => Direction.Down,
                '<' => Direction.Left,
                '>' => Direction.Right,
                _ => throw new InvalidOperationException("Invalid command.")
            });

        var obstacles = new List<Entity>();
        var obstacles2 = new List<Entity>();

        var robot = new Entity(EntityType.Robot, new Vector(1, 1));
        var robot2 = new Entity(EntityType.Robot, new Vector(1, 1));

        for (int i = 0; i < mapLines.Count; i++)
        {
            for (int j = 0; j < mapLines[i].Length; j++)
            {
                switch (mapLines[i][j])
                {
                    case '#':
                        obstacles.Add(new Entity(EntityType.Wall, new Vector(1, 1))
                        {
                            Position = new Vector(j , i)
                        });

                        obstacles2.Add(new Entity(EntityType.Wall, new Vector(2, 1))
                        {
                            Position = new Vector(j * 2, i)
                        });
                        break;
                    case 'O':
                        obstacles.Add(new Entity(EntityType.Box, new Vector(1, 1))
                        {
                            Position = new Vector(j, i)
                        });

                        obstacles2.Add(new Entity(EntityType.Box, new Vector(2, 1))
                        {
                            Position = new Vector(j * 2, i)
                        });
                        break;
                    case '@':
                        robot.Position = new Vector(j, i);
                        robot2.Position = new Vector(j * 2, i);
                        break;
                    case '.':
                        break;
                    default:
                        throw new InvalidOperationException("Invalid map character.");
                }
            }
        }

        return (new Problem(robot, obstacles.ToArray(), commands.ToArray()),
            new Problem(robot2, obstacles2.ToArray(), commands.ToArray()));
    }
}
