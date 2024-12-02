internal class Program
{
    public static void Main(string[] args)
    {
        ComputeDistance("input_01.txt", "output_01_01.txt");
        ComputeDistance("input_02.txt", "output_01_02.txt");
        ComputeSimilarity("input_01.txt", "output_02_01.txt");
        ComputeSimilarity("input_02.txt", "output_02_02.txt");
    }

    private static void ComputeDistance(string inputFileName, string outputFileName)
    {
        var leftQueue = new PriorityQueue<int, int>();
        var rightQueue = new PriorityQueue<int, int>();
        long totalDistance = 0;

        try
        {
            using (var reader = new StreamReader(inputFileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var values = ParseLine(line);
                    leftQueue.Enqueue(values[0], values[0]);
                    rightQueue.Enqueue(values[1], values[1]);
                }
            }

            while (leftQueue.Count > 0 && rightQueue.Count > 0)
            {
                var leftValue = leftQueue.Dequeue();
                var rightValue = rightQueue.Dequeue();
                totalDistance += Math.Abs(rightValue - leftValue);
            }

            WriteOutput(outputFileName, totalDistance);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in ComputeDistance: {ex.Message}");
        }
    }

    private static void ComputeSimilarity(string inputFileName, string outputFileName)
    {
        var leftValues = new List<int>();
        var rightFrequencies = new Dictionary<int, int>();
        long totalSimilarity = 0;

        try
        {
            using (var reader = new StreamReader(inputFileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var values = ParseLine(line);
                    leftValues.Add(values[0]);

                    if (rightFrequencies.ContainsKey(values[1]))
                        rightFrequencies[values[1]]++;
                    else
                        rightFrequencies[values[1]] = 1;
                }
            }

            foreach (var leftValue in leftValues)
            {
                if (rightFrequencies.TryGetValue(leftValue, out int frequency))
                {
                    totalSimilarity += leftValue * frequency;
                }
            }

            WriteOutput(outputFileName, totalSimilarity);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in ComputeSimilarity: {ex.Message}");
        }
    }

    private static int[] ParseLine(string line)
    {
        return line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                   .Select(int.Parse)
                   .ToArray();
    }

    private static void WriteOutput(string outputFileName, long result)
    {
        try
        {
            using (var writer = new StreamWriter(outputFileName))
            {
                writer.WriteLine(result);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to {outputFileName}: {ex.Message}");
        }
    }
}