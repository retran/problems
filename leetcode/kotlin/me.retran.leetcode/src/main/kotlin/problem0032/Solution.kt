package problem0032

import kotlin.math.max

class Solution {
    private val cache = mutableMapOf<Int, Int>()

    fun longestValidParentheses(s: String, index: Int): Int {
        if (index < 0) {
            return 0
        }

        if (cache.containsKey(index)) {
            return cache[index]!!
        }

        var length = 0
        if (index >= 1 && s[index] == ')') {
            val innerLength = longestValidParentheses(s, index - 1)
            val openingBraceIndex = index - innerLength - 1
            if (openingBraceIndex >= 0 && s[openingBraceIndex] == '(') {
                length = innerLength + 2 + longestValidParentheses(s, openingBraceIndex - 1)
            }
        }
        cache[index] = length
        return length
    }

    fun longestValidParentheses(s: String): Int {
        cache.clear()
        var length = 0
        for (i in s.indices) {
            length = max(length, longestValidParentheses(s, i))
        }
        return length
    }
}

// Test cases
fun main() {
    val tests = listOf(
        "" to 0,
        "(" to 0,
        ")" to 0,
        "()" to 2,
        "(()" to 2,
        ")()())" to 4,
        "()(())" to 6,
        "())" to 2,
        "((()))" to 6
    )

    for ((input, expected) in tests) {
        val sol = Solution()
        val result = sol.longestValidParentheses(input)
        println("Input: \"$input\" | Expected: $expected | Got: $result")
    }
}
