package problem0127

class Solution {
    fun ladderLength(beginWord: String, endWord: String, wordList: List<String>): Int {
        if (beginWord == endWord) {
            return 0
        }

        val wordSet = wordList.toHashSet()
        if (endWord !in wordSet) {
            return 0
        }

        val adjacencyLists = mutableMapOf<String, MutableList<String>>()
        for (word in wordSet.plus(beginWord)) {
            adjacencyLists[word] = mutableListOf()
            val characters = word.toCharArray()

            for (i in word.indices) {
                val originalChar = characters[i]
                for (c in 'a'..'z') {
                    if (c == originalChar) continue

                    characters[i] = c
                    val newWord = characters.concatToString()
                    if (newWord in wordSet) {
                        adjacencyLists[word]!!.add(newWord)
                    }
                }
                characters[i] = originalChar
            }
        }

        val queue = ArrayDeque<String>()
        queue.add(beginWord)

        val visited = mutableSetOf<String>()
        visited.add(beginWord)

        var length = 1
        while (queue.isNotEmpty()) {
            val count = queue.size
            repeat(count) {
                val word = queue.removeFirst()
                if (word == endWord) return length
                for (adjWord in adjacencyLists[word]!!) {
                    if (adjWord !in visited) {
                        visited.add(adjWord)
                        queue.add(adjWord)
                    }
                }
            }
            length++
        }

        return 0
    }
}

fun test() {
    val solution = Solution()

    // Test Case 1: Basic transformation
    println("Test Case 1: " + (
            solution.ladderLength("hit", "cog",
                listOf("hot", "dot", "dog", "lot", "log", "cog")
            ) == 5
            ))

    // Test Case 2: No valid transformation (endWord missing)
    println("Test Case 2: " + (
            solution.ladderLength("hit", "cog",
                listOf("hot", "dot", "dog", "lot", "log")
            ) == 0
            ))

    // Test Case 3: Single-step transformation
    println("Test Case 3: " + (
            solution.ladderLength("hit", "hot",
                listOf("hot")
            ) == 2
            ))

    // Test Case 4: No possible path (disconnected words)
    println("Test Case 4: " + (
            solution.ladderLength("hit", "cog",
                listOf("abc", "def", "ghi", "jkl")
            ) == 0
            ))

    // Test Case 5: Edge Case - One-letter words
    println("Test Case 5: " + (
            solution.ladderLength("a", "c",
                listOf("a", "b", "c")
            ) == 2
            ))

    // Test Case 6: Large input (Performance)
    val largeWordList = mutableListOf<String>()
    for (i in 1..10000) largeWordList.add("word$i")
    largeWordList.add("word9999")
    println("Test Case 6: " + (
            solution.ladderLength("word1", "word9999", largeWordList) >= 0
            ))

    // Test Case 7: beginWord == endWord
    println("Test Case 7: " + (
            solution.ladderLength("same", "same",
                listOf("same", "same", "same")
            ) == 0
            ))

    // Test Case 8: wordList contains duplicates
    println("Test Case 8: " + (
            solution.ladderLength("hit", "cog",
                listOf("hot", "hot", "dot", "dot", "dog", "lot", "log", "cog", "cog")
            ) == 5
            ))

    // Test Case 9: All words are the same
    println("Test Case 9: " + (
            solution.ladderLength("aaa", "aaa",
                listOf("aaa", "aaa", "aaa")
            ) == 0
            ))

    // Test Case 10: wordList contains beginWord
    println("Test Case 10: " + (
            solution.ladderLength("hit", "cog",
                listOf("hit", "hot", "dot", "dog", "lot", "log", "cog")
            ) == 5
            ))

    // Test Case 11: Only one possible path
    println("Test Case 11: " + (
            solution.ladderLength("aaa", "ddd",
                listOf("aab", "abb", "bbb", "bbc", "bcc", "ccc", "ccd", "cdd", "ddd")
            ) == 10
            ))

    // Test Case 12: Multiple shortest paths
    println("Test Case 12: " + (
            solution.ladderLength("hit", "cog",
                listOf("hot", "dot", "dog", "lot", "log", "cog", "mot", "mog")
            ) == 5
            ))
}