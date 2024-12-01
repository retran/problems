internal class Program
{
    private static void Main(string[] args)
    {
        static int CalculatePoints(string[] cards)
        {
            return cards.Select(GetCardPoints).Sum();
        }

        static int GetCardPoints(string card)
        {
            var parts = card.Split('|');
            var winningNumbers = GetWinningNumbers(parts[0]);
            var yourNumbers = GetYourNumbers(parts[1]);
            return CalculateCardPoints(winningNumbers, yourNumbers);
        }

        static HashSet<int> GetWinningNumbers(string part)
        {
            return part.Split(':').Last().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
        }

        static IEnumerable<int> GetYourNumbers(string part)
        {
            return part.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
        }

        static int CalculateCardPoints(HashSet<int> winningNumbers, IEnumerable<int> yourNumbers)
        {
            int cardPoints = 0;
            bool isFirstMatch = true;
            foreach (var number in yourNumbers)
            {
                if (winningNumbers.Contains(number))
                {
                    cardPoints = isFirstMatch ? 1 : cardPoints * 2;
                    isFirstMatch = false;
                }
            }
            return cardPoints;
        }

        static int CalculateTotalCards(string[] cards)
        {
            int[] cardCopies = Enumerable.Repeat(1, cards.Length).ToArray();
            for (int i = 0; i < cards.Length; i++)
            {
                var parts = cards[i].Split('|');
                var winningNumbers = GetWinningNumbers(parts[0]);
                var yourNumbers = GetYourNumbers(parts[1]);
                int matches = yourNumbers.Count(n => winningNumbers.Contains(n));
                for (int j = i + 1; j < i + 1 + matches && j < cards.Length; j++)
                {
                    cardCopies[j] += cardCopies[i];
                }
            }
            return cardCopies.Sum();
        }

        string[] cards = File.ReadAllLines("input_01.txt");

        int points = CalculatePoints(cards);
        Console.WriteLine($"Total points: {points}");

        int totalCards = CalculateTotalCards(cards);
        Console.WriteLine($"Total cards: {totalCards}");
    }
}