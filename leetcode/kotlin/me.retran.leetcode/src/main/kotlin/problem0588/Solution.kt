package problem0588

import java.util.SortedMap
import java.util.TreeMap

class FileSystem {
    private class Entry {
        var isFile: Boolean = false
        var content: String = ""
        val children: SortedMap<String, Entry> = TreeMap()
    }

    private val root = Entry()

    fun ls(path: String): List<String> {
        val node = traversePath(path)
        val result = mutableListOf<String>()
        if (node.isFile) {
            // If the path is a file, return its name.
            val components = path.split("/").filter { it.isNotEmpty() }
            result.add(components.last())
        } else {
            result.addAll(node.children.keys)
        }
        return result
    }

    fun mkdir(path: String) {
        traversePath(path, createIfMissing = true)
    }

    fun addContentToFile(filePath: String, content: String) {
        val node = traversePath(filePath, createIfMissing = true)
        node.isFile = true
        node.content += content
    }

    fun readContentFromFile(filePath: String): String {
        return traversePath(filePath).content
    }

    private fun traversePath(path: String, createIfMissing: Boolean = false): Entry {
        val components = path.split("/").filter { it.isNotEmpty() }
        var node = root
        for (component in components) {
            if (!node.children.containsKey(component)) {
                if (createIfMissing) {
                    node.children[component] = Entry()
                } else {
                    throw Exception("Path does not exist")
                }
            }
            node = node.children[component]!!
        }
        return node
    }
}

fun test() {
    val fs = FileSystem()

    // Initially, ls("/") should return an empty list.
    println("ls('/') = ${fs.ls("/")} (expected: [])")

    // Create directory /a/b/c.
    fs.mkdir("/a/b/c")
    // Now, ls("/") should list "a".
    println("ls('/') = ${fs.ls("/")} (expected: [a])")

    // Add content to file /a/b/c/d with "hello".
    fs.addContentToFile("/a/b/c/d", "hello")
    // ls on a file should return its name.
    println("ls('/a/b/c/d') = ${fs.ls("/a/b/c/d")} (expected: [d])")

    // Read file content.
    println("readContentFromFile('/a/b/c/d') = ${fs.readContentFromFile("/a/b/c/d")} (expected: hello)")

    // Append additional content to the same file.
    fs.addContentToFile("/a/b/c/d", " world")
    println("readContentFromFile('/a/b/c/d') = ${fs.readContentFromFile("/a/b/c/d")} (expected: hello world)")

    // Create another file in /a/b/c.
    fs.addContentToFile("/a/b/c/e", "test")
    // ls on /a/b/c should list both files in sorted order.
    println("ls('/a/b/c') = ${fs.ls("/a/b/c")} (expected: [d, e])")
}