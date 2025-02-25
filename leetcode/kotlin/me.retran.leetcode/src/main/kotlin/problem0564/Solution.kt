package problem0564

import kotlin.math.abs

class Solution {
    fun makePalindrome(word: String): String {
        var left = 0;
        var right = word.lastIndex

        var palindrome = word.toCharArray()

        while (left <= right) {
            if (word[left] != word[right]) {
                palindrome[right] = palindrome[left]
            }
            left++
            right--
        }

        return String(palindrome)
    }

    fun findBiggestLessThan(n: Int): Int {
        var left = 0
        var right = n
        var answer = 0

        while (left <= right) {
            val mid = left + (right - left) / 2
            val palindrome = makePalindrome(mid.toString()).toInt()

            if (palindrome < n) {
                left = mid + 1
                answer = palindrome
            } else {
                right = mid - 1
            }
        }

        return answer
    }

    fun findSmallestBiggerThan(n: Int): Int {
        var left = n
        var right = Int.MAX_VALUE
        var answer = 0

        while (left <= right) {
            val mid = left + (right - left) / 2
            val palindrome = makePalindrome(mid.toString()).toInt()

            if (palindrome > n) {
                right = mid - 1
                answer = palindrome
            } else {
                left = mid + 1
            }
        }

        return answer
    }

    fun nearestPalindromic(n: String): String {
        val number = n.toInt()
        val left = findBiggestLessThan(number)
        val right = findSmallestBiggerThan(number)

        val leftDistance = abs(number - left)
        val rightDistance = abs(number - right)

        if (leftDistance < rightDistance) {
            return left.toString()
        } else if (leftDistance > rightDistance) {
            return right.toString()
        } else {
            return minOf(left, right).toString()
        }
    }
}