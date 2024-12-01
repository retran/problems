public class Solution
{
    private readonly ISet<int> initialSet = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    public void BackTrack(int k, int n, HashSet<int> available, IList<IList<int>> answer, int last)
    {
        if (k == 0)
        {
            if (n == 0)
            {
                var set = initialSet.Except(available).ToList();
                answer.Add(set);
            }
            return;
        }

        int[] availableNumbers = available.ToArray();

        for (int i = 0; i < availableNumbers.Length; i++)
        {
            if (availableNumbers[i] > last)
            {
                available.Remove(availableNumbers[i]);
                BackTrack(k - 1, n - availableNumbers[i], available, answer, availableNumbers[i]);
                available.Add(availableNumbers[i]);
            }
        }
    }

    public IList<IList<int>> CombinationSum3(int k, int n)
    {
        var answer = new List<IList<int>>();
        var available = new HashSet<int>(initialSet);
        BackTrack(k, n, available, answer, 0);
        return answer;
    }
}