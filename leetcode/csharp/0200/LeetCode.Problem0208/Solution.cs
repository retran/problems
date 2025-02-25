public class Trie
{
    private class Node
    {
        public string Value { get; private set; }
        public bool IsEnd { get; set; }
        private Node[] _children;

        public Node this[char c]
        {
            get
            {
                return _children[c - 'a'];
            }
            set
            {
                _children[c - 'a'] = value;
            }
        }

        public Node(string value, bool isEnd = false)
        {
            Value = value;
            IsEnd = isEnd;
            _children = new Node[26];
        }
    }

    private Node _root = new Node("");

    public Trie()
    {

    }

    public void Insert(string word)
    {
        var current = _root;
        for (int i = 0; i < word.Length; i++)
        {
            char c = word[i];
            if (current[c] == null)
            {
                current[c] = new Node(current.Value + c);
            }
            current = current[c];

            if (i == word.Length - 1)
            {
                current.IsEnd = true;
            }
        }
    }

    public bool Search(string word)
    {
        var current = _root;
        foreach (var c in word)
        {
            current = current[c];
            if (current == null)
            {
                return false;
            }
        }
        return current.IsEnd;
    }

    public bool StartsWith(string prefix)
    {
        var current = _root;
        foreach (var c in prefix)
        {
            current = current[c];
            if (current == null)
            {
                return false;
            }
        }
        return true;
    }
}

/**
 * Your Trie object will be instantiated and called as such:
 * Trie obj = new Trie();
 * obj.Insert(word);
 * bool param_2 = obj.Search(word);
 * bool param_3 = obj.StartsWith(prefix);
 */