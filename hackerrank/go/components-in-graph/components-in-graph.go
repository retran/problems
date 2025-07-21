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

type DisjointSet struct {
	parents []int32
	ranks []int32
	sizes []int32
}

func (set *DisjointSet) Find(idx int32) int32 {
	if set.parents[idx] == idx {
		return idx
	}

	set.parents[idx] = set.Find(set.parents[idx])
	return set.parents[idx]
}

func (set *DisjointSet) Union(left, right int32) {
	left = set.Find(left)
	right = set.Find(right)

	if (left == right) {
		return
	}

	if set.ranks[left] == set.ranks[right] {
		set.parents[left] = right
		set.ranks[right]++
		set.sizes[right] += set.sizes[left]
	} else if set.ranks[left] < set.ranks[right] {
		set.parents[left] = right
		set.sizes[right] += set.sizes[left]
	} else {
		set.parents[right] = left
		set.sizes[left] += set.sizes[right]
	}
}

func createDisjointSet(n int32) *DisjointSet {
	set := DisjointSet{
		parents: make([]int32, n),
		ranks: make([]int32, n),
		sizes: make([]int32, n),
	}

	for i := range n {
		set.parents[i] = i
		set.sizes[i] = 1
	}

	return &set
}

/*
 * Complete the 'componentsInGraph' function below.
 *
 * The function is expected to return an INTEGER_ARRAY.
 * The function accepts 2D_INTEGER_ARRAY gb as parameter.
 */
func componentsInGraph(gb [][]int32) []int32 {
	var count int32 = 0
	
	for _, edge := range gb {
		count = max(count, edge[1])
	}

	set := createDisjointSet(count)

	for _, edge := range gb {
		set.Union(edge[0]-1, edge[1]-1)
	}

	var minSize int32 = math.MaxInt32
	var maxSize int32 = 0
	for i := range count {
		if set.parents[i] == i && set.sizes[i] > 1 {
			minSize = min(minSize, set.sizes[i])
			maxSize = max(maxSize, set.sizes[i])
		}
	}

	return []int32{minSize, maxSize}
}

func main() {
    reader := bufio.NewReaderSize(os.Stdin, 16 * 1024 * 1024)

    stdout, err := os.Create(os.Getenv("OUTPUT_PATH"))
    checkError(err)

    defer stdout.Close()

    writer := bufio.NewWriterSize(stdout, 16 * 1024 * 1024)

    nTemp, err := strconv.ParseInt(strings.TrimSpace(readLine(reader)), 10, 64)
    checkError(err)
    n := int32(nTemp)

    var gb [][]int32
    for i := 0; i < int(n); i++ {
        gbRowTemp := strings.Split(strings.TrimRight(readLine(reader)," \t\r\n"), " ")

        var gbRow []int32
        for _, gbRowItem := range gbRowTemp {
            gbItemTemp, err := strconv.ParseInt(gbRowItem, 10, 64)
            checkError(err)
            gbItem := int32(gbItemTemp)
            gbRow = append(gbRow, gbItem)
        }

        if len(gbRow) != 2 {
            panic("Bad input")
        }

        gb = append(gb, gbRow)
    }

    result := componentsInGraph(gb)

    for i, resultItem := range result {
        fmt.Fprintf(writer, "%d", resultItem)

        if i != len(result) - 1 {
            fmt.Fprintf(writer, " ")
        }
    }

    fmt.Fprintf(writer, "\n")

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
