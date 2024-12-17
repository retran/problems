using System.Text.RegularExpressions;

internal class Program
{
    private record Memory(int[] Program)
    {
        public long RegisterA { get; set; }
        public long RegisterB { get; set; }
        public long RegisterC { get; set; }
    }

    public static void Main(string[] args)
    {
        try
        {
            var part1Files = new[]
            {
                ("input_01.txt", "output_01_01.txt"),
                ("input_02.txt", "output_01_02.txt"),
                ("input_03.txt", "output_01_03.txt")
            };

            foreach (var (inputFile, outputFile) in part1Files)
            {
                SolvePart1(inputFile, outputFile);
            }

            var part2Files = new[]
            {
                ("input_03.txt", "output_02_03.txt"),
                ("input_02.txt", "output_02_02.txt")
            };

            foreach (var (inputFile, outputFile) in part2Files)
            {
                SolvePart2(inputFile, outputFile);
            }

        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void SolvePart1(string inputFilePath, string outputFilePath)
    {
        if (string.IsNullOrWhiteSpace(inputFilePath) || string.IsNullOrWhiteSpace(outputFilePath))
        {
            throw new ArgumentException("File paths cannot be null or whitespace.");
        }

        var memory = ParseInput(inputFilePath);
        var output = ExecuteProgram(memory);

        WriteOutput(outputFilePath, output);
    }

    private static void SolvePart2(string inputFilePath, string outputFilePath)
    {
        if (string.IsNullOrWhiteSpace(inputFilePath) || string.IsNullOrWhiteSpace(outputFilePath))
        {
            throw new ArgumentException("File paths cannot be null or whitespace.");
        }

        var memory = ParseInput(inputFilePath);
        var queue = new Queue<(long Value, int Level)>();

        for (int i = 0; i < 8; i++)
        {
            queue.Enqueue((i, 1));
        }

        while (queue.TryDequeue(out var current))
        {
            var (currentValue, level) = current;
            var programSlice = string.Join(',', memory.Program.Skip(memory.Program.Length - level).Take(level));

            var output = ExecuteProgram(new Memory(memory.Program)
            {
                RegisterA = currentValue,
                RegisterB = memory.RegisterB,
                RegisterC = memory.RegisterC
            });

            if (string.Join(',', output) == programSlice)
            {
                if (output.Length == memory.Program.Length)
                {
                    File.WriteAllText(outputFilePath, currentValue.ToString());
                    return;
                }

                for (int i = 0; i < 8; i++)
                {
                    queue.Enqueue(((currentValue << 3) + i, level + 1));
                }
            }
        }
    }

    private static long[] ExecuteProgram(Memory memory)
    {
        var output = new List<long>();
        int instructionPointer = 0;

        while (instructionPointer < memory.Program.Length)
        {
            int opcode = memory.Program[instructionPointer];
            int operand = memory.Program[instructionPointer + 1];

            switch (opcode)
            {
                case 0: // adv
                    memory.RegisterA /= (long)Math.Pow(2, ComboOperand(memory, operand));
                    break;
                case 1: // bxl
                    memory.RegisterB ^= operand;
                    break;
                case 2: // bst
                    memory.RegisterB = ComboOperand(memory, operand) % 8;
                    break;
                case 3: // jnz
                    instructionPointer = (memory.RegisterA != 0) ? operand : instructionPointer + 2;
                    continue;
                case 4: // bxc
                    memory.RegisterB ^= memory.RegisterC;
                    break;
                case 5: // out
                    output.Add(ComboOperand(memory, operand) % 8);
                    break;
                case 6: // bdv
                    memory.RegisterB = memory.RegisterA / (long)Math.Pow(2, ComboOperand(memory, operand));
                    break;
                case 7: // cdv
                    memory.RegisterC = memory.RegisterA / (long)Math.Pow(2, ComboOperand(memory, operand));
                    break;
                default:
                    throw new InvalidOperationException($"Invalid opcode: {opcode} at instruction {instructionPointer}");
            }

            instructionPointer += 2;
        }

        return output.ToArray();
    }

    private static long ComboOperand(Memory memory, int value) => value switch
    {
        0 => 0,
        1 => 1,
        2 => 2,
        3 => 3,
        4 => memory.RegisterA,
        5 => memory.RegisterB,
        6 => memory.RegisterC,
        _ => throw new ArgumentException("Invalid combo operand.")
    };

    private static Memory ParseInput(string filePath)
    {
        var regex = new Regex(@"Register A: (\d+)\s*\r?\nRegister B: (\d+)\s*\r?\nRegister C: (\d+)\s*\r?\nProgram: ([\d,]+)");
        var input = File.ReadAllText(filePath);

        var match = regex.Match(input);
        if (!match.Success)
        {
            throw new FormatException("Input file is not in the correct format.");
        }

        return new Memory(match.Groups[4].Value.Split(',').Select(int.Parse).ToArray())
        {
            RegisterA = long.Parse(match.Groups[1].Value),
            RegisterB = long.Parse(match.Groups[2].Value),
            RegisterC = long.Parse(match.Groups[3].Value)
        };
    }

    private static void WriteOutput(string outputFilePath, long[] output)
    {
        File.WriteAllText(outputFilePath, string.Join(',', output));
    }
}
