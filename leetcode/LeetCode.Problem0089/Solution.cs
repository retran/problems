using System;
using System.Collections.Generic;

public class Solution {
    public IList<int> GrayCode(int n) {
        IList<int> result = new List<int>();
        
        int totalCodes = 1 << n; // 2^n
        for (int i = 0; i < totalCodes; i++) {
            // Apply the Gray code formula
            int grayCode = i ^ (i >> 1);
            result.Add(grayCode);
        }
        
        return result;
    }
}
