// https://leetcode.com/problems/text-justification

using System.Text;

public class Solution {
    public string JustifyLine(IList<string> words, int currentLineLength, int maxWidth, bool lastLine) {
        int additionalSpaces = maxWidth - currentLineLength;

        int spacesBetweenWords = 0;
        int biggerSpacesCount = 0;

        bool needToJustify = !lastLine && additionalSpaces != 0 && words.Count != 1;

        if (needToJustify) {
            spacesBetweenWords = additionalSpaces / (words.Count - 1);
            biggerSpacesCount = additionalSpaces % (words.Count - 1);
        }

        StringBuilder justifiedLine = new();
        for (int i = 0; i < words.Count; i++) {
            justifiedLine.Append(words[i]);
            if (i < words.Count - 1) {
                justifiedLine.Append(' ');
                if (needToJustify) {
                    justifiedLine.Append(' ', spacesBetweenWords);
                    if (i < biggerSpacesCount) {
                        justifiedLine.Append(' ');
                    }
                }
            }
        }

        if (justifiedLine.Length < maxWidth) {
            justifiedLine.Append(' ', maxWidth - justifiedLine.Length);
        }

        return justifiedLine.ToString();
    }

    public IList<string> FullJustify(string[] words, int maxWidth) {
        List<string> lines = new();
        List<string> wordsInLine = new();
        int currentLineLength = 0;

        for (int i = 0; i < words.Length; i++) {
            if (currentLineLength + words[i].Length <= maxWidth) {
                currentLineLength += words[i].Length + 1;
                wordsInLine.Add(words[i]);
                continue;
            }
            
            currentLineLength -= 1; // remove last space;

            lines.Add(JustifyLine(wordsInLine, currentLineLength, maxWidth, false));
            
            wordsInLine.Clear();
            wordsInLine.Add(words[i]);
            currentLineLength = words[i].Length + 1;
        }

        lines.Add(JustifyLine(wordsInLine, currentLineLength, maxWidth, true));

        return lines;
    }
}