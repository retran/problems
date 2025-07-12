package main

import (
	"bufio"
	"fmt"
	"io"
	"os"
	"strconv"
	"strings"
)

type FrequencyMap map[rune]int

func equals(a, b FrequencyMap) bool {

}

var cache map[string]FrequencyMap = make(map[string]FrequencyMap)

func createFrequencyMap(s string) FrequencyMap {
	fm, ok := cache[s]
	if ok {
		return fm
	}

	frequencyMap := make(FrequencyMap)
	for _, r := range s {
		frequencyMap[r]++
	}
	cache[s] = frequencyMap
	return frequencyMap
}

/*
 * Complete the 'sherlockAndAnagrams' function below.
 *
 * The function is expected to return an INTEGER.
 * The function accepts STRING s as parameter.
 */
func sherlockAndAnagrams(s string) int32 {
	var count int32 = 0

	for length := 1; length < len(s); length++ {
		fmt.Println()
		for i := 0; i < len(s)-length; i++ {
			mainFrequencyMap := createFrequencyMap(s[i : i+length])
			for j := i + 1; j < len(s)-length+1; j++ {
				frequencyMap := createFrequencyMap(s[j : j+length])
				if equals(mainFrequencyMap, frequencyMap) {
					count++
				}
			}
		}
	}

	return count
}

func main() {
	reader := bufio.NewReaderSize(os.Stdin, 16*1024*1024)

	stdout, err := os.Create(os.Getenv("OUTPUT_PATH"))
	checkError(err)

	defer stdout.Close()

	writer := bufio.NewWriterSize(stdout, 16*1024*1024)

	qTemp, err := strconv.ParseInt(strings.TrimSpace(readLine(reader)), 10, 64)
	checkError(err)
	q := int32(qTemp)

	for qItr := 0; qItr < int(q); qItr++ {
		s := readLine(reader)

		result := sherlockAndAnagrams(s)

		fmt.Fprintf(writer, "%d\n", result)
	}

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
