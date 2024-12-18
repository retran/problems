using System.Text.RegularExpressions;

internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Solve("input_01_01.txt", "output_01_01.txt", false);
            Solve("input_02_01.txt", "output_02_01.txt", true);
            Solve("input_02.txt", "output_01_02.txt", false);
            Solve("input_02.txt", "output_02_02.txt", true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred in Main: {ex.Message}");
        }
    }

    private static void Solve(string inputFileName, string outputFileName, bool conditionals = false)
    {
        var regex = !conditionals
            ? new Regex("mul\\((\\d{1,3}),(\\d{1,3})\\)")
            : new Regex("mul\\((\\d{1,3}),(\\d{1,3})\\)|don't\\(\\)|do\\(\\)");

        long sum = 0;

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

        File.WriteAllText(outputFileName, sum.ToString());
    }
}
