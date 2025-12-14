import { readInput } from "../utils";

const mod = (n: number, m: number): number => ((n % m) + m) % m;

export const part1 = (rawInput: string) => {
  const lines = rawInput.trim().split("\n");
  let current = 50;
  let count = 0;

  for (let i = 0; i < lines.length; i++) {
    const line = lines[i];
    const isLeft = line.charCodeAt(0) === 76; // 'L' is 76
    const amount = parseInt(line.substring(1), 10);
    const delta = isLeft ? -amount : amount;
    current = mod(current + delta, 100);
    if (current === 0) {
      count++;
    }
  }

  return count;
};

export const part2 = (rawInput: string) => {
  const lines = rawInput.trim().split("\n");

  let absolutePos = 50;
  let count = 0;

  for (let i = 0; i < lines.length; i++) {
    const line = lines[i];
    const isLeft = line.charCodeAt(0) === 76;
    const amount = parseInt(line.substring(1), 10);
    const delta = isLeft ? -amount : amount;
    const prevLap = Math.floor(absolutePos / 100);
    absolutePos += delta;
    const currentLap = Math.floor(absolutePos / 100);
    count += Math.abs(currentLap - prevLap);
  }

  return count;
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
