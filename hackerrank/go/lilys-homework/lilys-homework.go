package main

import (
	"bufio"
	"fmt"
	"io"
	"os"
	"slices"
	"sort"
	"strconv"
	"strings"
)

func countSwaps(arr, sorted []int32) int32 {
	indices := make(map[int32]int)
	for index, value := range arr {
		indices[value] = index
	}

	var swaps int32 = 0
	for i := range arr {
		if arr[i] != sorted[i] {
			index := indices[sorted[i]]
			tmp := arr[i]
			arr[i] = arr[index]
			arr[index] = tmp

			indices[arr[i]] = i
			indices[arr[index]] = index
			swaps++
		}
	}

	return swaps
}

/*
 * Complete the 'lilysHomework' function below.
 *
 * The function is expected to return an INTEGER.
 * The function accepts INTEGER_ARRAY arr as parameter.
 */
func lilysHomework(arr []int32) int32 {
	sortedAsc := slices.Clone(arr)
	slices.Sort(sortedAsc)

	sortedDesc := slices.Clone(arr)
	sort.Slice(sortedDesc,
		func(i, j int) bool {
			return sortedDesc[i] > sortedDesc[j]
		})

	swapsAsc := countSwaps(slices.Clone(arr), sortedAsc)
	swapsDesc := countSwaps(slices.Clone(arr), sortedDesc)

	if swapsAsc < swapsDesc {
		return swapsAsc
	} else {
		return swapsDesc
	}
}

func main() {
	reader := bufio.NewReaderSize(os.Stdin, 16*1024*1024)

	stdout, err := os.Create(os.Getenv("OUTPUT_PATH"))
	checkError(err)

	defer stdout.Close()

	writer := bufio.NewWriterSize(stdout, 16*1024*1024)

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

	result := lilysHomework(arr)

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
