internal class Program
{
    public static void Main(string[] args)
    {
        void Part01()
        {
            var lines = File.ReadAllLines("input_01.txt");
            var sum = 0;
            foreach (var line in lines)
            {
                var firstDigit = FindFirstDigit(line);
                var lastDigit = FindLastDigit(line);
                if (!string.IsNullOrEmpty(firstDigit) && !string.IsNullOrEmpty(lastDigit))
                    sum += int.Parse(firstDigit + lastDigit);
            }

            Console.WriteLine("The sum of all calibration values is: " + sum);
        }

        void Part02()
        {
            var spelledOutDigits = new Dictionary<string, int>
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 }
            };
            var lines = File.ReadAllLines("calibration.txt");
            var sum = 0;
            foreach (var line in lines)
            {
                var firstDigit = FindFirstDigitOrSpelledOutDigit(line, spelledOutDigits);
                var lastDigit = FindLastDigitOrSpelledOutDigit(line, spelledOutDigits);
                if (!string.IsNullOrEmpty(firstDigit) && !string.IsNullOrEmpty(lastDigit))
                    sum += int.Parse(firstDigit + lastDigit);
            }

            Console.WriteLine("The sum of all calibration values is: " + sum);
        }

        string FindFirstDigit(string line)
        {
            for (var i = 0; i < line.Length; i++)
                if (char.IsDigit(line[i]))
                    return line[i].ToString();
            return "";
        }

        string FindLastDigit(string line)
        {
            for (var i = line.Length - 1; i >= 0; i--)
                if (char.IsDigit(line[i]))
                    return line[i].ToString();
            return "";
        }

        string FindFirstDigitOrSpelledOutDigit(string line, Dictionary<string, int> spelledOutDigits)
        {
            for (var i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                    return line[i].ToString();
                foreach (var spelledOutDigit in spelledOutDigits)
                    if (i + spelledOutDigit.Key.Length <= line.Length &&
                        line.Substring(i, spelledOutDigit.Key.Length) == spelledOutDigit.Key)
                        return spelledOutDigit.Value.ToString();
            }

            return "";
        }

        string FindLastDigitOrSpelledOutDigit(string line, Dictionary<string, int> spelledOutDigits)
        {
            for (var i = line.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(line[i]))
                    return line[i].ToString();
                foreach (var spelledOutDigit in spelledOutDigits)
                    if (i - spelledOutDigit.Key.Length + 1 >= 0 &&
                        line.Substring(i - spelledOutDigit.Key.Length + 1, spelledOutDigit.Key.Length) ==
                        spelledOutDigit.Key)
                        return spelledOutDigit.Value.ToString();
            }

            return "";
        }

        Part01();
        Part02();
    }
}