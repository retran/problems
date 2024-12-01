using System;
using System.Collections.Generic;

public class Solution 
{
    public IList<string> GenerateAbbreviations(string word) 
    {
        List<string> results = new List<string>();
        Backtrack(word, 0, "", 0, results);
        return results;
    }

    private void Backtrack(string word, int index, string current, int count, List<string> results) 
    {
        if (index == word.Length) 
        {
            if (count > 0) {
                current += count;
            }
            results.Add(current);
            return;
        }

        Backtrack(word, index + 1, current, count + 1, results);

        if (count > 0) {
            current += count;
        }

        Backtrack(word, index + 1, current + word[index], 0, results);
    }
}
