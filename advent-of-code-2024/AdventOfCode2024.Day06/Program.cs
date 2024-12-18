using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

internal class Program
{
    private static readonly (int Row, int Column)[] Directions =
    {
        (-1, 0),
        (0, 1),
        (1, 0),
        (0, -1)
    };

    public static void Main(string[] args)
    {
        try
        {
            Solve("input_01.txt", "output_01_01.txt", "output_02_01.txt");
            Solve("input_02.txt", "output_01_02.txt", "output_02_02.txt");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error in Main: {ex.Message}");
        }
    }

    private static void Solve(string inputFile, string outputFileForPart1, string outputFileForPart2)
    {
        var (grid, startPosition) = ReadInput(inputFile);

        var distinctPositions = CountDistinctPositions(grid, startPosition);
        Files.WriteAllText(outputFileForPart1, distinctPositions.ToString());

        var possibleLoops = CountPossibleLoops(grid, startPosition);
        Files.WriteAllText(outputFileForPart2, possibleLoops.ToString());
    }

    private static long CountDistinctPositions(char[,] grid, (int Row, int Column) startPosition)
    {
        var visited = new HashSet<(int Row, int Column)>();
        var currentDirection = Directions[0];
        var position = startPosition;

        while (IsWithinGrid(grid, position))
        {
            visited.Add(position);
            var nextPosition = (Row: position.Row + currentDirection.Row, Column: position.Column + currentDirection.Column);

            if (IsWithinGrid(grid, nextPosition) && grid[nextPosition.Row, nextPosition.Column] == '#')
            {
                currentDirection = TurnRight(currentDirection);
            }
            else
            {
                position = nextPosition;
            }
        }

        return visited.Count;
    }

    private static void CountPossibleLoops(char[,] grid, (int Row, int Column) startPosition)
    {
        var gridCopy = (char[,])originalGrid.Clone();
        int loopsCount = 0;

        for (int i = 0; i < gridCopy.GetLength(0); i++)
        {
            for (int j = 0; j < gridCopy.GetLength(1); j++)
            {
                if (gridCopy[i, j] == '.')
                {
                    gridCopy[i, j] = '#';
                    if (IsLoop(gridCopy, startPosition))
                    {
                        loopsCount++;
                    }
                    gridCopy[i, j] = '.';
                }
            }
        }

        return loopsCount;
    }

    private static bool IsLoop(char[,] grid, (int Row, int Column) start)
    {
        var position = start;
        var currentDirection = Directions[0];
        var visited = new HashSet<((int Row, int Column) Pos, (int Row, int Column) Dir)>();

        while (IsWithinGrid(grid, position))
        {
            if (!visited.Add((position, currentDirection)))
            {
                return true;
            }

            var nextPosition = (Row: position.Row + currentDirection.Row, Column: position.Column + currentDirection.Column);

            if (IsWithinGrid(grid, nextPosition) && grid[nextPosition.Row, nextPosition.Column] == '#')
            {
                currentDirection = TurnRight(currentDirection);
            }
            else
            {
                position = nextPosition;
            }
        }

        return false;
    }

    private static bool IsWithinGrid(char[,] grid, (int Row, int Column) pos)
    {
        return pos.Row >= 0 && pos.Row < grid.GetLength(0) &&
               pos.Column >= 0 && pos.Column < grid.GetLength(1);
    }

    private static (int Row, int Column) TurnRight((int Row, int Column) currentDirection)
    {
        var currentIndex = Array.IndexOf(Directions, currentDirection);
        return Directions[(currentIndex + 1) % Directions.Length];
    }

    private static (char[,] Grid, (int Row, int Column) Start) ReadInput(string inputFileName)
    {
        var lines = File.ReadAllLines(inputFileName);
        var height = lines.Length;
        var width = lines[0].Length;
        var grid = new char[height, width];
        (int Row, int Column) startPosition = (0, 0);

        for (int i = 0; i < height; i++)
        {
            var lineChars = lines[i].ToCharArray();
            for (int j = 0; j < width; j++)
            {
                grid[i, j] = lineChars[j];
                if (grid[i, j] == '^')
                {
                    startPosition = (i, j);
                }
            }
        }

        return (grid, startPosition);
    }
}
