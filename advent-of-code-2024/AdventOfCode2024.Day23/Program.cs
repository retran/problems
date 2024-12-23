internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var problems = new[]
            {
                ("input_01.txt", "output_01_01.txt", "output_02_01.txt"),
                ("input_02.txt", "output_01_02.txt", "output_02_02.txt")
            };

            foreach (var (inputFile, outputFileForPart1, outputFileForPart2) in problems)
            {
                ProcessFiles(inputFile, outputFileForPart1, outputFileForPart2);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    private static void ProcessFiles(string inputFile, string outputFileForPart1, string outputFileForPart2)
    {
        if (string.IsNullOrWhiteSpace(inputFile)
            || string.IsNullOrWhiteSpace(outputFileForPart1)
            || string.IsNullOrWhiteSpace(outputFileForPart2))
        {
            throw new ArgumentException("File paths cannot be null or whitespace.");
        }

        var graph = ParseInput(inputFile);

        var threeComputerSets = FindThreeComputerDirectlyConnectedSets(graph);
        File.WriteAllText(outputFileForPart1, 
            threeComputerSets.Count(set => 
                set.Any(c => c.StartsWith("t")))
            .ToString());

        var largestDirectlyConnectedSet = FindLargestDirectlyConnectedSet(graph);
        File.WriteAllText(outputFileForPart2, 
            string.Join(",", largestDirectlyConnectedSet.Order())
            .ToString());
    }

    private static IDictionary<string, ISet<string>> ParseInput(string filePath)
    {
        var graph = new Dictionary<string, ISet<string>>();

        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split('-');
            if (parts.Length != 2)
            {
                throw new FormatException($"Invalid input format: {line}");
            }

            AddConnection(graph, parts[0], parts[1]);
            AddConnection(graph, parts[1], parts[0]);
        }

        return graph;
    }

    private static void AddConnection(IDictionary<string, ISet<string>> graph, string from, string to)
    {
        if (!graph.TryGetValue(from, out var connections))
        {
            connections = new HashSet<string>();
            graph[from] = connections;
        }
        connections.Add(to);
    }

    private static ISet<string> FindLargestDirectlyConnectedSet(IDictionary<string, ISet<string>> graph)
    {
        var computers = graph.Keys.ToArray();
        ISet<string> largestSet = new HashSet<string>();
        foreach (var computer in computers)
        {
            var connectedSet = FindLargestDirectlyConnectedSet(graph, computer);
            if (connectedSet.Count > largestSet.Count)
            {
                largestSet = connectedSet;
            }
        }
        return largestSet;
    }

    private static ISet<string> FindLargestDirectlyConnectedSet(IDictionary<string, ISet<string>> graph, string computer)
    {
        var elements = new HashSet<string>(graph[computer]) { computer }.ToArray();
        for (int size = elements.Length; size >= 2; size--)
        {
            foreach (var subset in GenerateCombinations(elements, size))
            {
                if (IsConnected(graph, subset))
                {
                    return subset;
                }
            }
        }

        return new HashSet<string>();
    }

    private static List<HashSet<string>> FindThreeComputerDirectlyConnectedSets(IDictionary<string, ISet<string>> graph)
    {
        var computers = graph.Keys.ToArray();
        var sets = new List<HashSet<string>>();
        for (int i = 0; i < computers.Length; i++)
        {
            for (int j = i + 1; j < computers.Length; j++)
            {
                for (int k = j + 1; k < computers.Length; k++)
                {
                    var subset = new HashSet<string> { computers[i], computers[j], computers[k] };
                    if (IsConnected(graph, subset))
                    {
                        sets.Add(subset);
                    }
                }
            }
        }
        return sets;
    }

    private static List<HashSet<string>> GenerateCombinations(string[] elements, int size)
    {
        var combinations = new List<HashSet<string>>();
        int[] indices = Enumerable.Range(0, size).ToArray();
        while (true)
        {
            combinations.Add(new HashSet<string>(indices.Select(idx => elements[idx])));
            int position = size - 1;
            while (position >= 0 && indices[position] == elements.Length - size + position)
            {
                position--;
            }
            if (position < 0)
            {
                break;
            }
            indices[position]++;
            for (int i = position + 1; i < size; i++)
            {
                indices[i] = indices[i - 1] + 1;
            }
        }
        return combinations;
    }

    private static bool IsConnected(IDictionary<string, ISet<string>> graph, IEnumerable<string> computers)
    {
        var nodes = computers.ToArray();
        return nodes.All(node => nodes.All(other => node == other || graph[node].Contains(other)));
    }
}
