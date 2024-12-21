internal class Program
{
    private static readonly char[,] NumericKeypad =
    {
        { '7', '8', '9' },
        { '4', '5', '6' },
        { '1', '2', '3' },
        { ' ', '0', 'A' }
    };

    private static readonly char[,] DirectionalKeypad =
    {
        { ' ', '^', 'A' },
        { '<', 'v', '>' },
    };

    private static readonly Dictionary<(int, int, char, char), string[]> GeneratedSequenceCache = new();
    private static readonly Dictionary<string, int> SequenceScoreCache = new();
    private static readonly Dictionary<(object, char, char, int), long> SequenceLengthCache = new();
    private static readonly Dictionary<(char, char), string[]> NumericKeypadExpander = BuildExpander(NumericKeypad);
    private static readonly Dictionary<(char, char), string[]> DirectionalKeypadExpander = BuildExpander(DirectionalKeypad);

    public static void Main(string[] args)
    {
        try
        {
            var testFiles = new[]
            {
                ("input_01.txt", "output_01_01.txt", 2),
                ("input_02.txt", "output_01_02.txt", 2),
                ("input_01.txt", "output_02_01.txt", 25),
                ("input_02.txt", "output_02_02.txt", 25)
            };

            foreach (var (inputFile, outputFileName, directionalKeypadsCount) in testFiles)
            {
                Solve(inputFile, outputFileName, directionalKeypadsCount);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void Solve(string inputFile, string outputFile, int directionalKeypadsCount)
    {
        if (string.IsNullOrWhiteSpace(inputFile)
            || string.IsNullOrWhiteSpace(outputFile))
        {
            throw new ArgumentException("File paths cannot be null or whitespace.");
        }

        var codes = File.ReadLines(inputFile);

        long score = 0;
        foreach (var code in codes)
        {
            var expandedSequences = ExpandSequence(NumericKeypadExpander, code);
            var minLength = expandedSequences.Min(sequence =>
                ComputeExpandedSequenceLength(
                    DirectionalKeypadExpander,
                    sequence,
                    directionalKeypadsCount));
            var numericValue = int.Parse(code.Substring(0, 3));
            score += minLength * numericValue;
        }
        File.WriteAllText(outputFile, score.ToString());
    }

    private static long ComputeExpandedSequenceLength(Dictionary<(char, char), string[]> expander, string sequence, int count)
    {
        if (count == 0)
        {
            return sequence.Length;
        }

        char previousButton = 'A';
        long totalLength = 0;
        foreach (var nextButton in sequence)
        {
            if (!SequenceLengthCache.TryGetValue((expander, previousButton, nextButton, count), out var length))
            {
                var key = (previousButton, nextButton);
                if (!expander.TryGetValue(key, out var expandedSequences))
                {
                    throw new InvalidOperationException($"No sequence found for key: {key}");
                }

                length = expandedSequences.Min(sequence => ComputeExpandedSequenceLength(expander, sequence, count - 1));
                SequenceLengthCache[(expander, previousButton, nextButton, count)] = length;
            }
            totalLength += length;
            previousButton = nextButton;
        }

        return totalLength;
    }

    private static Dictionary<(char, char), string[]> BuildExpander(char[,] keypad)
    {
        var expander = new Dictionary<(char, char), string[]>();
        var rows = keypad.GetLength(0);
        var columns = keypad.GetLength(1);

        for (int rowStart = 0; rowStart < rows; rowStart++)
        {
            for (int columnStart = 0; columnStart < columns; columnStart++)
            {
                if (keypad[rowStart, columnStart] == ' ')
                {
                    continue;
                }

                for (int rowEnd = 0; rowEnd < rows; rowEnd++)
                {
                    for (int columnEnd = 0; columnEnd < columns; columnEnd++)
                    {
                        if (keypad[rowEnd, columnEnd] == ' ')
                        {
                            continue;
                        }

                        var startButton = keypad[rowStart, columnStart];
                        var endButton = keypad[rowEnd, columnEnd];

                        var key = (startButton, endButton);
                        if (!expander.ContainsKey(key))
                        {
                            var horizontalDistance = Math.Abs(columnStart - columnEnd);
                            var verticalDistance = Math.Abs(rowStart - rowEnd);

                            var horizontalDirection = columnStart < columnEnd ? '>' : '<';
                            var verticalDirection = rowStart < rowEnd ? 'v' : '^';

                            var sequences = GenerateAllPossibleSequences(
                                horizontalDistance,
                                verticalDistance,
                                horizontalDirection,
                                verticalDirection
                            );

                            sequences = RemoveInvalidSequences(keypad, rowStart, columnStart, sequences);
                            sequences = RemoveNonoptimalSequences(sequences);

                            expander[key] = sequences;
                        }
                    }
                }
            }
        }

        return expander;
    }

    private static string[] Expand(Dictionary<(char, char), string[]> expander, string[] sequences)
    {
        var result = new List<string>();

        foreach (var sequence in sequences)
        {
            result.AddRange(ExpandSequence(expander, sequence));
        }

        return RemoveNonoptimalSequences(result.ToArray());
    }

    private static string[] ExpandSequence(Dictionary<(char, char), string[]> expander, string sequence)
    {
        var result = new List<string>();
        char previousButton = 'A';

        foreach (var nextButton in sequence)
        {
            var key = (previousButton, nextButton);
            if (!expander.TryGetValue(key, out var subSequences))
            {
                continue;
            }

            var newSequences = new List<string>();
            if (result.Count == 0)
            {
                newSequences.AddRange(subSequences);
            }
            else
            {
                foreach (var existingSequence in result)
                {
                    foreach (var subSequence in subSequences)
                    {
                        newSequences.Add(existingSequence + subSequence);
                    }
                }
            }

            result = newSequences;
            previousButton = nextButton;
        }

        return RemoveNonoptimalSequences(result.ToArray());
    }

    private static string[] GenerateAllPossibleSequences(
        int horizontalDistance,
        int verticalDistance,
        char horizontalDirection,
        char verticalDirection)
    {
        if (horizontalDistance == 0 && verticalDistance == 0)
        {
            return ["A"];
        }

        var key = (horizontalDistance, verticalDistance, horizontalDirection, verticalDirection);
        if (GeneratedSequenceCache.TryGetValue(key, out var cachedSequences))
        {
            return cachedSequences;
        }

        var sequences = new List<string>();

        if (horizontalDistance > 0)
        {
            var subSequences = GenerateAllPossibleSequences(horizontalDistance - 1, verticalDistance, horizontalDirection, verticalDirection);
            foreach (var sequence in subSequences)
            {
                sequences.Add(horizontalDirection + sequence);
            }
        }

        if (verticalDistance > 0)
        {
            var subSequences = GenerateAllPossibleSequences(horizontalDistance, verticalDistance - 1, horizontalDirection, verticalDirection);
            foreach (var sequence in subSequences)
            {
                sequences.Add(verticalDirection + sequence);
            }
        }

        var result = sequences.ToArray();
        GeneratedSequenceCache[key] = result;
        return result;
    }

    private static string[] RemoveInvalidSequences(char[,] keypad, int startRow, int startColumn, string[] sequences)
    {
        return sequences.Where(sequence => IsValidSequence(keypad, startRow, startColumn, sequence)).ToArray();
    }

    private static bool IsValidSequence(char[,] keypad, int startRow, int startColumn, string sequence)
    {
        var currentRow = startRow;
        var currentColumn = startColumn;

        foreach (var direction in sequence)
        {
            switch (direction)
            {
                case '^': currentRow--; break;
                case 'v': currentRow++; break;
                case '<': currentColumn--; break;
                case '>': currentColumn++; break;
                case 'A': return true;
            }

            if (currentRow < 0 || currentRow >= keypad.GetLength(0) ||
                currentColumn < 0 || currentColumn >= keypad.GetLength(1))
            {
                return false;
            }

            if (keypad[currentRow, currentColumn] == ' ')
            {
                return false;
            }
        }

        return true;
    }

    private static string[] RemoveNonoptimalSequences(string[] sequences)
    {
        if (sequences.Length == 0)
        {
            return sequences;
        }

        var minScore = sequences.Min(ComputeSequenceScore);
        return sequences.Where(p => ComputeSequenceScore(p) == minScore).ToArray();
    }

    private static int ComputeSequenceScore(string sequence)
    {
        if (SequenceScoreCache.TryGetValue(sequence, out var score))
        {
            return score;
        }

        score = 0;
        var previousButton = 'A';

        foreach (var nextButton in sequence)
        {
            if (previousButton != nextButton)
            {
                score++;
            }
            previousButton = nextButton;
        }

        SequenceScoreCache[sequence] = score;
        return score;
    }
}
