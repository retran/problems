package main

import (
	"bufio"
	"fmt"
	"io"
	"os"
	"sort"
	"strconv"
	"strings"
)

/*
 * Complete the 'hackerlandRadioTransmitters' function below.
 *
 * The function is expected to return an INTEGER.
 * The function accepts following parameters:
 * 1. INTEGER_ARRAY x
 * 2. INTEGER k
 */
func hackerlandRadioTransmitters(x []int32, k int32) int32 {
	sort.Slice(x, func(i, j int) bool {
		return x[i] < x[j]
	})

	var transmitters int32 = 0
	n := len(x)
	i := 0

	for i < n {
		transmitters++

		firstUncoveredHouse := x[i]
		transmitterPosIndex := i
		for transmitterPosIndex+1 < n && x[transmitterPosIndex+1] <= firstUncoveredHouse+k {
			transmitterPosIndex++
		}

		transmitterLocation := x[transmitterPosIndex]

		nextUncoveredIndex := transmitterPosIndex
		for nextUncoveredIndex+1 < n && x[nextUncoveredIndex+1] <= transmitterLocation+k {
			nextUncoveredIndex++
		}

		i = nextUncoveredIndex + 1
	}

	return transmitters
}

func main() {
	reader := bufio.NewReaderSize(os.Stdin, 16*1024*1024)

	stdout, err := os.Create(os.Getenv("OUTPUT_PATH"))
	checkError(err)

	defer stdout.Close()

	writer := bufio.NewWriterSize(stdout, 16*1024*1024)

	firstMultipleInput := strings.Split(strings.TrimSpace(readLine(reader)), " ")

	nTemp, err := strconv.ParseInt(firstMultipleInput[0], 10, 64)
	checkError(err)
	n := int32(nTemp)

	kTemp, err := strconv.ParseInt(firstMultipleInput[1], 10, 64)
	checkError(err)
	k := int32(kTemp)

	xTemp := strings.Split(strings.TrimSpace(readLine(reader)), " ")

	var x []int32

	for i := 0; i < int(n); i++ {
		xItemTemp, err := strconv.ParseInt(xTemp[i], 10, 64)
		checkError(err)
		xItem := int32(xItemTemp)
		x = append(x, xItem)
	}

	result := hackerlandRadioTransmitters(x, k)

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
