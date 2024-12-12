internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            ProcessInputAndWriteOutput("input_01.txt", "output_01_01.txt", "output_02_01.txt");
            ProcessInputAndWriteOutput("input_02.txt", "output_01_02.txt", "output_02_02.txt");
            ProcessInputAndWriteOutput("input_03.txt", "output_01_03.txt", "output_02_03.txt");
            ProcessInputAndWriteOutput("input_04.txt", "output_01_04.txt", "output_02_04.txt");
            ProcessInputAndWriteOutput("input_05.txt", "output_01_05.txt", "output_02_05.txt");
            ProcessInputAndWriteOutput("input_06.txt", "output_01_06.txt", "output_02_06.txt");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static readonly (int Row, int Column)[] _directions = [(0, 1), (1, 0), (0, -1), (-1, 0)];

    private static void ProcessInputAndWriteOutput(string inputFilePath, string outputFilePathForPart1, string outputFilePathForPart2)
    {
        if (string.IsNullOrWhiteSpace(inputFilePath)
            || string.IsNullOrWhiteSpace(outputFilePathForPart1)
            || string.IsNullOrWhiteSpace(outputFilePathForPart2))
        {
            throw new ArgumentException("File paths cannot be null or whitespace.");
        }

        char[,] garden = ReadInput(inputFilePath);
        var counted = new HashSet<(int Row, int Column)>();

        long totalPriceWithPerimeter = 0;
        long totalPriceWithSides = 0;

        for (var i = 0; i < garden.GetLength(0); i++)
        {
            for (var j = 0; j < garden.GetLength(1); j++)
            {
                if (counted.Contains((i, j)))
                {
                    continue;
                }

                var queue = new Queue<(int Row, int Column)>();
                queue.Enqueue((i, j));

                int currentPlant = garden[i, j];

                var currentRegionSides = new HashSet<(int Row, int Column, int BorderRow, int BorderColumn)>();
                int currentRegionPerimeter = 0;
                int currentRegionSquare = 0;

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();

                    if (counted.Contains(current))
                    {
                        continue;
                    }

                    if (garden[current.Row, current.Column] != currentPlant)
                    {
                        continue;
                    }

                    foreach (var direction in _directions)
                    {
                        var newRow = current.Row + direction.Row;
                        var newColumn = current.Column + direction.Column;

                        if (newRow < 0
                            || newRow >= garden.GetLength(0)
                            || newColumn < 0
                            || newColumn >= garden.GetLength(1)
                            || garden[newRow, newColumn] != currentPlant)
                        {
                            currentRegionSides.Add((current.Row, current.Column, newRow, newColumn));
                            currentRegionPerimeter++;
                            continue;
                        }

                        queue.Enqueue((newRow, newColumn));
                    }

                    currentRegionSquare++;
                    counted.Add(current);
                }

                totalPriceWithPerimeter += currentRegionSquare * currentRegionPerimeter;

                var unionFind = new UnionFind(currentRegionSides);
                var sides = currentRegionSides.ToArray();
                for (int k = 0; k < sides.Count(); k++)
                {
                    for (int l = 0; l < sides.Count(); l++)
                    {
                        if (k == l)
                        {
                            continue;
                        }

                        if (Math.Abs(sides[k].Row - sides[l].Row) + Math.Abs(sides[k].Column - sides[l].Column) == 1
                            && sides[k].Row - sides[k].BorderRow == sides[l].Row - sides[l].BorderRow
                            && sides[k].Column - sides[k].BorderColumn == sides[l].Column - sides[l].BorderColumn)
                        {
                            unionFind.Union(sides[k], sides[l]);
                        }
                    }
                }

                totalPriceWithSides += currentRegionSquare * unionFind.GetCount();
            }
        }

        File.WriteAllText(outputFilePathForPart1, totalPriceWithPerimeter.ToString());
        File.WriteAllText(outputFilePathForPart2, totalPriceWithSides.ToString());
    }

    private static char[,] ReadInput(string inputFilePath)
    {
        var lines = File.ReadAllLines(inputFilePath);

        var map = new char[lines.Length, lines[0].Length];

        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                map[i, j] = lines[i][j];
            }
        }

        return map;
    }

    public class UnionFind
    {
        private readonly Dictionary<(int, int, int, int), (int, int, int, int)> _parent;
        private readonly Dictionary<(int, int, int, int), int> _rank;
        private int _count;

        public UnionFind(IEnumerable<(int Row, int Column, int BorderRow, int BorderColumn)> elements)
        {
            _parent = new Dictionary<(int, int, int, int), (int, int, int, int)>();
            _rank = new Dictionary<(int, int, int, int), int>();
            _count = 0;

            foreach (var element in elements)
            {
                _parent[element] = element;
                _rank[element] = 0;
                _count++;
            }
        }

        public (int, int, int, int) Find((int, int, int, int) x)
        {
            if (!_parent.ContainsKey(x))
                throw new KeyNotFoundException($"Element {x} not found in UnionFind.");

            if (!_parent[x].Equals(x))
            {
                _parent[x] = Find(_parent[x]);
            }

            return _parent[x];
        }

        public void Union((int, int, int, int) x, (int, int, int, int) y)
        {
            var rootX = Find(x);
            var rootY = Find(y);

            if (rootX.Equals(rootY)) return;

            if (_rank[rootX] > _rank[rootY])
            {
                _parent[rootY] = rootX;
            }
            else if (_rank[rootX] < _rank[rootY])
            {
                _parent[rootX] = rootY;
            }
            else
            {
                _parent[rootY] = rootX;
                _rank[rootX]++;
            }

            _count--;
        }

        public int GetCount()
        {
            return _count;
        }
    }
}
