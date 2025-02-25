package problem0361

import kotlin.math.max

class Solution {
    data class Entry(val enemiesInRow: Int, val enemiesInColumn: Int, val totalEnemies: Int)

    fun maxKilledEnemies(grid: Array<CharArray>): Int {
        val rows = grid.size
        val columns = grid[0].size

        var maxKilledEnemies = 0
        val dp = Array(rows) { Array<Entry?>(grid[0].size) { null } }

        for (i in 0..rows - 1) {
            for (j in 0..columns - 1) {
                var enemiesInColumn = 0;
                if (i == 0 || grid[i - 1][j] == 'W') {
                    for (k in i..rows - 1) {
                        if (grid[k][j] == 'E') {
                            enemiesInColumn++
                        }

                        if (grid[k][j] == 'W') {
                            break
                        }
                    }
                } else {
                    enemiesInColumn = dp[i - 1][j]!!.enemiesInColumn
                }

                var enemiesInRow = 0;
                if (j == 0 || grid[i][j - 1] == 'W') {
                    for (k in j..columns - 1) {
                        if (grid[i][k] == 'E') {
                            enemiesInRow++
                        }

                        if (grid[i][k] == 'W') {
                            break
                        }
                    }
                } else {
                    enemiesInRow = dp[i][j - 1]!!.enemiesInRow
                }

                var totalEnemies = enemiesInRow + enemiesInColumn
                if (grid[i][j] != '0') {
                    totalEnemies = 0
                } else if (grid[i][j] == 'E') {
                    totalEnemies -= 1
                }

                dp[i][j] = Entry(enemiesInRow, enemiesInColumn, totalEnemies)
                maxKilledEnemies = max(totalEnemies, maxKilledEnemies)
            }
        }

        return maxKilledEnemies
    }
}