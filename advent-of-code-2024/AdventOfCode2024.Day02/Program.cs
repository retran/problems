internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Solve("input_01.txt", "output_01_01.txt", IsReportSafeWithoutAdjustment);
            Solve("input_01.txt", "output_02_01.txt", IsReportSafeWithOneAdjustment);
            Solve("input_02.txt", "output_01_02.txt", IsReportSafeWithoutAdjustment);
            Solve("input_02.txt", "output_02_02.txt", IsReportSafeWithOneAdjustment);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred in Main: {ex.Message}");
        }
    }

    private static bool IsReportSafe(int[] report, int excludedIndex = -1)
    {
        if (report.Length < 2)
        {
            return true;
        }

        int previousDifference = 0;

        for (int i = 0; i < report.Length - 1; i++)
        {
            if (i == excludedIndex)
            {
                continue;
            }

            int j = i + 1;
            while (j == excludedIndex && j < report.Length)
            {
                j++;
            }

            if (j >= report.Length)
            {
                break;
            }

            int currentDifference = report[j] - report[i];

            if (Math.Abs(currentDifference) < 1 || Math.Abs(currentDifference) > 3)
            {
                return false;
            }

            if (i != 0 && previousDifference * currentDifference < 0)
            {
                return false;
            }

            previousDifference = currentDifference;
        }

        return true;
    }

    private static bool IsReportSafeWithoutAdjustment(int[] report)
    {
        return IsReportSafe(report, -1);
    }

    private static bool IsReportSafeWithOneAdjustment(int[] report)
    {
        if (IsReportSafe(report, -1))
        {
            return true;
        }

        for (int i = 0; i < report.Length; i++)
        {
            if (IsReportSafe(report, i))
            {
                return true;
            }
        }

        return false;
    }

    private static void Solve(string inputFileName, string outputFileName, Func<int[], bool> isReportSafeFunc)
    {
        int safeReportsCount = 0;
        using (var reader = new StreamReader(inputFileName))
        {
            while (!reader.EndOfStream)
            {
                string? line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var report = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                 .Select(int.Parse)
                                 .ToArray();

                if (isReportSafeFunc(report))
                {
                    safeReportsCount++;
                }
            }
        }

        File.WriteAllText(outputFile, safeReportsCount.ToString());
    }
}
