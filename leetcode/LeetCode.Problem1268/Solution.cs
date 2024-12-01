// https://leetcode.com/problems/search-suggestions-system

public class Solution
{
    private class Node
    {
        public Node[] children = new Node[26];

        public string Value { get; set; }

        public bool IsWord { get; set; }

        public Node Add(char c, string value, bool isWord)
        {
            if (children[c - 'a'] == null)
            {
                children[c - 'a'] = new Node();
            }

            children[c - 'a'].IsWord |= isWord;
            if (isWord)
            {
                children[c - 'a'].Value = value;
            }

            return children[c - 'a'];
        }

        public Node Get(char c)
        {
            return children[c - 'a'];
        }
    }

    private void CollectWords(Node root, int n, IList<string> answer)
    {
        if (answer.Count() == n)
        {
            return;
        }

        if (root.IsWord)
        {
            answer.Add(root.Value);
        }

        for (int i = 0; i < 26; i++)
        {
            var next = root.Get((char)('a' + i));
            if (next != null)
            {
                CollectWords(next, n, answer);
            }
        }
    }


    private void AddToTrie(Node root, string word)
    {
        Node current = root;
        for (int i = 0; i < word.Length - 1; i++)
        {
            current = current.Add(word[i], string.Empty, false);
        }

        current.Add(word[word.Length - 1], word, true);
    }

    private IList<string> GetFromTrie(Node root, string pattern)
    {
        var current = root;
        for (int i = 0; i < pattern.Length; i++)
        {
            current = current.Get(pattern[i]);
            if (current == null)
            {
                return Enumerable.Empty<string>().ToList();
            }
        }

        var answer = new List<string>();
        CollectWords(current, 3, answer);
        return answer;
    }


    public IList<IList<string>> SuggestedProducts(string[] products, string searchWord)
    {
        var answer = new List<IList<string>>();
        Node root = new Node();
        for (int i = 0; i < products.Length; i++)
        {
            AddToTrie(root, products[i]);
        }

        var sb = new StringBuilder();
        for (int i = 0; i < searchWord.Length; i++)
        {
            sb.Append(searchWord[i]);
            answer.Add(GetFromTrie(root, sb.ToString()));
        }

        return answer;
    }
}