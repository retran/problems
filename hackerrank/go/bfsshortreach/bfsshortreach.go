package main

import (
	"bufio"
	"container/list"
	"fmt"
	"io"
	"os"
	"strconv"
	"strings"
)

/*
* Complete the 'bfs' function below.
*
* The function is expected to return an INTEGER_ARRAY.
* The function accepts following parameters:
*  1. INTEGER n
*  2. INTEGER m
*  3. 2D_INTEGER_ARRAY edges
*  4. INTEGER s
 */
func bfs(n int32, m int32, edges [][]int32, s int32) []int32 {
	graph := make(map[int32][]int32)

	distances := make([]int32, n)
	for i := range n {
		distances[i] = -1
	}

	for _, edge := range edges {
		from := edge[0]
		to := edge[1]

		graph[from] = append(graph[from], to)
		graph[to] = append(graph[to], from)
	}

	queue := list.New()
	visited := make(map[int32]bool)

	queue.PushBack(s)
	distance := 0

	for queue.Len() > 0 {
		for count := queue.Len(); count > 0; count-- {
			current := queue.Front().Value.(int32)
			queue.Remove(queue.Front())

			if visited[current] {
				continue
			}

			distances[current-1] = int32(distance) * 6
			visited[current] = true

			for _, node := range graph[current] {
				if !visited[node] {
					queue.PushBack(node)
				}
			}
		}
		distance++
	}

	answer := make([]int32, 0, n-1)
	for _, dist := range distances {
		if dist != 0 {
			answer = append(answer, dist)
		}
	}

	return answer
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
		firstMultipleInput := strings.Split(strings.TrimSpace(readLine(reader)), " ")

		nTemp, err := strconv.ParseInt(firstMultipleInput[0], 10, 64)
		checkError(err)
		n := int32(nTemp)

		mTemp, err := strconv.ParseInt(firstMultipleInput[1], 10, 64)
		checkError(err)
		m := int32(mTemp)

		var edges [][]int32
		for i := 0; i < int(m); i++ {
			edgesRowTemp := strings.Split(strings.TrimRight(readLine(reader), " \t\r\n"), " ")

			var edgesRow []int32
			for _, edgesRowItem := range edgesRowTemp {
				edgesItemTemp, err := strconv.ParseInt(edgesRowItem, 10, 64)
				checkError(err)
				edgesItem := int32(edgesItemTemp)
				edgesRow = append(edgesRow, edgesItem)
			}

			if len(edgesRow) != 2 {
				panic("Bad input")
			}

			edges = append(edges, edgesRow)
		}

		sTemp, err := strconv.ParseInt(strings.TrimSpace(readLine(reader)), 10, 64)
		checkError(err)
		s := int32(sTemp)

		result := bfs(n, m, edges, s)

		for i, resultItem := range result {
			fmt.Fprintf(writer, "%d", resultItem)

			if i != len(result)-1 {
				fmt.Fprintf(writer, " ")
			}
		}

		fmt.Fprintf(writer, "\n")
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
