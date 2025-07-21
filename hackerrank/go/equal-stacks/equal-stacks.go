package main

import (
	"bufio"
	"fmt"
	"io"
	"os"
	"strconv"
	"strings"
)

/*
 * Complete the 'equalStacks' function below.
 *
 * The function is expected to return an INTEGER.
 * The function accepts following parameters:
 *  1. INTEGER_ARRAY h1
 *  2. INTEGER_ARRAY h2
 *  3. INTEGER_ARRAY h3
 */
func equalStacks(h1 []int32, h2 []int32, h3 []int32) int32 {
	stacks := [3][]int32{h1, h2, h3}
	heights := [3]int32{h1[len(h1)-1], h2[len(h2)-1], h3[len(h3)-1]}
	idxs := [3]int{len(h1) - 1, len(h2) - 1, len(h3) - 1}

	var maxHeight int32 = 0

	for {
		if (heights[0] == heights[1] && heights[0] == heights[2]) {
			maxHeight = heights[0]
		}	

		idx := 0
		for i := 1; i < len(stacks); i++ {
			if heights[idx] > heights[i] {
				idx = i
			}
		}

		idxs[idx]--
		
		if idxs[idx] < 0 {
			break
		}

		heights[idx] += stacks[idx][idxs[idx]]
	}
	
	return maxHeight 
}

func main() {
	reader := bufio.NewReaderSize(os.Stdin, 16*1024*1024)

	stdout, err := os.Create(os.Getenv("OUTPUT_PATH"))
	checkError(err)

	defer stdout.Close()

	writer := bufio.NewWriterSize(stdout, 16*1024*1024)

	firstMultipleInput := strings.Split(strings.TrimSpace(readLine(reader)), " ")

	n1Temp, err := strconv.ParseInt(firstMultipleInput[0], 10, 64)
	checkError(err)
	n1 := int32(n1Temp)

	n2Temp, err := strconv.ParseInt(firstMultipleInput[1], 10, 64)
	checkError(err)
	n2 := int32(n2Temp)

	n3Temp, err := strconv.ParseInt(firstMultipleInput[2], 10, 64)
	checkError(err)
	n3 := int32(n3Temp)

	h1Temp := strings.Split(strings.TrimSpace(readLine(reader)), " ")

	var h1 []int32

	for i := 0; i < int(n1); i++ {
		h1ItemTemp, err := strconv.ParseInt(h1Temp[i], 10, 64)
		checkError(err)
		h1Item := int32(h1ItemTemp)
		h1 = append(h1, h1Item)
	}

	h2Temp := strings.Split(strings.TrimSpace(readLine(reader)), " ")

	var h2 []int32

	for i := 0; i < int(n2); i++ {
		h2ItemTemp, err := strconv.ParseInt(h2Temp[i], 10, 64)
		checkError(err)
		h2Item := int32(h2ItemTemp)
		h2 = append(h2, h2Item)
	}

	h3Temp := strings.Split(strings.TrimSpace(readLine(reader)), " ")

	var h3 []int32

	for i := 0; i < int(n3); i++ {
		h3ItemTemp, err := strconv.ParseInt(h3Temp[i], 10, 64)
		checkError(err)
		h3Item := int32(h3ItemTemp)
		h3 = append(h3, h3Item)
	}

	result := equalStacks(h1, h2, h3)

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
