public class Solution
{
    public IList<string> LetterCombinations(string digits)
    {
        var digitsMap = new Dictionary<char, char[]>
        {
            {'2', ['a', 'b', 'c']},
            {'3', ['d', 'e', 'f']},
            {'4', ['g', 'h', 'i']},
            {'5', ['j', 'k', 'l']},
            {'6', ['m', 'n', 'o']},
            {'7', ['p', 'q', 'r', 's']},
            {'8', ['t', 'u', 'v']},
            {'9', ['w', 'x', 'y', 'z']}
        };

        var result = new List<string>();

        int index = 0;
        var queue = new Queue<string>();
        queue.Enqueue("");
        while (queue.Count > 0 && index < digits.Length)
        {
            var count = queue.Count;
            char c = digits[index];
            for (int i = 0; i < count; i++)
            {
                var current = queue.Dequeue();
                foreach (var letter in digitsMap[c])
                {
                    if (index + 1 == digits.Length)
                    {
                        result.Add(current + letter);
                    }
                    else
                    {
                        queue.Enqueue(current + letter);
                    }
                }
            }
            index++;
        }
        return result;
    }
}