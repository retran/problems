public class Solution
{
    public class Trie
    {
        public class TrieNode
        {
            public Dictionary<int, TrieNode> Children { get; }
            public int Count { get; set; }

            public TrieNode()
            {
                Children = new Dictionary<int, TrieNode>();
                Count = 0;
            }
        }

        private TrieNode root;

        public Trie()
        {
            root = new TrieNode();
        }

        public void Insert(int num, int bitLength)
        {
            TrieNode node = root;
            for (int i = bitLength; i >= 0; i--)
            {
                int bit = (num >> i) & 1;
                if (!node.Children.ContainsKey(bit))
                {
                    node.Children[bit] = new TrieNode();
                }
                node = node.Children[bit];
                node.Count++;
            }
        }

        public int CountLessThan(int num, int k, int bitLength)
        {
            TrieNode? node = root;
            int count = 0;
            for (int i = bitLength; i >= 0; i--)
            {
                if (node == null)
                    break;

                int bitNum = (num >> i) & 1;
                int bitK = (k >> i) & 1;

                if (bitK == 1)
                {
                    if (node.Children.ContainsKey(bitNum))
                        count += node.Children[bitNum].Count;

                    node = node.Children.ContainsKey(1 - bitNum) ? node.Children[1 - bitNum] : null;
                }
                else
                {
                    node = node.Children.ContainsKey(bitNum) ? node.Children[bitNum] : null;
                }
            }
            return count;
        }
    }

    private int CountPairsWithXOR(int[] nums, int k)
    {
        int maxVal = k;
        foreach (int num in nums)
        {
            if (num > maxVal)
                maxVal = num;
        }
        int bitLength = maxVal == 0 ? 0 : (int)Math.Floor(Math.Log(maxVal, 2)) + 1;

        Trie trie = new Trie();
        int count = 0;
        foreach (int num in nums)
        {
            count += trie.CountLessThan(num, k, bitLength);
            trie.Insert(num, bitLength);
        }
        return count;
    }

    public int CountPairs(int[] nums, int low, int high)
    {
        return CountPairsWithXOR(nums, high + 1) - CountPairsWithXOR(nums, low);
    }
}
