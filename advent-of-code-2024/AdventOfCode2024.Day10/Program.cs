internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            ProcessInputAndWriteOutput("input_01.txt", "output_01_01.txt", "output_02_01.txt");
            ProcessInputAndWriteOutput("input_02.txt", "output_01_02.txt", "output_02_02.txt");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            Console.Error.WriteLine(ex.StackTrace);
        }
    }

    private static readonly (int Row, int Column)[] _directions =
    {
        (-1, 0), (1, 0), (0, -1), (0, 1)
    };

    private static void ProcessInputAndWriteOutput(string inputFilePath, string outputFilePathForPart1, string outputFilePathForPart2)
    {
        if (string.IsNullOrWhiteSpace(inputFilePath) ||
            string.IsNullOrWhiteSpace(outputFilePathForPart1) ||
            string.IsNullOrWhiteSpace(outputFilePathForPart2))
        {
            throw new ArgumentException("Input or output file paths cannot be null or empty.");
        }

        int[,] heightmap = ReadHeightmap(inputFilePath);

        var (reachablePeaks, distinctTrails) = Count(heightmap);

        File.WriteAllText(outputFilePathForPart1, reachablePeaks.ToString());
        File.WriteAllText(outputFilePathForPart2, distinctTrails.ToString());
    }

    private static (int ReachablePeaks, int DistinctTrails) Count(int[,] heightmap)
    {
        int rows = heightmap.GetLength(0);
        int cols = heightmap.GetLength(1);

        int totalReachablePeaks = 0;
        int totalDistinctTrails = 0;

        var peaks = new HashSet<(int Row, int Column)>[rows, cols];
        var queues = new HashSet<(int Row, int Column)>[10];
        var distinctTrails = new int[rows, cols];

        queues[9] = new HashSet<(int Row, int Column)>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (heightmap[i, j] == 9)
                {
                    peaks[i, j] = new HashSet<(int Row, int Column)> { (i, j) };
                    distinctTrails[i, j] = 1;
                    queues[heightmap[i, j]].Add((i, j));
                }
            }
        }

        for (int current = 9; current >= 0; current--)
        {
            foreach (var (row, column) in queues[current])
            {
                foreach (var (rowDiff, columnDiff) in _directions)
                {
                    int newRow = row + rowDiff;
                    int newColumn = column + columnDiff;

                    if (newRow >= 0 && newRow < rows && newColumn >= 0 && newColumn < cols)
                    {
                        if (heightmap[newRow, newColumn] == current - 1)
                        {
                            if (queues[current - 1] == null)
                            {
                                queues[current - 1] = new HashSet<(int Row, int Column)>();
                            }

                            queues[current - 1].Add((newRow, newColumn));
                        }

                        if (heightmap[newRow, newColumn] == current + 1)
                        {
                            if (peaks[row, column] == null)
                            {
                                peaks[row, column] = new HashSet<(int Row, int Column)>();
                            }

                            if (peaks[newRow, newColumn] != null)
                            {
                                peaks[row, column].UnionWith(peaks[newRow, newColumn]);
                            }

                            distinctTrails[row, column] += distinctTrails[newRow, newColumn];
                        }
                    }
                }

                if (current == 0)
                {
                    if (peaks[row, column] != null)
                    {
                        totalReachablePeaks += peaks[row, column].Count;
                    }
                    totalDistinctTrails += distinctTrails[row, column];
                }
            }
        }

        return (totalReachablePeaks, totalDistinctTrails);
    }

    private static int[,] ReadHeightmap(string inputFilePath)
    {
        if (!File.Exists(inputFilePath))
        {
            throw new FileNotFoundException($"File not found: {inputFilePath}");
        }

        string[] lines = File.ReadAllLines(inputFilePath);
        if (lines.Length == 0)
        {
            throw new InvalidDataException("Input file is empty.");
        }

        int rows = lines.Length;
        int cols = lines[0].Length;
        int[,] heightmap = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            string line = lines[i].Trim();
            if (line.Length != cols)
            {
                throw new InvalidDataException("Inconsistent row lengths in input file.");
            }

            for (int j = 0; j < cols; j++)
            {
                if (!char.IsDigit(line[j]))
                {
                    throw new InvalidDataException("Invalid character in heightmap. Only digits are allowed.");
                }
                heightmap[i, j] = line[j] - '0';
            }
        }

        return heightmap;
    }
}
