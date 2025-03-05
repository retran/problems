package problem0068

class Solution {
    fun fullJustify(words: Array<String>, maxWidth: Int): List<String> {
        val justifiedLines = ArrayList<String>()

        var charactersInCurrentLine = 0
        var currentLine = ArrayList<String>()

        for (word in words) {
            if (charactersInCurrentLine + word.length + currentLine.size > maxWidth) {
                justifiedLines.add(justifyLine(currentLine, charactersInCurrentLine, maxWidth))
                currentLine = ArrayList()
                charactersInCurrentLine = 0
            }

            currentLine.add(word)
            charactersInCurrentLine += word.length
        }

        justifiedLines.add(justifyLeft(currentLine, maxWidth))
        return justifiedLines
    }

    private fun justifyLeft(words: List<String>, maxWidth: Int): String {
        val sb = StringBuilder()

        sb.append(words.joinToString(" "))
        while (sb.length < maxWidth) {
            sb.append(' ')
        }

        return sb.toString()
    }

    private fun justifyLine(words: ArrayList<String>, characters: Int, maxWidth: Int): String {
        if (words.size == 1) {
            return justifyLeft(words, maxWidth)
        }

        val sb = StringBuilder()

        val spaces = maxWidth - characters
        val spaceWidth = spaces / (words.size - 1)
        var additionalSpaces = spaces - spaceWidth * (words.size - 1)

        val materializedSpaceBuilder = StringBuilder()
        for (i in 0..spaceWidth - 1) {
            materializedSpaceBuilder.append(' ')
        }
        val materializedSpace = materializedSpaceBuilder.toString()

        for (i in 0..words.lastIndex - 1) {
            sb.append(words[i])
            sb.append(materializedSpace)
            if (additionalSpaces > 0) {
                additionalSpaces--
                sb.append(' ')
            }
        }
        sb.append(words.last())

        return sb.toString()
    }
}