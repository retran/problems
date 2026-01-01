import { readInput } from "../utils";

const parseGrid = (input: string) => {
  const lines = input.split(/\r?\n/).filter((l) => l.length > 0);
  const opsRow = lines.pop() || "";
  const argsRows = lines;
  const width = Math.max(opsRow.length, ...argsRows.map((r) => r.length));

  return { argsRows, opsRow, width };
};

export const part1 = (rawInput: string) => {
  const lines = rawInput.trim().split("\n");
  const ops = lines.pop()!.trim().split(/\s+/);
  const args = lines.map((l) => l.trim().split(/\s+/).map(Number));

  let sum = 0;
  for (let i = 0; i < ops.length; i++) {
    const values = args.map((row) => row[i]);
    if (ops[i] === "*") sum += values.reduce((a, b) => a * b, 1);
    else if (ops[i] === "+") sum += values.reduce((a, b) => a + b, 0);
  }
  return sum;
};

export const part2 = (rawInput: string) => {
  const { argsRows, opsRow, width } = parseGrid(rawInput);

  let grandTotal = 0;
  let currentProblemValues: number[] = [];
  let currentOperator = "";

  for (let x = width - 1; x >= 0; x--) {
    let verticalDigits = "";

    for (let y = 0; y < argsRows.length; y++) {
      const char = argsRows[y][x];
      if (char !== undefined && char !== " " && char !== "") {
        verticalDigits += char;
      }
    }

    const opChar = opsRow[x]?.trim();
    if (opChar) {
      currentOperator = opChar;
    }

    if (verticalDigits.length > 0) {
      currentProblemValues.push(parseInt(verticalDigits, 10));
    }

    const nextColHasContent =
      x > 0 &&
      ((opsRow[x - 1] && opsRow[x - 1] !== " ") ||
        argsRows.some((row) => row[x - 1] && row[x - 1] !== " "));

    if (!nextColHasContent || x === 0) {
      if (currentProblemValues.length > 0) {
        if (currentOperator === "*") {
          grandTotal += currentProblemValues.reduce((a, b) => a * b, 1);
        } else if (currentOperator === "+") {
          grandTotal += currentProblemValues.reduce((a, b) => a + b, 0);
        }

        currentProblemValues = [];
        currentOperator = "";
      }
    }
  }

  return grandTotal;
};

if (require.main === module) {
  const input = readInput(__dirname);

  console.time("Part 1 Time");
  console.log("Part 1 Result:", part1(input));
  console.timeEnd("Part 1 Time");

  console.log("---");

  console.time("Part 2 Time");
  console.log("Part 2 Result:", part2(input));
  console.timeEnd("Part 2 Time");
}
