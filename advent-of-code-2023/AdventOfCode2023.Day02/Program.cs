internal class Program
{
    private static void Main()
    {
        var gamesData = GetGamesData();
        Part01(gamesData);
        Part02(gamesData);
    }

    private static (int gameId, string[] rounds)[] GetGamesData()
    {
        var lines = File.ReadAllLines("input_01.txt");
        return lines.Select(line =>
        {
            var parts = line.Split(':');
            var gameId = int.Parse(parts[0].Replace("Game ", ""));
            var rounds = parts[1].Split(';');
            return (gameId, rounds);
        }).ToArray();
    }

    private static (int redCount, int greenCount, int blueCount) ParseRound(string round)
    {
        int redCount = 0, greenCount = 0, blueCount = 0;
        var cubes = round.Split(',');
        foreach (var cube in cubes)
        {
            var cubeParts = cube.Trim().Split(' ');
            var count = int.Parse(cubeParts[0]);
            var color = cubeParts[1];
            switch (color)
            {
                case "red":
                    redCount = Math.Max(redCount, count);
                    break;
                case "green":
                    greenCount = Math.Max(greenCount, count);
                    break;
                case "blue":
                    blueCount = Math.Max(blueCount, count);
                    break;
            }
        }

        return (redCount, greenCount, blueCount);
    }

    private static void Part01((int gameId, string[] rounds)[] gamesData)
    {
        var sumOfIds = 0;
        foreach (var (gameId, rounds) in gamesData)
        {
            var isPossible = true;
            foreach (var round in rounds)
            {
                var (redCount, greenCount, blueCount) = ParseRound(round);
                if (redCount > 12 || greenCount > 13 || blueCount > 14)
                {
                    isPossible = false;
                    break;
                }
            }

            if (isPossible) sumOfIds += gameId;
        }

        Console.WriteLine(sumOfIds);
    }

    private static void Part02((int gameId, string[] rounds)[] gamesData)
    {
        var sumOfPowers = 0;
        foreach (var (gameId, rounds) in gamesData)
        {
            int minRed = 0, minGreen = 0, minBlue = 0;
            foreach (var round in rounds)
            {
                var (redCount, greenCount, blueCount) = ParseRound(round);
                minRed = Math.Max(minRed, redCount);
                minGreen = Math.Max(minGreen, greenCount);
                minBlue = Math.Max(minBlue, blueCount);
            }

            var power = minRed * minGreen * minBlue;
            sumOfPowers += power;
        }

        Console.WriteLine(sumOfPowers);
    }
}