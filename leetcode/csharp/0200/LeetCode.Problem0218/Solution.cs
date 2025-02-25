using System;
using System.Collections.Generic;

public class Solution {
    public IList<IList<int>> GetSkyline(int[][] buildings) {
        // Get the whole skyline from all the input buildings.
        return DivideAndConquer(buildings, 0, buildings.Length - 1);
    }

    public IList<IList<int>> DivideAndConquer(int[][] buildings, int left, int right) {
        // If the given array of building contains only 1 building, we can
        // directly return the corresponding skyline.
        if (left == right) {
            return new List<IList<int>> {
                new List<int> { buildings[left][0], buildings[left][2] },
                new List<int> { buildings[left][1], 0 }
            };
        }

        // Otherwise, we shall recursively divide the buildings and 
        // merge the skylines.
        int mid = (right - left) / 2 + left;
        var leftSkyline = DivideAndConquer(buildings, left, mid);
        var rightSkyline = DivideAndConquer(buildings, mid + 1, right);

        return MergeSkylines(leftSkyline, rightSkyline);
    }

    // Given two skylines: leftSky and rightSky, merge them into one skyline.
    public IList<IList<int>> MergeSkylines(IList<IList<int>> leftSkyline, IList<IList<int>> rightSkyline) {
        // Initialize leftPos=0, rightPos=0 as the pointer of leftSky and rightSky.
        // Since we start from the left ground, thus our current height curY = 0,
        // the previous height from leftSky and rightSky are also 0.
        var answer = new List<IList<int>>();
        int leftPos = 0, rightPos = 0;
        int leftPrevHeight = 0, rightPrevHeight = 0;

        // Now we start to iterate over both skylines.
        while (leftPos < leftSkyline.Count && rightPos < rightSkyline.Count) {
            int nextLeftX = leftSkyline[leftPos][0];
            int nextRightX = rightSkyline[rightPos][0];
            int curX, curY;

            // If we meet leftSky key point first
            if (nextLeftX < nextRightX) {
                leftPrevHeight = leftSkyline[leftPos][1];
                curX = nextLeftX;
                curY = Math.Max(leftPrevHeight, rightPrevHeight);
                leftPos++;
            } 
            // If we meet rightSky key point first
            else if (nextLeftX > nextRightX) {
                rightPrevHeight = rightSkyline[rightPos][1];
                curX = nextRightX;
                curY = Math.Max(leftPrevHeight, rightPrevHeight);
                rightPos++;
            } 
            // If both skyline key points have the same x
            else {
                leftPrevHeight = leftSkyline[leftPos][1];
                rightPrevHeight = rightSkyline[rightPos][1];
                curX = nextLeftX;
                curY = Math.Max(leftPrevHeight, rightPrevHeight);
                leftPos++;
                rightPos++;
            }

            // Discard those key points that have the same height as the previous one.
            if (answer.Count == 0 || answer[^1][1] != curY) {
                answer.Add(new List<int> { curX, curY });
            }
        }

        // If we finish iterating over any skyline, 
        // just append the rest of the other skyline to the merged skyline.
        while (leftPos < leftSkyline.Count) {
            answer.Add(leftSkyline[leftPos]);
            leftPos++;
        }
        
        while (rightPos < rightSkyline.Count) {
            answer.Add(rightSkyline[rightPos]);
            rightPos++;
        }
        
        return answer;
    }
}
