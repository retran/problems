internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Solve("input_01.txt", "output_01_01.txt", 25);
            Solve("input_02.txt", "output_01_02.txt", 25);
            Solve("input_01.txt", "output_02_01.txt", 75);
            Solve("input_02.txt", "output_02_02.txt", 75);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void Solve(string inputFilePath, string outputFilePath, int blinks)
    {
        if (string.IsNullOrWhiteSpace(inputFilePath) || string.IsNullOrWhiteSpace(outputFilePath))
        {
            throw new ArgumentException("File paths cannot be null or whitespace.");
        }

        var stones = File.ReadAllText(inputFilePath).Split(' ').Select(long.Parse).ToList();
        var totalStoneCount = stones.Sum(stone => CalculateStoneCount(stone, blinks));
        File.WriteAllText(outputFilePath, totalStoneCount.ToString());
    }

    private static readonly Dictionary<(long, int), long> _cache = new();

    private static long CalculateStoneCount(long stone, int blinks)
    {
        if (_cache.TryGetValue((stone, blinks), out var count))
        {
            return count;
        }

        if (blinks == 0)
        {
            count = 1;
        }
        else if (stone == 0)
        {
            count = CalculateStoneCount(1, blinks - 1);
        }
        else if (stone.ToString().Length % 2 == 0)
        {
            var digits = stone.ToString();
            var mid = digits.Length / 2;
            var left = long.Parse(digits[..mid]);
            var right = long.Parse(digits[mid..]);
            count = CalculateStoneCount(left, blinks - 1) + CalculateStoneCount(right, blinks - 1);
        }
        else
        {
            count = CalculateStoneCount(stone * 2024, blinks - 1);
        }

        _cache[(stone, blinks)] = count;
        return count;
    }
}
