using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

internal class Program
{
    private const char FreeSpaceChar = '.';
    private const int FreeSpaceId = -1;

    public static void Main(string[] args)
    {
        try
        {
            ProcessInputAndWriteOutput("input_01.txt", "output_01_01.txt", false);
            ProcessInputAndWriteOutput("input_02.txt", "output_01_02.txt", false);
            ProcessInputAndWriteOutput("input_01.txt", "output_02_01.txt", true);
            ProcessInputAndWriteOutput("input_02.txt", "output_02_02.txt", true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error in Main: {ex.Message}");
        }
    }

    private static void ProcessInputAndWriteOutput(string inputFilePath, string outputFilePath, bool useWholeFileCompaction)
    {
        if (string.IsNullOrWhiteSpace(inputFilePath))
            throw new ArgumentException("Input file path cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(outputFilePath))
            throw new ArgumentException("Output file path cannot be null or empty.");

        string diskMap = ReadInput(inputFilePath);
        ValidateDiskMap(diskMap);

        var (unpacked, ids) = UnpackDiskMap(diskMap);

        if (useWholeFileCompaction)
            CompactFilesByWholeFile(unpacked, ids);
        else
            CompactFiles(unpacked, ids);

        long checksum = ComputeChecksum(unpacked, ids);
        WriteResult(outputFilePath, checksum);
    }

    private static string ReadInput(string inputFilePath)
    {
        if (!File.Exists(inputFilePath))
            throw new FileNotFoundException($"Input file not found: {inputFilePath}");

        return File.ReadAllText(inputFilePath).Trim();
    }

    private static void ValidateDiskMap(string diskMap)
    {
        if (string.IsNullOrEmpty(diskMap))
            throw new ArgumentException("Disk map cannot be empty.");

        if (!diskMap.All(char.IsDigit))
            throw new ArgumentException("Disk map must contain only digits.");
    }

    private static (char[] unpackedBlocks, int[] unpackedIds) UnpackDiskMap(string diskMap)
    {
        var blocks = new List<char>();
        var ids = new List<int>();

        int fileId = 0;
        for (int i = 0; i < diskMap.Length; i++)
        {
            char c = diskMap[i];
            if (!int.TryParse(c.ToString(), out int count))
                throw new InvalidDataException($"Invalid disk map character '{c}' at position {i}.");

            bool isFileSegment = i % 2 == 0;
            char blockChar = isFileSegment ? (char)('0' + fileId) : FreeSpaceChar;

            for (int k = 0; k < count; k++)
            {
                blocks.Add(blockChar);
                ids.Add(isFileSegment ? fileId : FreeSpaceId);
            }

            if (isFileSegment)
            {
                fileId++;
            }
        }

        return (blocks.ToArray(), ids.ToArray());
    }

    private static void CompactFiles(char[] unpacked, int[] ids)
    {
        int left = 0;
        int right = unpacked.Length - 1;

        while (left < right)
        {
            while (left < unpacked.Length && unpacked[left] != FreeSpaceChar)
            {
                left++;
            }

            while (right >= 0 && unpacked[right] == FreeSpaceChar)
            {
                right--;
            }

            if (left >= right) break;

            unpacked[left] = unpacked[right];
            ids[left] = ids[right];

            unpacked[right] = FreeSpaceChar;
            ids[right] = FreeSpaceId;
        }
    }

    private static void CompactFilesByWholeFile(char[] unpacked, int[] ids)
    {
        var fileIds = ids.Where(id => id != FreeSpaceId).Distinct().OrderByDescending(id => id).ToArray();
        foreach (var fid in fileIds)
        {
            var filePositions = Enumerable.Range(0, unpacked.Length).Where(i => ids[i] == fid).ToArray();
            if (filePositions.Length == 0) continue;

            int fileStart = filePositions.Min();
            int fileEnd = filePositions.Max();
            int fileLength = filePositions.Length;

            var freeSpans = FindFreeSpaceSpans(unpacked, ids, fileStart);
            var suitableSpan = freeSpans.Where(span => span.Length >= fileLength).OrderBy(span => span.Start).FirstOrDefault();

            if (suitableSpan.Length > 0)
            {
                for (int i = 0; i < fileLength; i++)
                {
                    unpacked[suitableSpan.Start + i] = unpacked[fileStart + i];
                    ids[suitableSpan.Start + i] = ids[fileStart + i];
                }

                for (int i = fileStart; i <= fileEnd; i++)
                {
                    if (ids[i] == fid)
                    {
                        unpacked[i] = FreeSpaceChar;
                        ids[i] = FreeSpaceId;
                    }
                }
            }
        }
    }

    private static List<(int Start, int Length)> FindFreeSpaceSpans(char[] unpacked, int[] ids, int limit)
    {
        var spans = new List<(int Start, int Length)>();
        int start = -1;
        for (int i = 0; i < limit; i++)
        {
            if (ids[i] == FreeSpaceId)
            {
                if (start == -1) start = i;
            }
            else
            {
                if (start != -1)
                {
                    spans.Add((start, i - start));
                    start = -1;
                }
            }
        }

        if (start != -1)
        {
            spans.Add((start, limit - start));
        }

        return spans;
    }

    private static long ComputeChecksum(char[] unpacked, int[] ids)
    {
        long checksum = 0;
        for (int i = 0; i < unpacked.Length; i++)
        {
            if (ids[i] != FreeSpaceId)
            {
                checksum += ids[i] * (long)i;
            }
        }
        return checksum;
    }

    private static void WriteResult(string outputFilePath, long output)
    {
        File.WriteAllText(outputFilePath, output.ToString());
    }
}
