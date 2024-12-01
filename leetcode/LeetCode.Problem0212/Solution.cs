public class Solution
{
    public class Trie
    {
        public class Node
        {
            public string Value { get; set; }
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

            public Node(string value = "", bool isEnd = false)
            {
                Value = value;
                IsEnd = isEnd;
                _children = new Node[26];
            }
        }

        public readonly Node Root = new Node("");

        public void Insert(string word)
        {
            var current = Root;
            for (int i = 0; i < word.Length; i++)
            {
                char c = word[i];
                if (current[c] == null)
                {
                    current[c] = new Node();
                }
                current = current[c];

                if (i == word.Length - 1)
                {
                    current.Value = word;
                    current.IsEnd = true;
                }
            }
        }
    }

    private readonly int[] _dx = [0, 0, -1, 1];
    private readonly int[] _dy = [-1, 1, 0, 0];

    public IEnumerable<string> GetWordsFrom(int i, int j, char[][] board, Trie words)
    {
        var queue = new Queue<(int, int, Trie.Node, bool[,])>();

        var visitedRoot = new bool[board.Length, board[0].Length];

        visitedRoot[i, j] = true;

        var root = words.Root[board[i][j]];

        if (root == null)
        {
            yield break;
        }

        queue.Enqueue((i, j, root, visitedRoot));

        while (queue.Count > 0)
        {
            var (x, y, node, visited) = queue.Dequeue();

            if (node.IsEnd)
            {
                yield return node.Value;
            }

            for (int k = 0; k < 4; k++)
            {
                int newX = x + _dx[k];
                int newY = y + _dy[k];

                if (newX >= 0 && newX < board.Length && newY >= 0 && newY < board[0].Length && !visited[newX, newY])
                {
                    var newNode = node[board[newX][newY]];
                    if (newNode != null)
                    {
                        var newVisited = visited.Clone() as bool[,];
                        newVisited[newX, newY] = true;
                        
                        queue.Enqueue((newX, newY, newNode, newVisited));
                    }
                }
            }
        }
    }

    public IList<string> FindWords(char[][] board, string[] words)
    {
        var wordDictionary = new Trie();

        foreach (var word in words)
        {
            wordDictionary.Insert(word);
        }

        var result = new HashSet<string>();

        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[0].Length; j++)
            {
                foreach (var word in GetWordsFrom(i, j, board, wordDictionary))
                {
                    result.Add(word);
                }
            }
        }

        return result.ToList();
    }
}