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
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    private static void Solve(string inputFile, string outputFileForPart1, string outputFileForPart2)
    {
        if (string.IsNullOrWhiteSpace(inputFile)
            || string.IsNullOrWhiteSpace(outputFileForPart1)
            || string.IsNullOrWhiteSpace(outputFileForPart2))
        {
            throw new ArgumentException("File paths cannot be null or whitespace.");
        }

        var seeds = File.ReadAllLines(inputFile).Select(long.Parse).ToList();

        long totalSum = 0;
        var keySums = new Dictionary<long, long>();

        foreach (var secretNumber in seeds)
        {
            totalSum += Compute(secretNumber, keySums);
        }

        File.WriteAllText(outputFileForPart1, totalSum.ToString());
        File.WriteAllText(outputFileForPart2, keySums.Values.Max().ToString());
    }

    private static long Compute(long secretNumber, Dictionary<long, long> keySums)
    {
        long previousPrice = secretNumber % 10;
        long nextSecretNumber = secretNumber;
        var sequence = new LinkedList<long>();
        var seenKeys = new HashSet<long>();

        for (int i = 0; i < 2000; i++)
        {
            nextSecretNumber = ComputeNextSecretNumber(nextSecretNumber);
            long price = nextSecretNumber % 10;

            long diff = price - previousPrice;
            sequence.AddLast(diff);

            if (sequence.Count > 4)
            {
                sequence.RemoveFirst();
            }

            if (sequence.Count == 4)
            {
                long key = GetHashCode(sequence);
                if (seenKeys.Add(key))
                {
                    keySums[key] = keySums.TryGetValue(key, out var existingSum)
                        ? existingSum + price
                        : price;
                }
            }

            previousPrice = price;
        }

        return nextSecretNumber;
    }

    private static long GetHashCode(IEnumerable<long> values)
    {
        if (values == null || values.Count() != 4)
        {
            throw new ArgumentException("The input array must contain exactly 4 elements.");
        }

        unchecked
        {
            long hash = 17;
            foreach (var value in values)
            {
                hash = hash * 31 + value;
            }
            return hash;
        }
    }

    private static long ComputeNextSecretNumber(long secretNumber)
    {
        const long mod = 16777216;

        long mul = secretNumber * 64;
        secretNumber ^= mul;
        secretNumber %= mod;

        long div = secretNumber / 32;
        secretNumber ^= div;
        secretNumber %= mod;

        mul = secretNumber * 2048;
        secretNumber ^= mul;
        secretNumber %= mod;

        return secretNumber;
    }
}
