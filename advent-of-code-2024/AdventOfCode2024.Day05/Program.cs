internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            ProcessFile("input_01.txt", "output_01_01.txt", countValid: true);
            ProcessFile("input_01.txt", "output_02_01.txt", countValid: false);
            ProcessFile("input_02.txt", "output_01_02.txt", countValid: true);
            ProcessFile("input_02.txt", "output_02_02.txt", countValid: false);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred in Main: {ex.Message}");
        }
    }

    private static void ProcessFile(string inputFileName, string outputFileName, bool countValid)
    {
        try
        {
            int totalSum = 0;
            var (rules, updates) = ReadInput(inputFileName);

            foreach (var update in updates)
            {
                if (IsUpdateValid(rules, update))
                {
                    if (countValid)
                    {
                        totalSum += update[update.Length / 2];
                    }
                }
                else
                {
                    if (!countValid)
                    {
                        var orderedUpdate = TopologicalSort(rules, update);
                        totalSum += orderedUpdate[orderedUpdate.Length / 2];
                    }
                }
            }

            WriteOutput(outputFileName, totalSum);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while processing the file '{inputFileName}': {ex.Message}");
        }
    }

    private static int[] TopologicalSort(IDictionary<int, ISet<int>> rules, int[] update)
    {
        var allowedNodes = update.ToHashSet();

        var graph = new Dictionary<int, HashSet<int>>();
        var inDegree = new Dictionary<int, int>();

        foreach (var item in update)
        {
            var predecessors = rules.TryGetValue(item, out var beforeSet) ? beforeSet : new HashSet<int>();

            foreach (var predecessor in predecessors)
            {
                if (!allowedNodes.Contains(predecessor))
                {
                    continue;
                }

                if (!graph.TryGetValue(predecessor, out var adjacentNodes))
                {
                    adjacentNodes = new HashSet<int>();
                    graph[predecessor] = adjacentNodes;
                }

                adjacentNodes.Add(item);

                if (!inDegree.ContainsKey(item))
                {
                    inDegree[item] = 0;
                }

                if (!inDegree.ContainsKey(predecessor))
                {
                    inDegree[predecessor] = 0;
                }

                inDegree[item]++;
            }
        }

        var queue = new Queue<int>();
        foreach (var node in inDegree.Keys)
        {
            if (inDegree[node] == 0)
            {
                queue.Enqueue(node);
            }
        }

        var sortedList = new List<int>();
        while (queue.Count > 0)
        {
            var currentNode = queue.Dequeue();
            sortedList.Add(currentNode);

            if (graph.TryGetValue(currentNode, out var neighbors))
            {
                foreach (var neighbor in neighbors)
                {
                    inDegree[neighbor]--;
                    if (inDegree[neighbor] == 0)
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        if (sortedList.Count != inDegree.Count)
        {
            throw new InvalidOperationException("Graph has at least one cycle.");
        }

        return sortedList.ToArray();
    }

    private static bool IsUpdateValid(IDictionary<int, ISet<int>> rules, int[] update)
    {
        var requiredBeforeNodes = new HashSet<int>();

        foreach (var item in update)
        {
            if (requiredBeforeNodes.Contains(item))
            {
                return false;
            }

            if (rules.TryGetValue(item, out var predecessors))
            {
                requiredBeforeNodes.UnionWith(predecessors);
            }
        }

        return true;
    }

    private static (IDictionary<int, ISet<int>> Rules, int[][] Updates) ReadInput(string inputFileName)
    {
        using var reader = new StreamReader(inputFileName);
        var rules = ReadOrderingRules(reader);
        var updates = ReadUpdates(reader);
        return (rules, updates);
    }

    private static IDictionary<int, ISet<int>> ReadOrderingRules(StreamReader reader)
    {
        var rules = new Dictionary<int, ISet<int>>();

        string? line;
        while (!string.IsNullOrWhiteSpace(line = reader.ReadLine()))
        {
            var parts = line.Split('|');
            if (parts.Length != 2)
            {
                throw new InvalidDataException($"Invalid data in the input file at line: {line}");
            }

            if (!int.TryParse(parts[0], out int before) || !int.TryParse(parts[1], out int after))
            {
                throw new InvalidDataException($"Invalid integer values in the input file at line: {line}");
            }

            if (!rules.TryGetValue(after, out var set))
            {
                set = new HashSet<int>();
                rules[after] = set;
            }

            set.Add(before);
        }

        return rules;
    }

    private static int[][] ReadUpdates(StreamReader reader)
    {
        var updates = new List<int[]>();
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var parts = line.Split(',');
            var update = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                if (!int.TryParse(parts[i], out int value))
                {
                    throw new InvalidDataException($"Invalid integer value '{parts[i]}' in the input file at line: {line}");
                }
                update[i] = value;
            }
            updates.Add(update);
        }
        return updates.ToArray();
    }

    private static void WriteOutput(string outputFileName, int totalSum)
    {
        using var writer = new StreamWriter(outputFileName);
        writer.WriteLine(totalSum);
    }
}