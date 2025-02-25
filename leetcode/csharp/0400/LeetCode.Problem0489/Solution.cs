using System;
using System.Collections.Generic;

public class Solution {
    private readonly int[][] directions = new int[][] {
        new int[] { 0, 1 },   // Right
        new int[] { 1, 0 },   // Down
        new int[] { 0, -1 },  // Left
        new int[] { -1, 0 }   // Up
    };

    public void CleanRoom(Robot robot) {
        HashSet<(int, int)> visited = new HashSet<(int, int)>();
        DFS(robot, 0, 0, 0, visited); // Start at (0, 0) facing right (0)
    }

    private void DFS(Robot robot, int x, int y, int direction, HashSet<(int, int)> visited) {
        // Clean the current cell
        robot.Clean();
        visited.Add((x, y));

        // Explore all four directions
        for (int i = 0; i < 4; i++) {
            int newDirection = (direction + i) % 4; // Calculate the new direction
            int newX = x + directions[newDirection][0]; // New X position
            int newY = y + directions[newDirection][1]; // New Y position

            // Check if the new cell is open and hasn't been visited
            if (!visited.Contains((newX, newY)) && robot.Move()) {
                // Recur to clean the new cell
                DFS(robot, newX, newY, newDirection, visited);

                // Backtrack to the previous cell
                robot.TurnLeft(); // Turn 90 degrees to the left
                robot.TurnLeft(); // Turn another 90 degrees to face back
                robot.Move(); // Move back to the previous cell
                robot.TurnLeft(); // Turn right to the original direction
                robot.TurnLeft(); // Turn right to the original direction

            }
            // Turn the robot right for the next direction
            robot.TurnRight();
        }
    }
}
