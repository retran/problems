﻿internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            ProcessFile("input_01.txt", "output_01_01.txt", "XMAS");
            ProcessFile("input_01.txt", "output_02_01.txt", "MAS", true);
            ProcessFile("input_02.txt", "output_01_02.txt", "XMAS");
            ProcessFile("input_02.txt", "output_02_02.txt", "MAS", true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred in Main: {ex.Message}");
        }
    }

    private static void ProcessFile(string inputFileName, string outputFileName, string word, bool crossSearch = false)
    {
        try
        {
            var matrix = ReadInput(inputFileName);
            int count = crossSearch ? CountCrossOccurrences(matrix, word) : CountWordOccurrences(matrix, word);
            WriteOutput(outputFileName, count);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while processing the file '{inputFileName}': {ex.Message}");
        }
    }

    private static char[][] ReadInput(string inputFileName)
    {
        var lines = new List<char[]>();

        using (var reader = new StreamReader(inputFileName))
        {
            while (!reader.EndOfStream)
            {
                string? line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    throw new InvalidDataException("Invalid data in the input file.");
                }

                lines.Add(line.ToCharArray());
            }
        }

        return lines.ToArray();
    }

    private static void WriteOutput(string outputFileName, int count)
    {
        using (var writer = new StreamWriter(outputFileName))
        {
            writer.WriteLine(count);
        }
    }

    private static int CountWordOccurrences(char[][] matrix, string word)
    {
        int count = 0;
        (int row, int col)[] directions = new (int row, int col)[]
        {
            (-1, -1), (-1, 0), (-1, 1),
            (0,  -1),          ( 0, 1),
            (1,  -1), (1,  0), ( 1, 1)
        };

        for (int row = 0; row < matrix.Length; row++)
        {
            for (int col = 0; col < matrix[row].Length; col++)
            {
                foreach (var direction in directions)
                {
                    if (SearchInDirection(matrix, word, row, col, direction.row, direction.col))
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    private static int CountCrossOccurrences(char[][] matrix, string word)
    {
        int count = 0;

        for (int row = 0; row < matrix.Length; row++)
        {
            for (int col = 0; col < matrix[row].Length; col++)
            {
                bool found =
                    (SearchInDirection(matrix, word, row, col, 1, 1) 
                        || SearchInDirection(matrix, word, row + word.Length - 1, col + word.Length - 1, -1, -1))
                    && (SearchInDirection(matrix, word, row + word.Length - 1, col, -1, 1)
                        || SearchInDirection(matrix, word, row, col + word.Length - 1, 1, -1));

                if (found)
                {
                    count++;
                }
            }
        }

        return count;
    }

    private static bool SearchInDirection(char[][] matrix, string word, int row, int col, int rowDirection, int colDirection)
    {
        for (int i = 0; i < word.Length; i++)
        {
            if (row < 0 || row >= matrix.Length || col < 0 || col >= matrix[row].Length || matrix[row][col] != word[i])
            {
                return false;
            }

            row += rowDirection;
            col += colDirection;
        }

        return true;
    }
}