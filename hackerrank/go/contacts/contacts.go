package main

import (
	"bufio"
	"fmt"
	"io"
	"os"
	"strconv"
	"strings"
)

type TrieNode struct {
	Nodes  [26]*TrieNode
	Count int32
}

type Trie struct {
	Root *TrieNode
}

func (trie *Trie) add(name string) {
	current := trie.Root
	current.Count++
	for _, r := range name {
		idx := r - 'a'
		if current.Nodes[idx] == nil {
			current.Nodes[idx] = &TrieNode{}
		}
		current = current.Nodes[idx]
		current.Count++
	}
}

func (trie *Trie) find(pattern string) int32 {
	current := trie.Root
	for _, r := range pattern {
		idx := r - 'a'
		if current.Nodes[idx] == nil {
			return 0
		}
		current = current.Nodes[idx]
	}
	return current.Count
}

func createTrie() *Trie {
	return &Trie{
		Root: &TrieNode{},
	}
}

/*
 * Complete the 'contacts' function below.
 *
 * The function is expected to return an INTEGER_ARRAY.
 * The function accepts 2D_STRING_ARRAY queries as parameter.
 */
func contacts(queries [][]string) []int32 {
	answers := make([]int32, 0, len(queries))

	trie := createTrie()

	for _, query := range queries {
		switch query[0] {
		case "add":
			trie.add(query[1])
		case "find":
			answers = append(answers, trie.find(query[1]))
		}
	}

	return answers
}

func main() {
	reader := bufio.NewReaderSize(os.Stdin, 16*1024*1024)

	stdout, err := os.Create(os.Getenv("OUTPUT_PATH"))
	checkError(err)

	defer stdout.Close()

	writer := bufio.NewWriterSize(stdout, 16*1024*1024)

	queriesRows, err := strconv.ParseInt(strings.TrimSpace(readLine(reader)), 10, 64)
	checkError(err)

	var queries [][]string
	for i := 0; i < int(queriesRows); i++ {
		queriesRowTemp := strings.Split(strings.TrimRight(readLine(reader), " \t\r\n"), " ")

		var queriesRow []string
		for _, queriesRowItem := range queriesRowTemp {
			queriesRow = append(queriesRow, queriesRowItem)
		}

		if len(queriesRow) != 2 {
			panic("Bad input")
		}

		queries = append(queries, queriesRow)
	}

	result := contacts(queries)

	for i, resultItem := range result {
		fmt.Fprintf(writer, "%d", resultItem)

		if i != len(result)-1 {
			fmt.Fprintf(writer, "\n")
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
