package main

import (
	"bufio"
	"fmt"
	"io"
	"os"
	"strconv"
	"strings"
)

func getMedianFromCounts(counts []int, d int32) float64 {
	count := 0
	if d%2 != 0 {
		medianPos := int(d/2) + 1
		for value, freq := range counts {
			count += freq
			if count >= medianPos {
				return float64(value)
			}
		}
	} else {
		m1Pos, m2Pos := int(d/2), int(d/2)+1
		m1, m2 := -1, -1

		for value, freq := range counts {
			count += freq
			if m1 == -1 && count >= m1Pos {
				m1 = value
			}
			if m2 == -1 && count >= m2Pos {
				m2 = value
				break
			}
		}
		return float64(m1+m2) / 2.0
	}
	return 0.0
}

/*
 * Complete the 'activityNotifications' function below.
 *
 * The function is expected to return an INTEGER.
 * The function accepts following parameters:
 *  1. INTEGER_ARRAY expenditure
 *  2. INTEGER d
 */
func activityNotifications(expenditure []int32, d int32) int32 {
	var notifications int32 = 0
	counts := make([]int, 201)

	for i := 0; i < int(d); i++ {
		counts[expenditure[i]]++
	}

	for i := int(d); i < len(expenditure); i++ {
		median := getMedianFromCounts(counts, d)

		if float64(expenditure[i]) >= 2*median {
			notifications++
		}

		counts[expenditure[i]]++
		counts[expenditure[i-int(d)]]--
	}

	return notifications
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

	dTemp, err := strconv.ParseInt(firstMultipleInput[1], 10, 64)
	checkError(err)
	d := int32(dTemp)

	expenditureTemp := strings.Split(strings.TrimSpace(readLine(reader)), " ")

	var expenditure []int32

	for i := 0; i < int(n); i++ {
		expenditureItemTemp, err := strconv.ParseInt(expenditureTemp[i], 10, 64)
		checkError(err)
		expenditureItem := int32(expenditureItemTemp)
		expenditure = append(expenditure, expenditureItem)
	}

	result := activityNotifications(expenditure, d)

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
