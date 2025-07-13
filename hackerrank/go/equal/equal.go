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

func equal(arr []int32) int32 {
	if len(arr) == 0 {
		return 0
	}

	minVal := arr[0]
	maxVal := arr[0]
	for _, v := range arr {
		if v < minVal {
			minVal = v
		}
		if v > maxVal {
			maxVal = v
		}
	}

	maxDiff := maxVal - minVal + 4

	dp := make([]int32, maxDiff+1)
	dp[0] = 0
	if maxDiff >= 1 {
		dp[1] = 1
	}
	if maxDiff >= 2 {
		dp[2] = 1
	}
	if maxDiff >= 3 {
		dp[3] = 2 // 1+2
	}
	if maxDiff >= 4 {
		dp[4] = 2 // 2+2
	}

	for i := int32(5); i <= maxDiff; i++ {
		minPrev := min(dp[i-5], min(dp[i-2], dp[i-1]))
		dp[i] = 1 + minPrev
	}

	minTotalOps := int32(math.MaxInt32)

	for i := range int32(5) {
		currentTotalOps := int32(0)
		baseline := minVal - i

		for _, val := range arr {
			diff := val - baseline
			currentTotalOps += dp[diff]
		}

		if currentTotalOps < minTotalOps {
			minTotalOps = currentTotalOps
		}
	}

	return minTotalOps
}

func main() {
	reader := bufio.NewReaderSize(os.Stdin, 16*1024*1024)

	stdout, err := os.Create(os.Getenv("OUTPUT_PATH"))
	checkError(err)

	defer stdout.Close()

	writer := bufio.NewWriterSize(stdout, 16*1024*1024)

	tTemp, err := strconv.ParseInt(strings.TrimSpace(readLine(reader)), 10, 64)
	checkError(err)
	t := int32(tTemp)

	for tItr := 0; tItr < int(t); tItr++ {
		nTemp, err := strconv.ParseInt(strings.TrimSpace(readLine(reader)), 10, 64)
		checkError(err)
		n := int32(nTemp)

		arrTemp := strings.Split(strings.TrimSpace(readLine(reader)), " ")

		var arr []int32

		for i := 0; i < int(n); i++ {
			arrItemTemp, err := strconv.ParseInt(arrTemp[i], 10, 64)
			checkError(err)
			arrItem := int32(arrItemTemp)
			arr = append(arr, arrItem)
		}

		result := equal(arr)

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
