// https://leetcode.com/problems/integer-to-roman

using System.Text;

public class Solution {
    public string IntToRoman(int num) {
        var numbers = new KeyValuePair<int, string>[] {
            new KeyValuePair<int, string>(1, "I"),
            new KeyValuePair<int, string>(4, "IV"),
            new KeyValuePair<int, string>(5, "V"),
            new KeyValuePair<int, string>(9, "IX"),
            new KeyValuePair<int, string>(10, "X"),
            new KeyValuePair<int, string>(40, "XL"),
            new KeyValuePair<int, string>(50, "L"),
            new KeyValuePair<int, string>(90, "XC"),
            new KeyValuePair<int, string>(100, "C"),
            new KeyValuePair<int, string>(400, "CD"),
            new KeyValuePair<int, string>(500, "D"),
            new KeyValuePair<int, string>(900, "CM"),
            new KeyValuePair<int, string>(1000, "M")
        };

        var result = new StringBuilder();
        while (num > 0) {
            for (int i = numbers.Length - 1; i >= 0; i--) {
                var key = numbers[i].Key;
                if (num >= key) {
                    result.Append(numbers[i].Value);
                    num -= key;
                    break;
                }
            }
        }
        return result.ToString();
    }
}