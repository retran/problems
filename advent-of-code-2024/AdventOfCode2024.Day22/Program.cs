using System.Text.RegularExpressions;

internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var part1Files = new[]
            {
                ("input_01.txt", "output_01_01.txt", "output_02_01.txt"),
                ("input_02.txt", "output_01_02.txt", "output_02_02.txt")
            };

            foreach (var (inputFile, outputFileForPart1, outputFileForPart2) in part1Files)
            {
                Solve(inputFile, outputFileForPart1, outputFileForPart2);
            }

        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    private record Problem();

    private static void Solve(string inputFile, string outputFileForPart1, string outputFileForPart2)
    {
        if (string.IsNullOrWhiteSpace(inputFile) 
            || string.IsNullOrWhiteSpace(outputFileForPart1)
            || string.IsNullOrWhiteSpace(outputFileForPart2))
        {
            throw new ArgumentException("File paths cannot be null or whitespace.");
        }

        var problem = ParseInput(inputFile);

    }

    private static Problem ParseInput(string file)
    {
        return new Problem();
    }

    private static void WriteOutput(string outputFilePath, long[] output)
    {
        File.WriteAllText(outputFilePath, string.Join(',', output));
    }
}
