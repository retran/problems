internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Solve("input_01.txt", "output_01_01.txt", countWithHarmonics: false);
            Solve("input_02.txt", "output_01_02.txt", countWithHarmonics: false);
            Solve("input_01.txt", "output_02_01.txt", countWithHarmonics: true);
            Solve("input_02.txt", "output_02_02.txt", countWithHarmonics: true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error in Main: {ex.Message}");
        }
    }

    private record Problem(int Rows, int Columns, IDictionary<char, IList<(int Row, int Column)>> FrequencyMap)

    private static void Solve(string inputFilePath, string outputFilePath, bool countWithHarmonics)
    {
        var problem = ReadInput(inputFilePath);
        var count = countWithHarmonics
            ? CountAntinodesWithHarmonics(problem.Rows, problem.Columns, problem.FrequencyMap)
            : CountAntinodes((problem.Rows, problem.Columns, problem.FrequencyMap);

        Files.WriteAllText(outputFile, count.ToString());
    }

    private static long CountAntinodes(int rows, int columns, IDictionary<char, IList<(int Row, int Column)>> frequencyMap)
    {
        var antinodes = new HashSet<(int Row, int Column)>();

        foreach (var kvp in frequencyMap)
        {
            var nodes = kvp.Value.ToArray();
            for (int i = 0; i < nodes.Length; i++)
            {
                for (int j = i + 1; j < nodes.Length; j++)
                {
                    var diff = (Row: nodes[i].Row - nodes[j].Row, Column: nodes[i].Column - nodes[j].Column);
                    var antinode1 = (Row: nodes[i].Row + diff.Row, Column: nodes[i].Column + diff.Column);
                    var antinode2 = (Row: nodes[j].Row - diff.Row, Column: nodes[j].Column - diff.Column);

                    if (IsWithinBounds(antinode1, rows, columns))
                        antinodes.Add(antinode1);
                    if (IsWithinBounds(antinode2, rows, columns))
                        antinodes.Add(antinode2);
                }
            }
        }

        return antinodes.Count;
    }

    private static long CountAntinodesWithHarmonics(int rows, int columns, IDictionary<char, IList<(int Row, int Column)>> frequencyMap)
    {
        var antinodes = new HashSet<(int Row, int Column)>();

        foreach (var kvp in frequencyMap)
        {
            var nodes = kvp.Value.ToArray();
            for (int i = 0; i < nodes.Length; i++)
            {
                for (int j = i + 1; j < nodes.Length; j++)
                {
                    var diff = (Row: nodes[i].Row - nodes[j].Row, Column: nodes[i].Column - nodes[j].Column);
                    AddAntinodesAlongLine(antinodes, nodes[i], diff, rows, columns);
                    AddAntinodesAlongLine(antinodes, nodes[j], (-diff.Row, -diff.Column), rows, columns);
                }
            }
        }

        return antinodes.Count;
    }

    private static void AddAntinodesAlongLine(HashSet<(int Row, int Column)> antinodes, (int Row, int Column) start, (int Row, int Column) diff, int rows, int columns)
    {
        int step = 0;
        while (true)
        {
            var current = (Row: start.Row + step * diff.Row, Column: start.Column + step * diff.Column);
            if (!IsWithinBounds(current, rows, columns))
                break;

            antinodes.Add(current);
            step++;
        }
    }

    private static Problem ReadInput(string inputFilePath)
    {
        if (!File.Exists(inputFilePath))
            throw new FileNotFoundException($"Input file not found: {inputFilePath}");

        int rows = 0;
        int columns = 0;
        var frequencyMap = new Dictionary<char, IList<(int Row, int Column)>>();

        using (var reader = new StreamReader(inputFilePath))
        {
            int row = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine() ?? string.Empty;
                columns = line.Length;
                for (int column = 0; column < line.Length; column++)
                {
                    var ch = line[column];
                    if (ch != '.')
                    {
                        if (!frequencyMap.ContainsKey(ch))
                            frequencyMap[ch] = new List<(int Row, int Column)>();
                        frequencyMap[ch].Add((row, column));
                    }
                }
                row++;
            }
            rows = row;
        }

        return (rows, columns, frequencyMap);
    }

    private static bool IsWithinBounds((int Row, int Column) coord, int rows, int columns)
    {
        return coord.Row >= 0 && coord.Row < rows && coord.Column >= 0 && coord.Column < columns;
    }
}