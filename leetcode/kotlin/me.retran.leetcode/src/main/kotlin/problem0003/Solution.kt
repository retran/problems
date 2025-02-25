package problem0003

class Solution {
    fun lengthOfLongestSubstring(s: String): Int {
        if (s.isEmpty()) return 0

        var i = 0
        var j = 0
        var maxLength = 0
        val seenChars = mutableSetOf<Char>()

        while (j < s.length) {
            if (s[j] in seenChars) {
                seenChars.remove(s[i])
                i++
            } else {
                seenChars.add(s[j])
                j++
                maxLength = maxOf(maxLength, j - i)
            }
        }

        return maxLength
    }
}
