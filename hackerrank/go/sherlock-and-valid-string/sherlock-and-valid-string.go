package main

import (
	"bufio"
	"fmt"
	"io"
	"os"
	"strings"
)

/*
 * Complete the 'isValid' function below.
 *
 * The function is expected to return a STRING.
 * The function accepts STRING s as parameter.
 */
func isValid(s string) string {
	frequencies := make(map[rune]int)

	for _, character := range s {
		frequencies[character]++
	}

	count := 0
	mostOftenFrequency := 0
	counts := make(map[int]int)
	for _, frequency := range frequencies {
		counts[frequency]++
		if counts[frequency] > count {
			count = counts[frequency]
			mostOftenFrequency = frequency
		}
	}

	removes := 1
	for _, frequency := range frequencies {
		if frequency != mostOftenFrequency {
			if (frequency == 1 || frequency-mostOftenFrequency == 1) && removes > 0 {
				removes--
			} else {
				return "NO"
			}
		}
	}

	return "YES"
}

func main() {
	reader := bufio.NewReaderSize(os.Stdin, 16*1024*1024)

	stdout, err := os.Create(os.Getenv("OUTPUT_PATH"))
	checkError(err)

	defer stdout.Close()

	writer := bufio.NewWriterSize(stdout, 16*1024*1024)

	s := readLine(reader)

	result := isValid(s)

	fmt.Fprintf(writer, "%s\n", result)

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
