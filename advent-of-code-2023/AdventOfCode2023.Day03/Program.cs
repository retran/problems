internal class Program
{
    private static readonly string[] SchematicLines =
        File.ReadAllLines("input_01.txt");

    public static void Main()
    {
        Console.WriteLine($"Part 1: {Part01()}");
        Console.WriteLine($"Part 2: {Part02()}");
    }

    public static int Part01()
    {
        var sum = 0;
        var processedNumbers = new HashSet<string>();
        for (var i = 0; i < SchematicLines.Length; i++)
        for (var j = 0; j < SchematicLines[i].Length; j++)
            if (char.IsDigit(SchematicLines[i][j]))
            {
                var start = j;
                var number = GetFullNumber(SchematicLines[i], ref start);
                var positionStr = $"{i},{start}";
                if (!processedNumbers.Contains(positionStr) && IsAdjacentToSymbol(i, j))
                {
                    sum += number;
                    processedNumbers.Add(positionStr);
                }
            }

        return sum;
    }

    public static int Part02()
    {
        var sum = 0;
        var processedNumbers = new HashSet<string>();
        for (var i = 0; i < SchematicLines.Length; i++)
        for (var j = 0; j < SchematicLines[i].Length; j++)
            if (SchematicLines[i][j] == '*')
            {
                var adjacentNumbers = GetAdjacentNumbers(i, j, processedNumbers);
                if (adjacentNumbers.Count == 2) sum += adjacentNumbers[0] * adjacentNumbers[1];
            }

        return sum;
    }

    private static int GetFullNumber(string line, ref int position)
    {
        while (position > 0 && char.IsDigit(line[position - 1])) position--;
        var end = position;
        while (end + 1 < line.Length && char.IsDigit(line[end + 1])) end++;
        return int.Parse(line.Substring(position, end - position + 1));
    }

    private static bool IsAdjacentToSymbol(int i, int j)
    {
        for (var dx = -1; dx <= 1; dx++)
        for (var dy = -1; dy <= 1; dy++)
        {
            var x = i + dx;
            var y = j + dy;
            if (x >= 0 && x < SchematicLines.Length && y >= 0 && y < SchematicLines[i].Length)
            {
                var c = SchematicLines[x][y];
                if (!char.IsDigit(c) && c != '.') return true;
            }
        }

        return false;
    }

    private static List<int> GetAdjacentNumbers(int i, int j, HashSet<string> processedNumbers)
    {
        var adjacentNumbers = new List<int>();
        for (var dx = -1; dx <= 1; dx++)
        for (var dy = -1; dy <= 1; dy++)
        {
            var x = i + dx;
            var y = j + dy;
            if (x >= 0 && x < SchematicLines.Length && y >= 0 && y < SchematicLines[i].Length &&
                char.IsDigit(SchematicLines[x][y]))
            {
                var start = y;
                var number = GetFullNumber(SchematicLines[x], ref start);
                var positionStr = $"{x},{start}";
                if (!processedNumbers.Contains(positionStr))
                {
                    adjacentNumbers.Add(number);
                    processedNumbers.Add(positionStr);
                }
            }
        }

        return adjacentNumbers;
    }
}