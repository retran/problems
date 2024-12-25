using System.Text.RegularExpressions;

internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var files = new[]
            {
                ("input_01.txt", "output_01_01.txt"),
                ("input_02.txt", "output_01_02.txt")
            };

            foreach (var (inputFile, outputFile) in files)
            {
                Solve(inputFile, outputFile);
            }

        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    private record Problem(IReadOnlyCollection<int[]> Keys, IEnumerable<int[]> Locks);

    private static void Solve(string inputFile, string outputFile)
    {
        if (string.IsNullOrWhiteSpace(inputFile)
            || string.IsNullOrWhiteSpace(outputFile))
        {
            throw new ArgumentException("File paths cannot be null or whitespace.");
        }

        var problem = ParseInput(inputFile);

        int count = 0;
        foreach (var key in problem.Keys)
        {
            foreach (var @lock in problem.Locks)
            {
                if (IsFit(@lock, key))
                {
                    count++;
                }
            }
        }

        File.WriteAllText(outputFile, count.ToString());

    }

    private static bool IsFit(int[] @lock, int[] key)
    {
        for (int i = 0; i < 5; i++)
        {
            if (@lock[i] + key[i] > 5)
            {
                return false;
            }
        }

        return true;
    }

    private static Problem ParseInput(string file)
    {
        using var sr = new StreamReader(file);

        var keys = new List<int[]>();
        var locks = new List<int[]>();

        while (!sr.EndOfStream)
        {
            var lines = new List<string>();
            for (int i = 0; i < 7; i++)
            {
                lines.Add(sr.ReadLine());
            }

            if (lines[0][0] == '.')
            {
                var key = new int[5];
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 1; j < 7; j++)
                    {
                        if (lines[j][i] == '#')
                        {
                            key[i] = 6 - j;
                            break;
                        }
                    }
                }
                keys.Add(key);
            }
            else
            {
                var @lock = new int[5];
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 1; j < 7; j++)
                    {
                        if (lines[j][i] == '.')
                        {
                            @lock[i] = j - 1;
                            break;
                        }
                    }
                }
                locks.Add(@lock);
            }

            sr.ReadLine();
            lines.Clear();
        }

        return new Problem(keys, locks);
    }
}
