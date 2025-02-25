package problem0126

class Solution {
    fun findLadders(beginWord: String, endWord: String, wordList: List<String>): List<List<String>> {
        val wordSet = wordList.toMutableSet()
        if (endWord !in wordSet) return emptyList()

        val queue = ArrayDeque<String>()
        queue.add(beginWord)

        val parents = mutableMapOf<String, MutableList<String>>()
        val level = mutableMapOf<String, Int>()
        level[beginWord] = 0

        var found = false
        var currentDepth = 0

        while (queue.isNotEmpty() && !found) {
            currentDepth++
            val nextLevelVisited = mutableSetOf<String>()

            for (i in queue.indices) {
                val word = queue.removeFirst()

                for (neighbor in getAdjacentWords(word, wordSet)) {
                    if (neighbor !in level) {
                        level[neighbor] = currentDepth
                        queue.add(neighbor)
                        nextLevelVisited.add(neighbor)
                    }

                    if (level[neighbor] == currentDepth) {
                        parents.computeIfAbsent(neighbor) { mutableListOf() }.add(word)
                    }

                    if (neighbor == endWord) {
                        found = true
                    }
                }
            }

            wordSet.removeAll(nextLevelVisited)
        }

        val result = mutableListOf<List<String>>()
        if (found) {
            val path = ArrayDeque<String>()
            backtrack(endWord, beginWord, parents, path, result)
        }
        return result
    }

    private fun getAdjacentWords(word: String, wordSet: Set<String>): List<String> {
        val adjacent = mutableListOf<String>()
        val wordArray = word.toCharArray()

        for (i in wordArray.indices) {
            val originalChar = wordArray[i]
            for (c in 'a'..'z') {
                if (c == originalChar) continue
                wordArray[i] = c
                val newWord = String(wordArray)
                if (newWord in wordSet) adjacent.add(newWord)
            }
            wordArray[i] = originalChar
        }

        return adjacent
    }

    private fun backtrack(
        word: String,
        beginWord: String,
        parents: Map<String, List<String>>,
        path: ArrayDeque<String>,
        result: MutableList<List<String>>
    ) {
        path.addFirst(word)
        if (word == beginWord) {
            result.add(path.toList())
        } else {
            parents[word]?.forEach { parent ->
                backtrack(parent, beginWord, parents, path, result)
            }
        }
        path.removeFirst()
    }
}
