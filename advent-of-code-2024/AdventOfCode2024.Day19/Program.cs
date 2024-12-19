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

    private record Problem(string[] Patterns, string[] Designs);

    private static void Solve(string inputFile, string outputFileForPart1, string outputFileForPart2)
    {
        if (string.IsNullOrWhiteSpace(inputFile) 
            || string.IsNullOrWhiteSpace(outputFileForPart1)
            || string.IsNullOrWhiteSpace(outputFileForPart2))
        {
            throw new ArgumentException("File paths cannot be null or whitespace.");
        }

        var problem = ParseInput(inputFile);

        var cache = new Dictionary<string, long>();

        long possibleDesigns = 0;
        long possibleDesignsVariants = 0;
        foreach (var design in problem.Designs)
        {
            var possibleDesignVariants = CountPossibleDesignVariants(cache, problem.Patterns, design);

            if (possibleDesignVariants != 0)
            {
                possibleDesigns++;
                possibleDesignsVariants += possibleDesignVariants;
            }
        }

        File.WriteAllText(outputFileForPart1, possibleDesigns.ToString());
        File.WriteAllText(outputFileForPart1, possibleDesignsVariants.ToString());
    }

    private static long CountPossibleDesignVariants(Dictionary<string, long> cache, string[] patterns, string design)
    {
        if (string.IsNullOrWhiteSpace(design))
        {
            return 1;
        }

        if (cache.TryGetValue(design, out var cachedCount))
        {
            return cachedCount;
        }

        long count = 0;
        foreach (var pattern in patterns)
        {
            if (design.StartsWith(pattern))
            {
                count += CountPossibleDesignVariants(cache, patterns, design.Substring(pattern.Length));
            }
        }

        cache[design] = count;

        return count;
    }

    private static Problem ParseInput(string file)
    {
        using var reader = new StreamReader(file);
        
        var line = reader.ReadLine()!;
        var patterns = line.Split(',').Select(p => p.Trim()).ToArray();
        reader.ReadLine();

        var designs = new List<string>();
        while (!reader.EndOfStream)
        {
            designs.Add(reader.ReadLine()!);
        }
        return new Problem(patterns, designs.ToArray());
    }
}
