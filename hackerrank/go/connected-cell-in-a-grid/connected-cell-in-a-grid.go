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
	parent  []int
	rank    []int
	size    []int
	maxSize int
}

func newUnionFind(n int) *UnionFind {
	parent := make([]int, n)
	rank := make([]int, n)
	size := make([]int, n)

	maxSize := 0
	if n > 0 {
		maxSize = 1
	}

	for i := range n {
		parent[i] = i
		rank[i] = 0
		size[i] = 1
	}

	return &UnionFind{
		parent:  parent,
		rank:    rank,
		size:    size,
		maxSize: maxSize,
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
		if uf.size[rootQ] > uf.maxSize {
			uf.maxSize = uf.size[rootQ]
		}
	} else if uf.rank[rootP] > uf.rank[rootQ] {
		uf.parent[rootQ] = rootP
		uf.size[rootP] += uf.size[rootQ]
		if uf.size[rootP] > uf.maxSize {
			uf.maxSize = uf.size[rootP]
		}
	} else {
		uf.parent[rootQ] = rootP
		uf.rank[rootP]++
		uf.size[rootP] += uf.size[rootQ]
		if uf.size[rootP] > uf.maxSize {
			uf.maxSize = uf.size[rootP]
		}
	}

	return true
}

func getIndex(m, i, j int) int {
	return i*m + j
}

/*
 * Complete the 'connectedCell' function below.
 *
 * The function is expected to return an INTEGER.
 * The function accepts 2D_INTEGER_ARRAY matrix as parameter.
 */
func connectedCell(matrix [][]int32) int32 {
	n := len(matrix)
	if n == 0 {
		return 0
	}
	m := len(matrix[0])
	if m == 0 {
		return 0
	}
	uf := newUnionFind(n * m)

	for i := range n {
		for j := range m {
			if matrix[i][j] == 1 {
				if i < n-1 && matrix[i+1][j] == 1 {
					uf.union(getIndex(m, i, j), getIndex(m, i+1, j))
				}
				if j < m-1 && matrix[i][j+1] == 1 {
					uf.union(getIndex(m, i, j), getIndex(m, i, j+1))
				}
				if i < n-1 && j < m-1 && matrix[i+1][j+1] == 1 {
					uf.union(getIndex(m, i, j), getIndex(m, i+1, j+1))
				}
				if i < n-1 && j > 0 && matrix[i+1][j-1] == 1 {
					uf.union(getIndex(m, i, j), getIndex(m, i+1, j-1))
				}
			}
		}
	}

	return int32(uf.maxSize)
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

	mTemp, err := strconv.ParseInt(strings.TrimSpace(readLine(reader)), 10, 64)
	checkError(err)
	m := int32(mTemp)

	var matrix [][]int32
	for i := 0; i < int(n); i++ {
		matrixRowTemp := strings.Split(strings.TrimRight(readLine(reader), " \t\r\n"), " ")

		var matrixRow []int32
		for _, matrixRowItem := range matrixRowTemp {
			matrixItemTemp, err := strconv.ParseInt(matrixRowItem, 10, 64)
			checkError(err)
			matrixItem := int32(matrixItemTemp)
			matrixRow = append(matrixRow, matrixItem)
		}

		if len(matrixRow) != int(m) {
			panic("Bad input")
		}

		matrix = append(matrix, matrixRow)
	}

	result := connectedCell(matrix)

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
