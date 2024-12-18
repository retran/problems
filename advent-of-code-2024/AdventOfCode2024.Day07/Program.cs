internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Solve("input_01.txt", "output_01_01.txt");
            Solve("input_02.txt", "output_01_02.txt");
            Solve("input_01.txt", "output_02_01.txt", allowConcatenation: true);
            Solve("input_02.txt", "output_02_02.txt", allowConcatenation: true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error in Main: {ex.Message}");
        }
    }

    private static void Solve(string inputFile, string outputFile, bool allowConcatenation = false)
    {
        var equations = ReadEquations(inputFile);
        long total = 0;
        var cache = new Dictionary<(long[], bool), bool>(new SequenceAndBoolEqualityComparer());

        foreach (var (target, values) in equations)
        {
            if (IsEquationSolvable(target, values, allowConcatenation, cache))
            {
                total += target;
            }
        }

        Files.WriteAllText(outputFile, total.ToString());
    }

    private static bool IsEquationSolvable(long targetResult, long[] values, bool allowConcatenation, Dictionary<(long[], bool), bool> cache)
    {
        if (values.Length == 1)
        {
            return values[0] == targetResult;
        }

        var key = (values, allowConcatenation);
        if (cache.TryGetValue(key, out bool cachedResult))
        {
            return cachedResult;
        }

        var first = values[0];
        var second = values[1];

        if (TryNextStep(targetResult, values, first + second, allowConcatenation, cache))
        {
            cache[key] = true;
            return true;
        }

        if (TryNextStep(targetResult, values, first * second, allowConcatenation, cache))
        {
            cache[key] = true;
            return true;
        }

        if (allowConcatenation)
        {
            if (TryConcatenateAndCheck(targetResult, values, first, second, allowConcatenation, cache))
            {
                cache[key] = true;
                return true;
            }
        }

        cache[key] = false;
        return false;
    }

    private static bool TryNextStep(long target, long[] values, long combinedValue, bool allowConcatenation, Dictionary<(long[], bool), bool> cache)
    {
        var newValues = new long[values.Length - 1];
        newValues[0] = combinedValue;
        Array.Copy(values, 2, newValues, 1, values.Length - 2);

        return IsEquationSolvable(target, newValues, allowConcatenation, cache);
    }

    private static bool TryConcatenateAndCheck(long target, long[] values, long first, long second, bool allowConcatenation, Dictionary<(long[], bool), bool> cache)
    {
        string concatenatedStr = first.ToString() + second.ToString();
        if (long.TryParse(concatenatedStr, out long concatenatedValue))
        {
            return TryNextStep(target, values, concatenatedValue, allowConcatenation, cache);
        }

        return false;
    }

    private static (long Target, long[] Values)[] ReadEquations(string inputFilePath)
    {
        var lines = new List<(long, long[])>();

        if (!File.Exists(inputFilePath))
        {
            throw new FileNotFoundException($"Input file not found: {inputFilePath}");
        }

        using var reader = new StreamReader(inputFilePath);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(line)) continue;

            var parts = line.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length != 2)
            {
                throw new FormatException($"Invalid line format in {inputFilePath}. Expected 'result: values', got '{line}'.");
            }

            if (!long.TryParse(parts[0], out long targetResult))
            {
                throw new FormatException($"Invalid target result '{parts[0]}' in line '{line}'.");
            }

            var valueParts = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (valueParts.Length == 0)
            {
                throw new FormatException($"No values provided in line '{line}'.");
            }

            long[] values;
            try
            {
                values = valueParts.Select(val =>
                {
                    if (!long.TryParse(val, out long parsed))
                    {
                        throw new FormatException($"Invalid number '{val}' in line '{line}'.");
                    }
                    return parsed;
                }).ToArray();
            }
            catch (Exception e)
            {
                throw new FormatException($"Error parsing values in line '{line}': {e.Message}", e);
            }

            lines.Add((targetResult, values));
        }

        return lines.ToArray();
    }

    private class SequenceAndBoolEqualityComparer : IEqualityComparer<(long[] Values, bool Flag)>
    {
        public bool Equals((long[] Values, bool Flag) x, (long[] Values, bool Flag) y)
        {
            if (x.Flag != y.Flag) return false;
            if (x.Values.Length != y.Values.Length) return false;
            for (int i = 0; i < x.Values.Length; i++)
            {
                if (x.Values[i] != y.Values[i]) return false;
            }
            return true;
        }

        public int GetHashCode((long[] Values, bool Flag) obj)
        {
            unchecked
            {
                int hash = obj.Flag.GetHashCode();
                foreach (var val in obj.Values)
                {
                    hash = (hash * 31) ^ val.GetHashCode();
                }
                return hash;
            }
        }
    }
}
