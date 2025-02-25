package problem0722

class Solution {
    fun removeComments(source: Array<String>): List<String> {
        val result = mutableListOf<String>()
        var inBlock = false
        var newLine = StringBuilder()

        for (line in source) {
            var i = 0
            if (!inBlock) newLine = StringBuilder()
            while (i < line.length) {
                if (!inBlock && i + 1 < line.length && line[i] == '/' && line[i + 1] == '/') {
                    break
                }
                if (!inBlock && i + 1 < line.length && line[i] == '/' && line[i + 1] == '*') {
                    inBlock = true
                    i += 1
                } else if (inBlock && i + 1 < line.length && line[i] == '*' && line[i + 1] == '/') {
                    inBlock = false
                    i += 1
                } else if (!inBlock) {
                    newLine.append(line[i])
                }
                i++
            }
            if (!inBlock && newLine.isNotEmpty()) {
                result.add(newLine.toString())
            }
        }
        return result
    }
}

// ---------------------- Testing ----------------------
fun test() {
    val solution = Solution()

    // Test 1:
    // Example from LeetCode:
    // Input:
    // [
    //  "/*Test program",
    //  "int main() {",
    //  "  // variable declaration ",
    //  "int a, b, c;",
    //  "/* This is a test",
    //  "   multiline",
    //  "   comment for",
    //  "   testing */",
    //  "a = b + c;",
    //  "}"
    // ]
    // Expected Output:
    // [
    //  "int main() {",
    //  "int a, b, c;",
    //  "a = b + c;",
    //  "}"
    // ]
    val source1 = arrayOf(
        "/*Test program",
        "int main() {",
        "  // variable declaration ",
        "int a, b, c;",
        "/* This is a test",
        "   multiline",
        "   comment for",
        "   testing */",
        "a = b + c;",
        "}"
    )
    val expected1 = listOf(
        "int main() {",
        "int a, b, c;",
        "a = b + c;",
        "}"
    )
    val output1 = solution.removeComments(source1)
    println("Test 1:")
    println("Expected: $expected1")
    println("Output  : $output1")
    println()

    // Test 2:
    // Inline line comment.
    // Input: ["a//b", "c"]
    // Expected Output: ["a", "c"]
    val source2 = arrayOf("a//b", "c")
    val expected2 = listOf("a", "c")
    val output2 = solution.removeComments(source2)
    println("Test 2:")
    println("Expected: $expected2")
    println("Output  : $output2")
    println()

    // Test 3:
    // Block comment spanning multiple lines.
    // Input: ["a/*comment", "line", "more_comment*/b"]
    // Expected Output: ["ab"]
    val source3 = arrayOf("a/*comment", "line", "more_comment*/b")
    val expected3 = listOf("ab")
    val output3 = solution.removeComments(source3)
    println("Test 3:")
    println("Expected: $expected3")
    println("Output  : $output3")
    println()

    // Test 4:
    // Block comment that starts and ends in the same line.
    // Input: ["code/*comment*/more code"]
    // Expected Output: ["codemore code"]
    val source4 = arrayOf("code/*comment*/more code")
    val expected4 = listOf("codemore code")
    val output4 = solution.removeComments(source4)
    println("Test 4:")
    println("Expected: $expected4")
    println("Output  : $output4")
    println()

    // Test 5:
    // Block comment that removes an entire line.
    // Input: ["/*remove this line*/", "keep this"]
    // Expected Output: ["keep this"]
    val source5 = arrayOf("/*remove this line*/", "keep this")
    val expected5 = listOf("keep this")
    val output5 = solution.removeComments(source5)
    println("Test 5:")
    println("Expected: $expected5")
    println("Output  : $output5")
    println()

    // Test 6:
    // Block comment with a tricky overlapping pattern.
    // Input: ["/*Test", "a//b", "c*/d"]
    // Expected Output: ["d"]
    val source6 = arrayOf("/*Test", "a//b", "c*/d")
    val expected6 = listOf("d")
    val output6 = solution.removeComments(source6)
    println("Test 6:")
    println("Expected: $expected6")
    println("Output  : $output6")
    println()
}

fun main() {
    test()
}
