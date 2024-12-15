using System.Drawing;
using System.Text.RegularExpressions;

internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var tasks = new[]
            {
                ("input_01.txt", "output_01_01.txt", "./part02_01/", 11, 7),
                ("input_02.txt", "output_01_02.txt", "./part02_02/", 101, 103)
            };

            foreach (var (input, outputPart1, outputPart2Dir, width, height) in tasks)
            {
                Solve(input, outputPart1, outputPart2Dir, width, height);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private record Vector(long X, long Y);

    private record Robot(Vector Position, Vector Velocity);

    private static void Solve(string inputFilePath, string outputPart1Path, string outputPart2Dir, int width, int height)
    {
        if (string.IsNullOrWhiteSpace(inputFilePath) || string.IsNullOrWhiteSpace(outputPart1Path))
        {
            throw new ArgumentException("File paths cannot be null or whitespace.");
        }

        var robots = ParseInput(inputFilePath);
        var movedRobots = SimulateRobots(robots, width, height, 100);
        
        int safetyFactor = ComputeSafetyFactor(movedRobots, width, height);
        File.WriteAllText(outputPart1Path, safetyFactor.ToString());

        GenerateRobotMovementVisualization(robots, width, height, 10000, outputPart2Dir);
    }

    private static void GenerateRobotMovementVisualization(IEnumerable<Robot> robots, int width, int height, int totalSeconds, string outputDirectory)
    {
        EnsureDirectoryExists(outputDirectory);

        SaveVisualization(robots, width, height, outputDirectory, 0);

        for (int t = 27; t <= totalSeconds; t += 101)
        {
            var updatedRobots = SimulateRobots(robots, width, height, t);
            SaveVisualization(updatedRobots, width, height, outputDirectory, t);
        }
    }

    private static void SaveVisualization(IEnumerable<Robot> robots, int width, int height, string outputDirectory, int index)
    {
        string fileName = Path.Combine(outputDirectory, index.ToString("D4") + ".png");

        using var bitmap = new Bitmap(width, height);
        using (Graphics gfx = Graphics.FromImage(bitmap))
        {
            gfx.Clear(Color.Black);
            foreach (var robot in robots)
            {
                if (robot.Position.X >= 0 && robot.Position.X < width && robot.Position.Y >= 0 && robot.Position.Y < height)
                {
                    bitmap.SetPixel((int)robot.Position.X, (int)robot.Position.Y, Color.Green);
                }
            }
        }

        using var resizedBitmap = new Bitmap(bitmap, new Size(width * 4, height * 4));
        resizedBitmap.Save(fileName);
    }

    private static int ComputeSafetyFactor(IEnumerable<Robot> robots, int width, int height)
    {
        int[] quadrantCounts = new int[4];
        int centerX = width / 2;
        int centerY = height / 2;

        foreach (var robot in robots)
        {
            if (robot.Position.X < centerX && robot.Position.Y < centerY) quadrantCounts[0]++;
            else if (robot.Position.X >= centerX && robot.Position.Y < centerY) quadrantCounts[1]++;
            else if (robot.Position.X < centerX && robot.Position.Y >= centerY) quadrantCounts[2]++;
            else if (robot.Position.X >= centerX && robot.Position.Y >= centerY) quadrantCounts[3]++;
        }

        return quadrantCounts.Aggregate(1, (product, count) => product * count);
    }

    private static IEnumerable<Robot> SimulateRobots(IEnumerable<Robot> robots, int width, int height, int seconds)
    {
        return robots.Select(robot => MoveRobot(robot, width, height, seconds));
    }

    private static Robot MoveRobot(Robot robot, int width, int height, int seconds)
    {
        long newX = (robot.Position.X + robot.Velocity.X * seconds) % width;
        long newY = (robot.Position.Y + robot.Velocity.Y * seconds) % height;

        newX = (newX + width) % width;
        newY = (newY + height) % height;

        return new Robot(new Vector(newX, newY), robot.Velocity);
    }

    private static IEnumerable<Robot> ParseInput(string inputFilePath)
    {
        var robots = new List<Robot>();
        var regex = new Regex(@"p=(-?\d+),(-?\d+) v=(-?\d+),(-?\d+)");

        foreach (Match match in regex.Matches(File.ReadAllText(inputFilePath)))
        {
            var position = new Vector(long.Parse(match.Groups[1].Value), long.Parse(match.Groups[2].Value));
            var velocity = new Vector(long.Parse(match.Groups[3].Value), long.Parse(match.Groups[4].Value));
            robots.Add(new Robot(position, velocity));
        }

        return robots;
    }

    private static void EnsureDirectoryExists(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }
}
