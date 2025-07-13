package main

import (
	"bufio"
	"fmt"
	"io"
	"os"
	"strings"
)

func crosswordPuzzle(crossword []string, words string) []string {
	wordList := strings.Split(words, ";")
	grid := make([][]rune, 10)
	for i, rowStr := range crossword {
		grid[i] = []rune(rowStr)
	}

	solve(grid, wordList)

	result := make([]string, 10)
	for i, rowRunes := range grid {
		result[i] = string(rowRunes)
	}
	return result
}

func solve(grid [][]rune, words []string) bool {
	if len(words) == 0 {
		return true
	}

	word := words[0]
	remainingWords := words[1:]

	for r := 0; r < 10; r++ {
		for c := 0; c < 10; c++ {
			// Try placing horizontally
			if canPlaceHorizontal(grid, word, r, c) {
				originalChars := placeHorizontal(grid, word, r, c)
				if solve(grid, remainingWords) {
					return true
				}
				undoHorizontal(grid, originalChars, r, c) // Backtrack
			}

			// Try placing vertically
			if canPlaceVertical(grid, word, r, c) {
				originalChars := placeVertical(grid, word, r, c)
				if solve(grid, remainingWords) {
					return true
				}
				undoVertical(grid, originalChars, r, c) // Backtrack
			}
		}
	}

	return false
}

func canPlaceHorizontal(grid [][]rune, word string, r, c int) bool {
	if c+len(word) > 10 {
		return false
	}
	for i := 0; i < len(word); i++ {
		if grid[r][c+i] != '-' && grid[r][c+i] != rune(word[i]) {
			return false
		}
	}
	return true
}

func placeHorizontal(grid [][]rune, word string, r, c int) []rune {
	original := make([]rune, len(word))
	for i := 0; i < len(word); i++ {
		original[i] = grid[r][c+i]
		grid[r][c+i] = rune(word[i])
	}
	return original
}

func undoHorizontal(grid [][]rune, original []rune, r, c int) {
	for i := 0; i < len(original); i++ {
		grid[r][c+i] = original[i]
	}
}

func canPlaceVertical(grid [][]rune, word string, r, c int) bool {
	if r+len(word) > 10 {
		return false
	}
	for i := 0; i < len(word); i++ {
		if grid[r+i][c] != '-' && grid[r+i][c] != rune(word[i]) {
			return false
		}
	}
	return true
}

func placeVertical(grid [][]rune, word string, r, c int) []rune {
	original := make([]rune, len(word))
	for i := 0; i < len(word); i++ {
		original[i] = grid[r+i][c]
		grid[r+i][c] = rune(word[i])
	}
	return original
}

func undoVertical(grid [][]rune, original []rune, r, c int) {
	for i := 0; i < len(original); i++ {
		grid[r+i][c] = original[i]
	}
}

func main() {
	reader := bufio.NewReaderSize(os.Stdin, 16*1024*1024)

	stdout, err := os.Create(os.Getenv("OUTPUT_PATH"))
	checkError(err)

	defer stdout.Close()

	writer := bufio.NewWriterSize(stdout, 16*1024*1024)

	var crossword []string

	for i := 0; i < 10; i++ {
		crosswordItem := readLine(reader)
		crossword = append(crossword, crosswordItem)
	}

	words := readLine(reader)

	result := crosswordPuzzle(crossword, words)

	for i, resultItem := range result {
		fmt.Fprintf(writer, "%s", resultItem)

		if i != len(result)-1 {
			fmt.Fprintf(writer, "\n")
		}
	}

	fmt.Fprintf(writer, "\n")

	writer.Flush()
}

func readLine(reader *bufio.Reader) string {
	str, _, err := reader.ReadLine()
	if err == io.EOF {
		return ""
	}

	return strings.TrimRight(string(str), "\r\n")
}

func checkError(err error) {
	if err != nil {
		panic(err)
	}
}
