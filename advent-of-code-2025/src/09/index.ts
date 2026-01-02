import { readInput } from "../utils";

const parse = (input: string) => {
  return input
    .trim()
    .split("\n")
    .map((line) => line.split(",").map(Number))
    .map((row) => {
      var [x, y] = row;
      return { x, y };
    });
};

export const part1 = (rawInput: string) => {
  const points = parse(rawInput);

  var max = 0;
  for (let i = 0; i < points.length - 1; i++) {
    const pointA = points[i];
    for (let j = i + 1; j < points.length; j++) {
      const pointB = points[j];
      const square =
        (Math.abs(pointA.x - pointB.x) + 1) *
        (Math.abs(pointA.y - pointB.y) + 1);
      if (square > max) {
        max = square;
      }
    }
  }

  return max;
};

export const part2 = (rawInput: string) => {
  const points = parse(rawInput);
  const n = points.length;

  const edges: {
    p1: { x: number; y: number };
    p2: { x: number; y: number };
  }[] = [];

  for (let i = 0; i < n; i++) {
    edges.push({ p1: points[i], p2: points[(i + 1) % n] });
  }

  const isPointInside = (x: number, y: number) => {
    let inside = false;
    for (const edge of edges) {
      const { p1, p2 } = edge;
      const intersect =
        p1.y > y !== p2.y > y &&
        x < ((p2.x - p1.x) * (y - p1.y)) / (p2.y - p1.y) + p1.x;
      if (intersect) inside = !inside;
    }
    return inside;
  };

  const intersectsAnything = (
    x1: number,
    y1: number,
    x2: number,
    y2: number,
  ) => {
    const minX = Math.min(x1, x2),
      maxX = Math.max(x1, x2);
    const minY = Math.min(y1, y2),
      maxY = Math.max(y1, y2);

    for (const edge of edges) {
      const { p1, p2 } = edge;

      if (p1.y === p2.y) {
        if (p1.y > minY && p1.y < maxY) {
          const eMinX = Math.min(p1.x, p2.x),
            eMaxX = Math.max(p1.x, p2.x);
          if (!(eMaxX <= minX || eMinX >= maxX)) {
            return true;
          }
        }
      } else {
        if (p1.x > minX && p1.x < maxX) {
          const eMinY = Math.min(p1.y, p2.y),
            eMaxY = Math.max(p1.y, p2.y);
          if (!(eMaxY <= minY || eMinY >= maxY)) {
            return true;
          }
        }
      }
    }
    return false;
  };

  let maxArea = 0;

  for (let i = 0; i < n; i++) {
    for (let j = i + 1; j < n; j++) {
      const p1 = points[i];
      const p2 = points[j];

      const w = Math.abs(p1.x - p2.x) + 1;
      const h = Math.abs(p1.y - p2.y) + 1;
      const area = w * h;

      if (area <= maxArea) continue;

      if (intersectsAnything(p1.x, p1.y, p2.x, p2.y)) continue;

      const midX = (p1.x + p2.x) / 2;
      const midY = (p1.y + p2.y) / 2;
      if (isPointInside(midX, midY)) {
        maxArea = area;
      }
    }
  }

  return maxArea;
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
