import { readInput } from "../utils";

const DIRECTIONS = [
  [-1, 0],
  [1, 0],
  [0, -1],
  [0, 1],
  [-1, -1],
  [-1, 1],
  [1, -1],
  [1, 1],
];

type Grid = string[][];

const parse = (input: string): Grid => {
  return input
    .trim()
    .split("\n")
    .map((line) => line.split(""));
};

const countNeighbors = (
  grid: Grid,
  y: number,
  x: number,
  height: number,
  width: number,
): number => {
  let count = 0;
  for (const [dy, dx] of DIRECTIONS) {
    const newY = y + dy;
    const newX = x + dx;

    if (newY >= 0 && newY < height && newX >= 0 && newX < width) {
      if (grid[newY][newX] === "@") {
        count++;
      }
    }
  }
  return count;
};

export const part1 = (rawInput: string) => {
  const grid = parse(rawInput);
  const height = grid.length;
  const width = grid[0].length;
  let total = 0;

  for (let y = 0; y < height; y++) {
    for (let x = 0; x < width; x++) {
      if (grid[y][x] === "@") {
        if (countNeighbors(grid, y, x, height, width) < 4) {
          total++;
        }
      }
    }
  }

  return total;
};

export const part2 = (rawInput: string) => {
  const grid = parse(rawInput); // Теперь это string[][]
  const height = grid.length;
  const width = grid[0].length;
  let total = 0;

  while (true) {
    const toRemove: { y: number; x: number }[] = [];

    for (let y = 0; y < height; y++) {
      for (let x = 0; x < width; x++) {
        if (grid[y][x] === "@") {
          if (countNeighbors(grid, y, x, height, width) < 4) {
            toRemove.push({ y, x });
          }
        }
      }
    }

    if (toRemove.length === 0) {
      break;
    }

    total += toRemove.length;

    for (const { y, x } of toRemove) {
      grid[y][x] = ".";
    }
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
