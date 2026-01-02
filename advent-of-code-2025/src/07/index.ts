import { readInput } from "../utils";

const parse = (input: string): string[] => {
  return input.trim().split("\n");
};

export const part1 = (rawInput: string): number => {
  const lines = parse(rawInput);
  if (lines.length === 0) return 0;

  const startIdx = lines[0].indexOf("S");
  if (startIdx === -1) return 0;

  let rays = new Set<number>();
  let splits = 0;

  rays.add(startIdx);

  for (let i = 1; i < lines.length; i++) {
    const line = lines[i];
    const nextRays = new Set<number>();

    for (const pos of rays) {
      if (line[pos] === "^") {
        splits++;
        nextRays.add(pos - 1);
        nextRays.add(pos + 1);
      } else {
        nextRays.add(pos);
      }
    }
    rays = nextRays;
  }

  return splits;
};

export const part2 = (rawInput: string): number => {
  const lines = parse(rawInput);
  if (lines.length === 0) return 0;

  const startIdx = lines[0].indexOf("S");
  if (startIdx === -1) return 0;

  let rays = new Map<number, number>();
  rays.set(startIdx, 1);

  for (let i = 1; i < lines.length; i++) {
    const line = lines[i];
    const nextRays = new Map<number, number>();

    for (const [pos, count] of rays) {
      if (line[pos] === "^") {
        const left = pos - 1;
        const right = pos + 1;

        nextRays.set(left, (nextRays.get(left) || 0) + count);
        nextRays.set(right, (nextRays.get(right) || 0) + count);
      } else {
        nextRays.set(pos, (nextRays.get(pos) || 0) + count);
      }
    }
    rays = nextRays;
  }

  return Array.from(rays.values()).reduce((sum, count) => sum + count, 0);
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
