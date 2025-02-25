import kotlin.math.*

class Solution {
    fun splitMessage(message: String, limit: Int): Array<String> {
        if (limit <= 5) {
            return emptyArray()
        }

        fun simulateWithDirection(candidate: Int): Pair<Int, Array<String>?> {
            var idx = 0
            val parts = mutableListOf<String>()

            for (i in 1 until candidate) {
                val suffix = "<$i/$candidate>"
                if (suffix.length >= limit) return Pair(-1, null)
                val available = limit - suffix.length
                if (message.length - idx < available) return Pair(-1, null)
                val content = message.substring(idx, idx + available)
                parts.add(content + suffix)
                idx += available
            }

            val suffix = "<$candidate/$candidate>"
            if (suffix.length >= limit) {
                return Pair(-1, null)
            }

            val available = limit - suffix.length
            val remaining = message.length - idx
            if (remaining > available) {
                return Pair(1, null)
            }

            parts.add(message.substring(idx) + suffix)
            idx += remaining
            return if (idx == message.length && parts.size == candidate)
                Pair(0, parts.toTypedArray())
            else
                Pair(-1, null)
        }

        var answer: Array<String>? = null

        for (d in 1..10) {
            if (limit <= (2 * d + 3)) {
                continue
            }

            val lowCandidate = max(1, 10.0.pow((d - 1).toDouble()).toInt())
            val highCandidate = min(message.length, 10.0.pow(d.toDouble()).toInt() - 1)
            if (lowCandidate > highCandidate) {
                continue
            }

            var low = lowCandidate
            var high = highCandidate
            var candidateFound: Array<String>? = null

            while (low <= high) {
                val mid = low + (high - low) / 2
                val (code, result) = simulateWithDirection(mid)
                when (code) {
                    0 -> {
                        candidateFound = result
                        high = mid - 1
                    }
                    1 -> {
                        low = mid + 1
                    }
                    -1 -> {
                        high = mid - 1
                    }
                }
            }

            if (candidateFound != null) {
                answer = candidateFound
                break
            }
        }

        return answer ?: emptyArray()
    }
}

// ---------------------- Testing ----------------------

fun test() {
    val solution = Solution()

    // Helper: Given an array of parts, reconstruct the message by removing suffixes.
    fun reconstruct(parts: Array<String>): String {
        return parts.joinToString("") { part ->
            val idx = part.lastIndexOf('<')
            if (idx != -1) part.substring(0, idx) else part
        }
    }

    // Test 1: Example 1
    // Input: "this is really a very awesome message", limit = 9
    // Expected: one valid answer with 14 parts.
    val message1 = "this is really a very awesome message"
    val limit1 = 9
    val result1 = solution.splitMessage(message1, limit1)
    println("Test 1 (limit=9):")
    if (result1.isNotEmpty()) {
        println("Parts (${result1.size}): ${result1.joinToString()}")
        println("Reconstructed: \"${reconstruct(result1)}\"")
    } else {
        println("No valid split found")
    }
    println()

    // Test 2: Example 2
    // Input: "short message", limit = 15
    // Expected: valid answer with 2 parts.
    val message2 = "short message"
    val limit2 = 15
    val result2 = solution.splitMessage(message2, limit2)
    println("Test 2 (limit=15):")
    if (result2.isNotEmpty()) {
        println("Parts (${result2.size}): ${result2.joinToString()}")
        println("Reconstructed: \"${reconstruct(result2)}\"")
    } else {
        println("No valid split found")
    }
    println()

    // Test 3: Provided failing case
    // Input: "abbababbbaaa aabaa a", limit = 8
    // Expected: one valid answer (for instance, 7 parts such as: 
    //   ["abb<1/7>", "aba<2/7>", "bbb<3/7>", "aaa<4/7>", " aa<5/7>", "baa<6/7>", " a<7/7>"])
    val message3 = "abbababbbaaa aabaa a"
    val limit3 = 8
    val result3 = solution.splitMessage(message3, limit3)
    println("Test 3 (limit=8):")
    if (result3.isNotEmpty()) {
        println("Parts (${result3.size}): ${result3.joinToString()}")
        println("Reconstructed: \"${reconstruct(result3)}\"")
    } else {
        println("No valid split found")
    }
    println()

    // Test 4: Single-part case.
    // For message that fits in one part.
    // Example: message = "hello", limit = 10. Suffix "<1/1>" is 5 characters; available = 5, message length = 5.
    val message4 = "hello"
    val limit4 = 10
    val result4 = solution.splitMessage(message4, limit4)
    println("Test 4 (single part, limit=10):")
    if (result4.isNotEmpty()) {
        println("Parts (${result4.size}): ${result4.joinToString()}")
        println("Reconstructed: \"${reconstruct(result4)}\"")
    } else {
        println("No valid split found")
    }
    println()

    // Test 5: Impossible case: limit too small.
    val message5 = "any message"
    val limit5 = 5
    val result5 = solution.splitMessage(message5, limit5)
    println("Test 5 (impossible, limit=5):")
    println("Result: ${if (result5.isEmpty()) "Empty array (as expected)" else result5.joinToString()}")
    println()
}

fun main() {
    test()
}
