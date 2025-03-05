package problem1861

class Solution {
    fun rotateTheBox(boxGrid: Array<CharArray>): Array<CharArray> {
        val rows = boxGrid.size
        val columns = boxGrid[0].size

        val rotatedBox = Array(columns) { CharArray(rows) { '.' } }

        for (i in 0 until rows) {
            for (j in 0 until columns) {
                rotatedBox[j][i] = boxGrid[rows - 1 - i][j]
            }

            var freePosition = columns - 1
            var currentPosition = columns - 1
            while (currentPosition >= 0) {
                if (rotatedBox[currentPosition][i] == '#') {
                    while (freePosition > currentPosition && rotatedBox[freePosition][i] != '.') {
                        freePosition--
                    }
                    if (freePosition > currentPosition) {
                        rotatedBox[freePosition][i] = '#'
                        rotatedBox[currentPosition][i] = '.'
                    }
                    freePosition--
                } else if (rotatedBox[currentPosition][i] == '*') {
                    freePosition = currentPosition - 1
                }
                currentPosition--
            }
        }

        return rotatedBox
    }
}
