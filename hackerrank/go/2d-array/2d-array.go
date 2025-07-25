package main

import (
	"bufio"
	"fmt"
	"io"
	"math"
	"os"
	"strconv"
	"strings"
)

func calculateHourglassSum(arr [][]int32, i int, j int) int32 {
	return arr[i][j] +
		arr[i-1][j-1] +
		arr[i-1][j] +
		arr[i-1][j+1] +
		arr[i+1][j-1] +
		arr[i+1][j] +
		arr[i+1][j+1]
}

/*
 * Complete the 'hourglassSum' function below.
 *
 * The function is expected to return an INTEGER.
 * The function accepts 2D_INTEGER_ARRAY arr as parameter.
 */
func hourglassSum(arr [][]int32) int32 {
	n := len(arr)
	if n < 3 {
		return 0
	}
	m := len(arr[0])
	if m < 3 {
		return 0
	}
	var maxSum int32 = math.MinInt32
	for i := 1; i < n-1; i++ {
		for j := 1; j < m-1; j++ {
			sum := calculateHourglassSum(arr, i, j)
			maxSum = max(maxSum, sum)
		}
	}
	return maxSum
}

func main() {
	reader := bufio.NewReaderSize(os.Stdin, 16*1024*1024)

	stdout, err := os.Create(os.Getenv("OUTPUT_PATH"))
	checkError(err)

	defer stdout.Close()

	writer := bufio.NewWriterSize(stdout, 16*1024*1024)

	var arr [][]int32
	for i := 0; i < 6; i++ {
		arrRowTemp := strings.Split(strings.TrimRight(readLine(reader), " \t\r\n"), " ")

		var arrRow []int32
		for _, arrRowItem := range arrRowTemp {
			arrItemTemp, err := strconv.ParseInt(arrRowItem, 10, 64)
			checkError(err)
			arrItem := int32(arrItemTemp)
			arrRow = append(arrRow, arrItem)
		}

		if len(arrRow) != 6 {
			panic("Bad input")
		}

		arr = append(arr, arrRow)
	}

	result := hourglassSum(arr)

	fmt.Fprintf(writer, "%d\n", result)

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
