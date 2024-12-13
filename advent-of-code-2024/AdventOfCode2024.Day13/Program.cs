using System.Text.RegularExpressions;

internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var tasks = new[]
            {
                ("input_01.txt", "output_01_01.txt", "output_02_01.txt"),
                ("input_02.txt", "output_01_02.txt", "output_02_02.txt")
            };

            foreach (var (input, output1, output2) in tasks)
            {
                ProcessInputAndWriteOutput(input, output1, output2);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private record Vector(long X, long Y);

    private record Problem(Vector ButtonA, Vector ButtonB, Vector Target);

    private static void ProcessInputAndWriteOutput(string inputFilePath, string outputFilePathForPart1, string outputFilePathForPart2)
    {
        if (string.IsNullOrWhiteSpace(inputFilePath) ||
            string.IsNullOrWhiteSpace(outputFilePathForPart1) ||
            string.IsNullOrWhiteSpace(outputFilePathForPart2))
        {
            throw new ArgumentException("File paths cannot be null or whitespace.");
        }

        var problems = ReadInput(inputFilePath);
        long totalCost1 = 0;
        long totalCost2 = 0;

        foreach (var problem in problems)
        {
            totalCost1 += CalculateCost(problem, problem.Target);
            totalCost2 += CalculateCost(problem, new Vector(problem.Target.X + 10000000000000, problem.Target.Y + 10000000000000));
        }

        File.WriteAllText(outputFilePathForPart1, totalCost1.ToString());
        File.WriteAllText(outputFilePathForPart2, totalCost2.ToString());
    }

    private static long CalculateCost(Problem problem, Vector adjustedTarget)
    {
        var matrix = new long[,]
        {
            {problem.ButtonA.X, problem.ButtonB.X, -adjustedTarget.X},
            {problem.ButtonA.Y, problem.ButtonB.Y, -adjustedTarget.Y}
        };

        var result = Solve(matrix);

        return result != null ? result[0] * 3 + result[1] : 0;
    }

    private static long[]? Solve(long[,] matrix)
    {
        long determinant = matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

        if (determinant == 0) 
        {
            return null;
        }

        long determinantA = -matrix[0, 2] * matrix[1, 1] + matrix[0, 1] * matrix[1, 2];
        long determinantB = matrix[0, 0] * matrix[1, 2] - matrix[1, 0] * matrix[0, 2];

        double a = (double)determinantA / determinant;
        double b = (double)determinantB / determinant;

        if (Math.Abs(a - (long)a) > 0.0001 || Math.Abs(b - (long)b) > 0.0001)
        {
            return null;
        }

        return [(long)a, (long)b];
    }

    private static IEnumerable<Problem> ReadInput(string inputFilePath)
    {
        var input = File.ReadAllText(inputFilePath);
        const string pattern = @"Button A: X\+(\d+), Y\+(\d+)\r?\nButton B: X\+(\d+), Y\+(\d+)\r?\nPrize: X=(\d+), Y=(\d+)";

        var regex = new Regex(pattern);
        var problems = new List<Problem>();

        foreach (Match match in regex.Matches(input))
        {
            if (match.Groups.Count == 7)
            {
                problems.Add(new Problem(
                    new Vector(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)),
                    new Vector(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value)),
                    new Vector(int.Parse(match.Groups[5].Value), int.Parse(match.Groups[6].Value))
                ));
            }
        }

        return problems;
    }
}
