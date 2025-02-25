using System;
using System.Text;

public class Solution {
    public string ToHex(int num) {
        if (num == 0) return "0";
        
        string hexDigits = "0123456789abcdef";
        StringBuilder hex = new StringBuilder();
        
        // Iterate over the bits, processing 4 bits (one hex digit) at a time
        for (int i = 0; i < 8 && num != 0; i++) {
            int currentDigit = num & 0xf; // Get the last 4 bits
            hex.Append(hexDigits[currentDigit]); // Convert to hex digit
            num >>= 4; // Shift right by 4 bits to process the next digit
        }
        
        // Reverse the string as we have added digits in reverse order
        for (int i = 0, j = hex.Length - 1; i < j; i++, j--) {
            char temp = hex[i];
            hex[i] = hex[j];
            hex[j] = temp;
        }

        return hex.ToString();
    }
}
