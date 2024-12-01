internal class Program
{
    private static void Main()
    {
        MapSeedToLocationPart01();
        MapSeedToLocationPart02();
    }

    private static void MapSeedToLocationPart01()
    {
        var almanacLines = File.ReadAllLines("input_01.txt").Where(line => !string.IsNullOrWhiteSpace(line)).ToList();
        var initialSeeds = almanacLines[0].Split(' ').Skip(1).Select(long.Parse).ToList();
        var conversionMaps = GetConversionMaps(almanacLines);
        var locations = initialSeeds.Select(seed =>
        {
            var currentSeed = seed;
            foreach (var conversionMap in conversionMaps)
            {
                var mapNode = conversionMap.Search(currentSeed);
                if (mapNode != null)
                {
                    var offset = currentSeed - mapNode.Value.start;
                    currentSeed = mapNode.Value.value + offset;
                }
            }

            return currentSeed;
        });
        Console.WriteLine(locations.Min());
    }

    private static void MapSeedToLocationPart02()
    {
        var almanacLines = File.ReadAllLines("input_01.txt").Where(line => !string.IsNullOrWhiteSpace(line)).ToList();
        var seedRanges = almanacLines[0].Split(' ').Skip(1).Select(long.Parse).ToList();
        var conversionMaps = GetConversionMaps(almanacLines);
        var ranges = new List<(long start, long end)>();
        for (var j = 0; j < seedRanges.Count; j += 2)
            ranges.Add((seedRanges[j], seedRanges[j] + seedRanges[j + 1] - 1));
        foreach (var map in conversionMaps)
        {
            var newRanges = new List<(long start, long end)>();
            foreach (var (start, end) in ranges)
            {
                var nodes = map.SearchRange(start, end).OrderBy(n => n.start);
                var currentStart = start;
                foreach (var node in nodes)
                {
                    if (node.start > currentStart) newRanges.Add((currentStart, node.start - 1));
                    var newStart = Math.Max(node.start, currentStart);
                    var newEnd = Math.Min(node.end, end);
                    var offset = newStart - node.start;
                    newRanges.Add((node.value + offset, node.value + offset + newEnd - newStart));
                    currentStart = node.end + 1;
                }

                if (currentStart <= end) newRanges.Add((currentStart, end));
            }

            ranges = CombineOverlappingRanges(newRanges);
        }

        var minLocation = ranges.Min(range => range.start);
        Console.WriteLine(minLocation);
    }

    private static List<IntervalList> GetConversionMaps(List<string> almanacLines)
    {
        var conversionMaps = new List<IntervalList>();
        var i = 1;
        while (i < almanacLines.Count)
        {
            if (!almanacLines[i].EndsWith("map:"))
            {
                i++;
                continue;
            }

            var conversionMap = new IntervalList();
            i++;
            while (i < almanacLines.Count && !almanacLines[i].EndsWith("map:"))
            {
                var mapParts = almanacLines[i].Split(' ').Select(long.Parse).ToList();
                conversionMap.Insert(mapParts[1], mapParts[1] + mapParts[2] - 1, mapParts[0]);
                i++;
            }

            conversionMaps.Add(conversionMap);
        }

        return conversionMaps;
    }

    private static List<(long start, long end)> CombineOverlappingRanges(List<(long start, long end)> ranges)
    {
        var sortedRanges = ranges.OrderBy(range => range.start).ToList();
        var combinedRanges = new List<(long start, long end)>();
        var currentStart = sortedRanges[0].start;
        var currentEnd = sortedRanges[0].end;
        for (var i = 1; i < sortedRanges.Count; i++)
            if (sortedRanges[i].start <= currentEnd)
            {
                currentEnd = Math.Max(currentEnd, sortedRanges[i].end);
            }
            else
            {
                combinedRanges.Add((currentStart, currentEnd));
                currentStart = sortedRanges[i].start;
                currentEnd = sortedRanges[i].end;
            }

        combinedRanges.Add((currentStart, currentEnd));
        return combinedRanges;
    }
}

public class IntervalList
{
    private readonly List<(long start, long end, long value)> intervals = new();

    public void Insert(long start, long end, long value)
    {
        intervals.Add((start, end, value));
    }

    public (long start, long end, long value)? Search(long point)
    {
        return intervals.FirstOrDefault(interval => interval.start <= point && interval.end >= point);
    }

    public IEnumerable<(long start, long end, long value)> SearchRange(long start, long end)
    {
        return intervals.Where(interval => interval.start <= end && interval.end >= start);
    }
}