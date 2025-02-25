package problem0212

class Solution {
    private data class Position(val row: Int, val column: Int)

    private data class State(val position: Position, val visited: Set<Position>, val currentNode: TrieNode)

    private data class TrieNode(val character: Char) {
        val children: MutableMap<Char, TrieNode> = mutableMapOf()

        var isWordEnd = false
        var word = ""
    }

    private fun buildTrie(words: Array<String>): TrieNode {
        val root = TrieNode('#')

        for (word in words) {
            var currentNode = root
            for (index in word.indices) {
                if (currentNode.children.containsKey(word[index])) {
                    currentNode = currentNode.children[word[index]]!!
                }
                else {
                    val node = TrieNode(word[index])
                    currentNode.children[word[index]] = node
                    currentNode = node
                }
            }

            currentNode.isWordEnd = true
            currentNode.word = word
        }

        return root
    }

    fun findWords(board: Array<CharArray>, words: Array<String>): List<String> {
        val rows = board.size
        val columns = board[0].size

        val directions = arrayOf(
            Position(-1, 0),
            Position(1, 0),
            Position(0, -1),
            Position(0, 1)
        )

        val root = buildTrie(words)

        val stack: ArrayDeque<State> = ArrayDeque()

        for (i in 0..rows - 1) {
            for (j in 0..columns - 1) {
                val position = Position(i, j)
                val character = board[i][j]
                if (root.children.containsKey(character)) {
                    val state = State(position, setOf(position), root.children[character]!!)
                    stack.addLast(state)
                }
            }
        }

        val answer = mutableSetOf<String>()

        while (stack.isNotEmpty()) {
            val currentState = stack.removeLast()

            if (currentState.currentNode.isWordEnd) {
                answer.add(currentState.currentNode.word)
            }

            for (direction in directions) {
                val nextPosition = Position(
                    currentState.position.row + direction.row,
                    currentState.position.column + direction.column)

                if (nextPosition.row !in 0..rows - 1 || nextPosition.column !in 0..columns - 1) {
                    continue
                }

                if (currentState.visited.contains(nextPosition)) {
                    continue
                }

                val character = board[nextPosition.row][nextPosition.column]
                if (currentState.currentNode.children.containsKey(character)) {
                    val newState = State(
                        nextPosition,
                        currentState.visited.plus(nextPosition),
                        currentState.currentNode.children[character]!!)
                    stack.addLast(newState)
                }
            }
        }
        return answer.toList()
    }
}