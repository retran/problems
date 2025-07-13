package main

import (
	"bufio"
	"fmt"
	"io"
	"os"
	"strconv"
	"strings"
)

type UnionFind struct {
	parent []int
	rank   []int
	size   []int
}

func newUnionFind(n int) *UnionFind {
	parent := make([]int, n)
	rank := make([]int, n)
	size := make([]int, n)

	for i := range n {
		parent[i] = i
		rank[i] = 0
		size[i] = 1
	}

	return &UnionFind{
		parent: parent,
		rank:   rank,
		size:   size,
	}
}

func (uf *UnionFind) find(p int) int {
	if uf.parent[p] != p {
		uf.parent[p] = uf.find(uf.parent[p])
	}
	return uf.parent[p]
}

func (uf *UnionFind) union(p, q int) bool {
	rootP := uf.find(p)
	rootQ := uf.find(q)

	if rootP == rootQ {
		return false
	}

	if uf.rank[rootP] < uf.rank[rootQ] {
		uf.parent[rootP] = rootQ
		uf.size[rootQ] += uf.size[rootP]
	} else if uf.rank[rootP] > uf.rank[rootQ] {
		uf.parent[rootQ] = rootP
		uf.size[rootP] += uf.size[rootQ]
	} else {
		uf.parent[rootQ] = rootP
		uf.rank[rootP]++
		uf.size[rootP] += uf.size[rootQ]
	}

	return true
}

/*
 * Complete the 'journeyToMoon' function below.
 *
 * The function is expected to return an INTEGER.
 * The function accepts following parameters:
 *  1. INTEGER n
 *  2. 2D_INTEGER_ARRAY astronaut
 */
func journeyToMoon(n int32, astronaut [][]int32) int64 {
	uf := newUnionFind(int(n))

	for _, pair := range astronaut {
		uf.union(int(pair[0]), int(pair[1]))
	}

	var pairs int64 = 0
	var sum int64 = 0

	for country := range int(n) {
		if uf.parent[country] == country {
			pairs += sum * int64(uf.size[country])
			sum += int64(uf.size[country])
		}
	}

	return pairs
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

	pTemp, err := strconv.ParseInt(firstMultipleInput[1], 10, 64)
	checkError(err)
	p := int32(pTemp)

	var astronaut [][]int32
	for i := 0; i < int(p); i++ {
		astronautRowTemp := strings.Split(strings.TrimRight(readLine(reader), " \t\r\n"), " ")

		var astronautRow []int32
		for _, astronautRowItem := range astronautRowTemp {
			astronautItemTemp, err := strconv.ParseInt(astronautRowItem, 10, 64)
			checkError(err)
			astronautItem := int32(astronautItemTemp)
			astronautRow = append(astronautRow, astronautItem)
		}

		if len(astronautRow) != 2 {
			panic("Bad input")
		}

		astronaut = append(astronaut, astronautRow)
	}

	result := journeyToMoon(n, astronaut)

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
