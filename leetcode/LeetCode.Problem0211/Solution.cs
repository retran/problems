public class WordDictionary
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

    public WordDictionary()
    {

    }

    public void AddWord(string word)
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
        int index = 0;
        var queue = new Queue<Node>();
        queue.Enqueue(_root);

        while (queue.Count > 0 && index < word.Length) 
        {
            int size = queue.Count;
            for (int i = 0; i < size; i++) 
            {
                var node = queue.Dequeue();
                char c = word[index];
                if (c != '.') 
                {
                    if (node[c] != null) 
                    {
                        if (index == word.Length - 1 && node[c].IsEnd) 
                        {
                            return true;
                        }
                        queue.Enqueue(node[c]);
                    }
                } 
                else 
                {
                    for (char j = 'a'; j <= 'z'; j++) 
                    {
                        if (node[j] != null) 
                        {
                            if (index == word.Length - 1 && node[j].IsEnd) 
                            {
                                return true;
                            }
                            queue.Enqueue(node[j]);
                        }
                    }
                }
            }
            index++;
        }

        return false;
    }
}

/**
 * Your WordDictionary object will be instantiated and called as such:
 * WordDictionary obj = new WordDictionary();
 * obj.AddWord(word);
 * bool param_2 = obj.Search(word);
 */