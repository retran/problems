package main

import (
    "bufio"
    "fmt"
    "io"
    "os"
    "strconv"
    "strings"
		"container/heap"
)

type CookieHeap []int32

func (h CookieHeap) Len() int {
	return len(h)
}

func (h CookieHeap) Less(i, j int) bool {
	return h[i] < h[j]
}

func (h CookieHeap) Swap(i, j int) {
	h[i], h[j] = h[j], h[i]
}

// How it is work?
func (h *CookieHeap) Push(item any) {
	*h = append(*h, item.(int32))
}

func (h *CookieHeap) Pop() any {
	old := *h
	n := len(old)
	item := old[n-1]
	*h = old[0 : n-1]
	return item
}

/*
 * Complete the 'cookies' function below.
 *
 * The function is expected to return an INTEGER.
 * The function accepts following parameters:
 *  1. INTEGER k
 *  2. INTEGER_ARRAY A
 */
func cookies(k int32, A []int32) int32 {
	cookies := make(CookieHeap, len(A))
	copy(cookies, A)

	heap.Init(&cookies)

	var iterations int32 = 0
	for len(cookies) > 1 && cookies[0] < k {
		first := heap.Pop(&cookies).(int32)
		second := heap.Pop(&cookies).(int32)
		newCookie := first + 2 * second
		heap.Push(&cookies, newCookie)
		iterations++
	}

	if cookies[0] < k {
		return -1
	}

	return iterations
}

func main() {
    reader := bufio.NewReaderSize(os.Stdin, 16 * 1024 * 1024)

    stdout, err := os.Create(os.Getenv("OUTPUT_PATH"))
    checkError(err)

    defer stdout.Close()

    writer := bufio.NewWriterSize(stdout, 16 * 1024 * 1024)

    firstMultipleInput := strings.Split(strings.TrimSpace(readLine(reader)), " ")

    nTemp, err := strconv.ParseInt(firstMultipleInput[0], 10, 64)
    checkError(err)
    n := int32(nTemp)

    kTemp, err := strconv.ParseInt(firstMultipleInput[1], 10, 64)
    checkError(err)
    k := int32(kTemp)

    ATemp := strings.Split(strings.TrimSpace(readLine(reader)), " ")

    var A []int32

    for i := 0; i < int(n); i++ {
        AItemTemp, err := strconv.ParseInt(ATemp[i], 10, 64)
        checkError(err)
        AItem := int32(AItemTemp)
        A = append(A, AItem)
    }

    result := cookies(k, A)

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
