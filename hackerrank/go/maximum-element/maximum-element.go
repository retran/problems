package main

import (
    "bufio"
    "fmt"
    "io"
    "os"
    "strconv"
    "strings"
		"container/list"
)

/*
 * Complete the 'getMax' function below.
 *
 * The function is expected to return an INTEGER_ARRAY.
 * The function accepts STRING_ARRAY operations as parameter.
 */
func getMax(operations []string) []int32 {
	answers := make([]int32, 0, len(operations))
	stack := list.New()
	maxStack := list.New()
	
	for _, line := range operations {
		parts := strings.Fields(line)
		switch parts[0] {
		case "1":
			val, _ := strconv.Atoi(parts[1])
			value := int32(val)

			stack.PushBack(value)
			if (maxStack.Len() == 0 || maxStack.Back().Value.(int32) <= value) {
				maxStack.PushBack(value)
			}
		case "2":
			if maxStack.Back().Value == stack.Back().Value {
				maxStack.Remove(maxStack.Back())
			}
			stack.Remove(stack.Back())
		case "3":
			answers = append(answers, maxStack.Back().Value.(int32))
		}
	}

	return answers
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

    var ops []string

    for i := 0; i < int(n); i++ {
        opsItem := readLine(reader)
        ops = append(ops, opsItem)
    }

    res := getMax(ops)

    for i, resItem := range res {
        fmt.Fprintf(writer, "%d", resItem)

        if i != len(res) - 1 {
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
