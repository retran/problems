import { readInput } from "../utils";

class Heap<T> {
  private heap: T[] = [];
  constructor(private compareFn: (a: T, b: T) => number) {}

  public push(val: T): void {
    this.heap.push(val);
    this.siftUp(this.heap.length - 1);
  }

  public pop(): T | undefined {
    if (this.size() === 0) return undefined;
    const top = this.heap[0];
    const last = this.heap.pop()!;
    if (this.size() > 0) {
      this.heap[0] = last;
      this.siftDown(0);
    }
    return top;
  }

  public peek(): T | undefined {
    return this.heap[0];
  }

  public size(): number {
    return this.heap.length;
  }

  public toArray(): T[] {
    return this.heap;
  }

  private siftUp(i: number): void {
    while (i > 0) {
      const p = (i - 1) >> 1;
      if (this.compareFn(this.heap[i], this.heap[p]) < 0) {
        [this.heap[i], this.heap[p]] = [this.heap[p], this.heap[i]];
        i = p;
      } else break;
    }
  }

  private siftDown(i: number): void {
    while (true) {
      let s = i,
        l = 2 * i + 1,
        r = 2 * i + 2;
      if (l < this.size() && this.compareFn(this.heap[l], this.heap[s]) < 0)
        s = l;
      if (r < this.size() && this.compareFn(this.heap[r], this.heap[s]) < 0)
        s = r;
      if (s !== i) {
        [this.heap[i], this.heap[s]] = [this.heap[s], this.heap[i]];
        i = s;
      } else break;
    }
  }
}

class UnionFind {
  private parent: Int32Array;
  private sz: Int32Array;
  private roots: Map<number, number> = new Map();

  constructor(n: number) {
    this.parent = new Int32Array(n);
    this.sz = new Int32Array(n).fill(1);
    for (let i = 0; i < n; i++) {
      this.parent[i] = i;
      this.roots.set(i, 1);
    }
  }

  public find(i: number): number {
    if (this.parent[i] === i) return i;
    return (this.parent[i] = this.find(this.parent[i])); // Path compression
  }

  public union(i: number, j: number): void {
    let rootI = this.find(i);
    let rootJ = this.find(j);
    if (rootI !== rootJ) {
      if (this.sz[rootI] < this.sz[rootJ]) [rootI, rootJ] = [rootJ, rootI];
      this.parent[rootJ] = rootI;
      this.sz[rootI] += this.sz[rootJ];
      this.roots.delete(rootJ);
      this.roots.set(rootI, this.sz[rootI]);
    }
  }

  public count(): number {
    return this.roots.size;
  }

  public getTop3Sizes(): number[] {
    if (this.roots.size <= 3) {
      return Array.from(this.roots.values()).sort((a, b) => b - a);
    }

    const minHeapTop3 = new Heap<number>((a, b) => a - b);

    for (const size of this.roots.values()) {
      if (minHeapTop3.size() < 3) {
        minHeapTop3.push(size);
      } else if (size > minHeapTop3.peek()!) {
        minHeapTop3.pop();
        minHeapTop3.push(size);
      }
    }

    return minHeapTop3.toArray();
  }
}

interface Point {
  x: number;
  y: number;
  z: number;
}

const parse = (input: string): Point[] => {
  return input
    .trim()
    .split("\n")
    .map((line) => {
      const [x, y, z] = line.split(",").map((part) => parseInt(part.trim()));
      return { x, y, z };
    });
};

export const part1 = (rawInput: string, iterations: number) => {
  const junctions = parse(rawInput);
  const n = junctions.length;

  const maxHeap = new Heap<{ i: number; j: number; d: number }>(
    (a, b) => b.d - a.d,
  );

  for (let i = 0; i < n - 1; i++) {
    for (let j = i + 1; j < n; j++) {
      const dx = junctions[i].x - junctions[j].x;
      const dy = junctions[i].y - junctions[j].y;
      const dz = junctions[i].z - junctions[j].z;
      const d = dx * dx + dy * dy + dz * dz;

      if (maxHeap.size() < iterations) {
        maxHeap.push({ i, j, d });
      } else if (d < maxHeap.peek()!.d) {
        maxHeap.pop();
        maxHeap.push({ i, j, d });
      }
    }
  }

  const dsu = new UnionFind(n);
  for (const edge of maxHeap.toArray()) {
    dsu.union(edge.i, edge.j);
  }

  let [x, y, z] = dsu.getTop3Sizes();
  return x * y * z;
};

export const part2 = (rawInput: string) => {
  const junctions = parse(rawInput);
  const n = junctions.length;

  const minHeap = new Heap<{ i: number; j: number; d: number }>(
    (a, b) => a.d - b.d,
  );

  for (let i = 0; i < n - 1; i++) {
    for (let j = i + 1; j < n; j++) {
      const dx = junctions[i].x - junctions[j].x;
      const dy = junctions[i].y - junctions[j].y;
      const dz = junctions[i].z - junctions[j].z;
      const d = dx * dx + dy * dy + dz * dz;
      minHeap.push({ i, j, d });
    }
  }

  const dsu = new UnionFind(n);
  var last = { i: 0, j: 0 };
  while (dsu.count() > 1 && minHeap.size() > 0) {
    const edge = minHeap.pop()!;
    last = { i: edge.i, j: edge.j };
    dsu.union(edge.i, edge.j);
    if (dsu.count() === 1) {
      return junctions[edge.i].x * junctions[edge.j].x;
    }
  }

  throw new Error("Unexpected: Graph never became fully connected");
};

if (require.main === module) {
  const input = readInput(__dirname);

  console.time("Part 1 Time");
  console.log("Part 1 Result:", part1(input, 1000));
  console.timeEnd("Part 1 Time");

  console.log("---");

  console.time("Part 2 Time");
  console.log("Part 2 Result:", part2(input));
  console.timeEnd("Part 2 Time");
}
