public class Solution {
    public int GetSum(int a, int b) {
        while (b != 0) {
            int carry = (a & b) << 1; // Calculate carry
            a = a ^ b;                // Calculate sum without carry
            b = carry;                // Assign carry to b and repeat
        }
        return a;
    }
}
