public class Solution
{
    private void Backtrack(int[] candidates, int target, int startIndex, int currentSum, List<int> currentCombination, IList<IList<int>> result)
    {
        if (currentSum == target)
        {
            result.Add(new List<int>(currentCombination));
            return;
        }

        for (int i = startIndex; i < candidates.Length; i++)
        {
            if (i > startIndex && candidates[i] == candidates[i - 1])
            {
                continue;
            }

            int newSum = currentSum + candidates[i];
            if (newSum > target)
            {
                break;
            }

            currentCombination.Add(candidates[i]);
            Backtrack(candidates, target, i + 1, newSum, currentCombination, result);
            currentCombination.RemoveAt(currentCombination.Count - 1);
        }
    }

    public IList<IList<int>> CombinationSum2(int[] candidates, int target)
    {
        Array.Sort(candidates);
        var result = new List<IList<int>>();
        Backtrack(candidates, target, 0, 0, new List<int>(), result);
        return result;
    }
}
