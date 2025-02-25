package problem0692

class Solution {
    class TrieNode {
        val children = mutableMapOf<Char, TrieNode>()
        var word: String? = null
    }

    class Trie {
        private val root = TrieNode()

        fun insert(word: String) {
            var node = root
            for (char in word) {
                node = node.children.computeIfAbsent(char) { TrieNode() }
            }
            node.word = word
        }

        fun collectWords(node: TrieNode = root, result: MutableList<String>) {
            node.word?.let { result.add(it) }
            for (char in node.children.keys.sorted()) {
                collectWords(node.children[char]!!, result)
            }
        }
    }

    fun topKFrequent(words: Array<String>, k: Int): List<String> {
        val freqMap = mutableMapOf<String, Int>()

        for (word in words) {
            freqMap[word] = freqMap.getOrDefault(word, 0) + 1
        }

        val maxFreq = words.size
        val buckets = Array<Trie>(maxFreq + 1) { Trie() }

        for ((word, freq) in freqMap) {
            buckets[freq].insert(word)
        }

        val result = mutableListOf<String>()
        for (i in maxFreq downTo 1) {
            if (result.size == k) break
            val trie = buckets[i]
            val sortedWords = mutableListOf<String>()
            trie.collectWords(result = sortedWords)
            result.addAll(sortedWords.take(k - result.size))
        }

        return result
    }
}
