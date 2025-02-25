package problem0424

class Solution {
    fun characterReplacement(s: String, k: Int): Int {
        val frequencies = IntArray(26)
        var left = 0
        var right = 0

        fun charToIndex(char: Char): Int = char - 'A'

        fun slidingWindowLength(): Int = right - left + 1

        fun biggestFrequency(): Int = frequencies.max()

        fun usedReplacements(): Int = slidingWindowLength() - biggestFrequency()

        fun incFrequency(character: Char) {
            frequencies[charToIndex(character)]++
        }

        fun decFrequency(character: Char) {
            frequencies[charToIndex(character)]--
        }

        incFrequency(s[0])
        var bestLength = 1

        while (right < s.lastIndex) {
            right++
            incFrequency(s[right])

            if (usedReplacements() <= k) {
                if (slidingWindowLength() > bestLength) {
                    bestLength = slidingWindowLength()
                }
            } else {
                decFrequency(s[left])
                left++
            }
        }

        return bestLength
    }
}