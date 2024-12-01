internal class Program
{
    private static void ComputeDistance(string inputFileName, string outputFileName)
    {
        var left = new PriorityQueue<int, int>();
        var right = new PriorityQueue<int, int>();

        long totalDistance = 0;

        using (var sr = new StreamReader(inputFileName))
        {
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var values = line
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(int.Parse)
                    .ToArray();
                left.Enqueue(values[0], values[0]);
                right.Enqueue(values[1], values[1]);
            }
        }

        while (left.Count > 0 && right.Count > 0)
        {
            var l = left.Dequeue();
            var r = right.Dequeue();
            totalDistance += Math.Abs(r - l);
        }

        using (var sw = new StreamWriter(outputFileName))
        {
            sw.WriteLine(totalDistance);
        }
    }

    private static void ComputeSimilarity(string inputFileName, string outputFileName)
    {
        var left = new List<int>();
        var rightFrequencies = new Dictionary<int, int>();

        using (var sr = new StreamReader(inputFileName))
        {
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var values = line
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(int.Parse)
                    .ToArray();
                left.Add(values[0]);

                if (rightFrequencies.ContainsKey(values[1]))
                {
                    rightFrequencies[values[1]]++;
                }
                else
                {
                    rightFrequencies[values[1]] = 1;
                }
            }
        }

        long totalSimilarity = 0;

        foreach (var l in left)
        {
            if (rightFrequencies.ContainsKey(l))
            {
                totalSimilarity += l * rightFrequencies[l];
            }
        }

        using (var sw = new StreamWriter(outputFileName))
        {
            sw.WriteLine(totalSimilarity);
        }
    }

    public static void Main(string[] args)
    {
        ComputeDistance("input_01.txt", "output_01_01.txt");
        ComputeDistance("input_02.txt", "output_01_02.txt");
        ComputeSimilarity("input_01.txt", "output_02_01.txt");
        ComputeSimilarity("input_02.txt", "output_02_02.txt");
    }
}