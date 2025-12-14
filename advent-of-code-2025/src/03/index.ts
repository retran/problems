import { readInput } from "../utils";

const parse = (input: string) => {
  return input.trim().split("\n");
};

export const part1 = (rawInput: string) => {
  const lines = parse(rawInput);
  let total = 0;

  const rightMaxes = new Int8Array(2000);

  for (let l = 0; l < lines.length; l++) {
    const line = lines[l];
    const len = line.length;

    let currentMaxRight = -1;

    for (let i = len - 1; i > 0; i--) {
      const digit = line.charCodeAt(i) - 48;
      if (digit > currentMaxRight) {
        currentMaxRight = digit;
      }
      rightMaxes[i] = currentMaxRight;
    }

    let currentMaxLeft = -1;
    let maxScore = -1;

    for (let i = 0; i < len - 1; i++) {
      const digit = line.charCodeAt(i) - 48;

      if (digit > currentMaxLeft) {
        currentMaxLeft = digit;
      }

      const score = currentMaxLeft * 10 + rightMaxes[i + 1];

      if (score > maxScore) {
        maxScore = score;
      }
    }

    total += maxScore;
  }

  return total;
};

export const part2 = (rawInput: string) => {
  const lines = parse(rawInput);
  let total = 0;
  const NEEDED_LENGTH = 12;

  for (let l = 0; l < lines.length; l++) {
    const line = lines[l];
    const len = line.length;

    if (len < NEEDED_LENGTH) continue;

    let currentIdx = 0;
    let currentNum = 0;

    for (let i = 0; i < NEEDED_LENGTH; i++) {
      const remainingNeeded = NEEDED_LENGTH - 1 - i;
      const searchLimit = len - remainingNeeded;

      let maxDigit = -1;
      let maxDigitIdx = -1;

      for (let j = currentIdx; j < searchLimit; j++) {
        const digit = line.charCodeAt(j) - 48;

        if (digit === 9) {
          maxDigit = 9;
          maxDigitIdx = j;
          break;
        }

        if (digit > maxDigit) {
          maxDigit = digit;
          maxDigitIdx = j;
        }
      }

      currentNum = currentNum * 10 + maxDigit;
      currentIdx = maxDigitIdx + 1;
    }

    total += currentNum;
  }

  return total;
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
