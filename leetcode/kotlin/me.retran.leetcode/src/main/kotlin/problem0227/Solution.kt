package problem0227

// <expression>      ::= <term> <expression_tail>
// <expression_tail> ::= "+" <term> <expression_tail>
//                      |  "-" <term> <expression_tail>
//                      |  ε
//
// <term>            ::= <factor> <term_tail>
// <term_tail>       ::= "*" <factor> <term_tail>
//                      |  "/" <factor> <term_tail>
//                      |  ε
//
// <factor>          ::= <number>
//
// <number>          ::= <digit> <number_tail>
// <number_tail>     ::= <digit> <number_tail>
//                      |  ε
//
// <digit>           ::= "0" | "1" | "2" | "3" | "4"
//                      | "5" | "6" | "7" | "8" | "9"

class Solution {
    private sealed class Token(open val index: Int) {
        data class NumberToken(override val index: Int, val value: Int) : Token(index)
        data class PlusToken(override val index: Int) : Token(index)
        data class MinusToken(override val index: Int) : Token(index)
        data class MultiplicationToken(override val index: Int) : Token(index)
        data class DivisionToken(override val index: Int) : Token(index)
        data class EofToken(override val index: Int) : Token(index)
    }

    private class Lexer(private val input: String) {
        private var index = 0
        private var currentToken: Token = computeNextToken()

        private fun skipWhitespace() {
            while (index < input.length && input[index].isWhitespace()) {
                index++
            }
        }

        private fun computeNextToken(): Token {
            skipWhitespace()
            if (index >= input.length) return Token.EofToken(index)
            val ch = input[index]
            return when {
                ch == '+' -> {
                    index++
                    Token.PlusToken(index - 1)
                }
                ch == '-' -> {
                    index++
                    Token.MinusToken(index - 1)
                }
                ch == '*' -> {
                    index++
                    Token.MultiplicationToken(index - 1)
                }
                ch == '/' -> {
                    index++
                    Token.DivisionToken(index - 1)
                }
                ch.isDigit() -> {
                    val start = index
                    while (index < input.length && input[index].isDigit()) {
                        index++
                    }
                    val value = input.substring(start, index).toInt()
                    Token.NumberToken(start, value)
                }
                else -> throw Exception("Invalid character '$ch' at position $index.")
            }
        }

        fun peek(): Token = currentToken

        fun next(): Token {
            val token = currentToken
            currentToken = computeNextToken()
            return token
        }
    }

    private class Parser(private val lexer: Lexer) {
        fun evaluate(): Int = evaluateExpression()

        private fun evaluateExpression(): Int {
            var result = evaluateTerm()
            while (lexer.peek() is Token.PlusToken || lexer.peek() is Token.MinusToken) {
                val op = lexer.next()
                val term = evaluateTerm()
                result = when (op) {
                    is Token.PlusToken -> result + term
                    is Token.MinusToken -> result - term
                    else -> throw Exception("Unexpected operator at position ${op.index}")
                }
            }
            return result
        }

        private fun evaluateTerm(): Int {
            var result = evaluateFactor()
            while (lexer.peek() is Token.MultiplicationToken || lexer.peek() is Token.DivisionToken) {
                val op = lexer.next()
                val factor = evaluateFactor()
                result = when (op) {
                    is Token.MultiplicationToken -> result * factor
                    is Token.DivisionToken -> result / factor
                    else -> throw Exception("Unexpected operator at position ${op.index}")
                }
            }
            return result
        }

        private fun evaluateFactor(): Int {
            val token = lexer.peek()
            return when (token) {
                is Token.NumberToken -> {
                    lexer.next()
                    token.value
                }
                else -> throw Exception("Expected a number at position ${token.index}")
            }
        }
    }

    fun calculate(s: String): Int {
        val lexer = Lexer(s)
        val parser = Parser(lexer)
        return parser.evaluate()
    }
}
