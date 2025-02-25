package problem0465

class Solution {

    fun minTransfers(transactions: Array<IntArray>): Int {
        fun isBitSet(value: Int, bit: Int): Boolean = ((value shr bit) and 1) != 0
        fun setBit(value: Int, bit: Int): Int = (1 shl bit) or value
        fun unsetBit(value: Int, bit: Int): Int = (1 shl bit).inv() and value

        val accountsAll = IntArray(12)

        for (transaction in transactions) {
            accountsAll[transaction[0]] -= transaction[2]
            accountsAll[transaction[1]] += transaction[2]
        }

        val accounts = accountsAll.filter { it != 0 }.toIntArray()
        val size = accounts.size

        val cache = mutableMapOf<Int, Int>()

        fun countGroupsWithZeroBalance(mask: Int): Int {
            if (mask == 0) {
                return 0
            }

            if (cache.containsKey(mask)) {
                return cache[mask]!!
            }

            var count = 0
            var totalBalance = 0
            for (i in 0..size - 1) {
                if (isBitSet(mask, i)) {
                    totalBalance += accounts[i]
                    count = maxOf(count, countGroupsWithZeroBalance(unsetBit(mask, i)))
                }
            }

            if (totalBalance == 0) {
                count++
            }
            cache[mask] = count
            return count
        }

        val mask = (1 shl size) - 1
        return size - countGroupsWithZeroBalance(mask)
    }
}