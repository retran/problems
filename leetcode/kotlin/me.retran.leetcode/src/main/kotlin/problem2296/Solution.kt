package problem2296

class TextEditor {
    private val left = ArrayDeque<Char>()
    private val right = ArrayDeque<Char>()

    fun addText(text: String) {
        for (c in text) {
            left.addLast(c)
        }
    }

    fun deleteText(k: Int): Int {
        var count = 0
        while (left.isNotEmpty() && count < k) {
            left.removeLast()
            count++
        }
        return count
    }

    fun cursorLeft(k: Int): String {
        repeat(minOf(k, left.size)) {
            right.addFirst(left.removeLast())
        }
        return getCursorText()
    }

    fun cursorRight(k: Int): String {
        repeat(minOf(k, right.size)) {
            left.addLast(right.removeFirst())
        }
        return getCursorText()
    }

    private fun getCursorText(): String {
        return left.takeLast(10).joinToString("")
    }
}

fun test() {
    val editor = TextEditor()

    // Test Case 1:
    // Add "hello", then delete 2 characters.
    // "hello" → delete last 2 ("lo") → remaining "hel"
    editor.addText("hello")
    println("Test Case 1: " + (editor.deleteText(2) == 2))
    // Expected: 2 (deleted "lo"), remaining text "hel"

    // Test Case 2:
    // Now add " world": "hel" + " world" = "hel world"
    // Then move cursor left 5:
    // "hel world" (9 chars) → move 5 from left to right:
    // Left becomes "hel " and right holds "world".
    editor.addText(" world")
    println("Test Case 2: " + (editor.cursorLeft(5) == "hel "))

    // Test Case 3:
    // From previous state, left = "hel " and right = "world".
    // Moving cursor right 3 moves 3 characters from right to left.
    // Left becomes "hel " + "wor" = "hel wor"
    // Right now has remaining "ld".
    println("Test Case 3: " + (editor.cursorRight(3) == "hel wor"))

    // Test Case 4:
    // Now move cursor left 10: this moves all characters from left to right.
    // Left becomes empty; getCursorText returns "".
    println("Test Case 4: " + (editor.cursorLeft(10) == ""))

    // Test Case 5:
    // Move cursor right 10: that brings all characters from right back to left.
    // Right previously held "hel world", so left becomes "hel world".
    println("Test Case 5: " + (editor.cursorRight(10) == "hel world"))

    // Test Case 6:
    // Now add "!!!" at the cursor (at the end).
    // Left becomes "hel world!!!"
    // Then moving cursor left 3 moves the last 3 characters ("!!!") to right.
    // Left becomes "hel world"
    editor.addText("!!!")
    println("Test Case 6: " + (editor.cursorLeft(3) == "hel world"))

    // Test Case 7:
    // Delete text with k=100. Left currently is "hel world" (9 characters).
    // Should delete all 9 characters.
    println("Test Case 7: " + (editor.deleteText(100) == 9))

    // Test Case 8:
    // With left now empty (and right still holding the previously moved characters "!!!"),
    // moving cursor left any amount returns "".
    println("Test Case 8: " + (editor.cursorLeft(2) == ""))

    // Test Case 9:
    // Now, move cursor right 2: right has the 3 characters "!!!".
    // This moves 2 characters to left so that left becomes "!!".
    println("Test Case 9: " + (editor.cursorRight(2) == "!!"))

    // Test Case 10:
    // Now add "abcde": left becomes "!!" + "abcde" = "!!abcde".
    // Then move cursor left 2: this removes the last 2 characters ("de") from left.
    // Left becomes "!!abc", and those 2 characters ("de") are moved to right.
    // Then deleteText(2) removes the last 2 characters from left ("bc"), leaving left = "!!a".
    // Finally, cursorRight(2) moves 2 characters from right to left.
    // After moving, left becomes "!!a" + "de" = "!!ade".
    println("Test Case 10: " + run {
        editor.addText("abcde")   // left: "!!" + "abcde" = "!!abcde"
        editor.cursorLeft(2)        // moves "de" to right; left becomes "!!abc"
        editor.deleteText(2)        // deletes "bc"; left becomes "!!a"
        editor.cursorRight(2) == "!!ade" // moves "de" from right back; left becomes "!!ade"
    })
}