using System.Text.RegularExpressions;

internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Execute("input_01_01.txt", "output_01_01.txt", false);
            Execute("input_02_01.txt", "output_02_01.txt", true);
            Execute("input_02.txt", "output_01_02.txt", false);
            Execute("input_02.txt", "output_02_02.txt", true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred in Main: {ex.Message}");
        }
    }

    private static void Execute(string inputFileName, string outputFileName, bool conditionals = false)
    {
        var regex = !conditionals
            ? new Regex("mul\\((\\d{1,3}),(\\d{1,3})\\)")
            : new Regex("mul\\((\\d{1,3}),(\\d{1,3})\\)|don't\\(\\)|do\\(\\)");

        long sum = 0;

        try
        {
            using (var reader = new StreamReader(inputFileName))
            {
                string? line = reader.ReadToEnd();
                if (string.IsNullOrWhiteSpace(line))
                {
                    throw new InvalidDataException("Invalid data in the input file.");
                }

                bool enabled = true;
                var matches = regex.Matches(line);
                foreach (Match match in matches)
                {
                    switch (match.Value)
                    {
                        case "don't()":
                            enabled = false;
                            break;
                        case "do()":
                            enabled = true;
                            break;
                        default:
                            if (enabled)
                            {
                                sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                            }
                            break;
                    }
                }
            }

            using (var writer = new StreamWriter(outputFileName))
            {
                writer.WriteLine(sum);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while processing the file '{inputFileName}': {ex.Message}");
        }
    }
}
