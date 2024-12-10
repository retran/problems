internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            ProcessInputAndWriteOutput("input_01.txt", "output_01_01.txt", false);
            ProcessInputAndWriteOutput("input_02.txt", "output_01_02.txt", false);
            ProcessInputAndWriteOutput("input_01.txt", "output_02_01.txt", true);
            ProcessInputAndWriteOutput("input_02.txt", "output_02_02.txt", true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void ProcessInputAndWriteOutput(string inputFilePath, string outputFilePath, bool countRating)
    {
        if (string.IsNullOrWhiteSpace(inputFilePath))
            throw new ArgumentException();
        if (string.IsNullOrWhiteSpace(outputFilePath))
            throw new ArgumentException();

        int[,] heightmap = ReadHeightmap(inputFilePath);
        long totalScore = 0;

        int rows = heightmap.GetLength(0);
        int cols = heightmap.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (heightmap[i, j] == 0)
                    totalScore += countRating ?
                        CountDistinctTrails(heightmap, i, j) :
                        CountReachablePeaksFrom(heightmap, i, j);
            }
        }

        File.WriteAllText(outputFilePath, totalScore.ToString());
    }

    private static int CountReachablePeaksFrom(int[,] heightmap, int startRow, int startCol)
    {
        int rows = heightmap.GetLength(0);
        int cols = heightmap.GetLength(1);
        var peaks = new HashSet<(int, int)>();
        var queue = new Queue<(int, int)>();
        queue.Enqueue((startRow, startCol));
        var directions = new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();
            int current = heightmap[x, y];
            if (current == 9) peaks.Add((x, y));
            foreach (var (dx, dy) in directions)
            {
                int newX = x + dx;
                int newY = y + dy;
                if (newX >= 0 && newX < rows && newY >= 0 && newY < cols && heightmap[newX, newY] == current + 1)
                    queue.Enqueue((newX, newY));
            }
        }

        return peaks.Count;
    }

    private static long CountDistinctTrails(int[,] heightmap, int startRow, int startCol)
    {
        int rows = heightmap.GetLength(0);
        int cols = heightmap.GetLength(1);
        var memo = new long[rows, cols];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                memo[i, j] = -1;
        return CountPaths(heightmap, startRow, startCol, memo);
    }

    private static long CountPaths(int[,] heightmap, int x, int y, long[,] memo)
    {
        if (memo[x, y] != -1) return memo[x, y];
        int rows = heightmap.GetLength(0);
        int cols = heightmap.GetLength(1);
        int current = heightmap[x, y];
        if (current == 9) return memo[x, y] = 1;
        long ways = 0;
        var directions = new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
        foreach (var (dx, dy) in directions)
        {
            int nx = x + dx;
            int ny = y + dy;
            if (nx >= 0 && nx < rows && ny >= 0 && ny < cols && heightmap[nx, ny] == current + 1)
                ways += CountPaths(heightmap, nx, ny, memo);
        }
        return memo[x, y] = ways;
    }

    private static int[,] ReadHeightmap(string inputFilePath)
    {
        if (!File.Exists(inputFilePath))
            throw new FileNotFoundException();

        string[] lines = File.ReadAllLines(inputFilePath);
        if (lines.Length < 1)
            throw new InvalidDataException();

        int rows = lines.Length;
        int cols = lines[0].Length;
        int[,] heightmap = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            string line = lines[i].Trim();
            if (line.Length != cols)
                throw new InvalidDataException();

            for (int j = 0; j < cols; j++)
            {
                char c = line[j];
                if (!char.IsDigit(c))
                    throw new InvalidDataException();
                heightmap[i, j] = c - '0';
            }
        }

        return heightmap;
    }
}
