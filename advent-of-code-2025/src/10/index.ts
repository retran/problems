import { readInput } from "../utils";

const parse = (input: string) => {
  return input
    .trim()
    .split("\n")
    .map((line) => {
      const targetStrMatch = line.match(/\[([#\.]+)\]/);
      const targetStr = targetStrMatch ? targetStrMatch[1] : "";

      const voltMatch = line.match(/\{([\d,]+)\}/);
      const joltages = voltMatch ? voltMatch[1].split(",").map(Number) : [];

      const buttonMatches = [...line.matchAll(/\(([\d,]+)\)/g)];
      const buttons = buttonMatches.map((m) => m[1].split(",").map(Number));

      return { targetStr, buttons, joltages };
    });
};

function solveLights(target: string, buttons: number[][]): number {
  const startStr = ".".repeat(target.length);

  if (startStr === target) return 0;

  const queue: [string, number][] = [[startStr, 0]];
  const visited = new Set<string>();
  visited.add(startStr);

  let head = 0;
  while (head < queue.length) {
    const [currentState, dist] = queue[head++];

    for (const button of buttons) {
      const nextStateArr = currentState.split("");
      for (const pos of button) {
        if (pos < nextStateArr.length) {
          nextStateArr[pos] = nextStateArr[pos] === "#" ? "." : "#";
        }
      }
      const nextState = nextStateArr.join("");

      if (nextState === target) {
        return dist + 1;
      }

      if (!visited.has(nextState)) {
        visited.add(nextState);
        queue.push([nextState, dist + 1]);
      }
    }

    if (queue.length > 500000) break;
  }

  return 0;
}

type Rational = { n: bigint; d: bigint };

function gcd(a: bigint, b: bigint): bigint {
  return b === 0n ? (a < 0n ? -a : a) : gcd(b, a % b);
}

const R = {
  from: (n: number | bigint): Rational => ({ n: BigInt(n), d: 1n }),

  simplify: (r: Rational): Rational => {
    if (r.n === 0n) return { n: 0n, d: 1n };
    const common = gcd(r.n, r.d);
    const sign = r.d < 0n ? -1n : 1n;
    return { n: (r.n / common) * sign, d: (r.d / common) * sign };
  },

  add: (a: Rational, b: Rational): Rational =>
    R.simplify({ n: a.n * b.d + b.n * a.d, d: a.d * b.d }),
  sub: (a: Rational, b: Rational): Rational =>
    R.simplify({ n: a.n * b.d - b.n * a.d, d: a.d * b.d }),
  mul: (a: Rational, b: Rational): Rational =>
    R.simplify({ n: a.n * b.n, d: a.d * b.d }),
  div: (a: Rational, b: Rational): Rational =>
    R.simplify({ n: a.n * b.d, d: a.d * b.n }),

  isInteger: (r: Rational) => r.d === 1n,
  isNonNegative: (r: Rational) => r.n >= 0n,

  floor: (r: Rational): bigint => {
    if (r.d === 1n) return r.n;
    if (r.n >= 0n) return r.n / r.d;
    return (r.n - r.d + 1n) / r.d;
  },

  ceil: (r: Rational): bigint => {
    if (r.d === 1n) return r.n;
    if (r.n >= 0n) return (r.n + r.d - 1n) / r.d;
    return r.n / r.d;
  },
};

function solveJoltages(target: number[], buttons: number[][]): number {
  const rows = target.length;
  const cols = buttons.length;

  const M: Rational[][] = Array.from({ length: rows }, (_, i) => {
    const row = new Array(cols + 1).fill(null).map(() => R.from(0));
    for (let j = 0; j < cols; j++) {
      if (buttons[j].includes(i)) row[j] = R.from(1);
    }
    row[cols] = R.from(target[i]);
    return row;
  });

  let pivotRow = 0;
  const pivotCols: number[] = new Array(rows).fill(-1);
  const isPivotCol: boolean[] = new Array(cols).fill(false);

  for (let j = 0; j < cols && pivotRow < rows; j++) {
    let sel = pivotRow;
    while (sel < rows && M[sel][j].n === 0n) sel++;
    if (sel === rows) continue;

    [M[sel], M[pivotRow]] = [M[pivotRow], M[sel]];
    pivotCols[pivotRow] = j;
    isPivotCol[j] = true;

    const div = M[pivotRow][j];
    for (let k = j; k <= cols; k++) M[pivotRow][k] = R.div(M[pivotRow][k], div);

    for (let i = 0; i < rows; i++) {
      if (i !== pivotRow && M[i][j].n !== 0n) {
        const factor = M[i][j];
        for (let k = j; k <= cols; k++) {
          M[i][k] = R.sub(M[i][k], R.mul(factor, M[pivotRow][k]));
        }
      }
    }
    pivotRow++;
  }

  for (let i = pivotRow; i < rows; i++) {
    if (M[i][cols].n !== 0n) return 0;
  }

  const freeVars: number[] = [];
  for (let j = 0; j < cols; j++) if (!isPivotCol[j]) freeVars.push(j);

  let minTotalPresses = Infinity;

  function search(idx: number, assignments: bigint[]) {
    if (idx === freeVars.length) {
      let currentSum = 0;
      let possible = true;
      const fullSolution = new Array(cols).fill(0n);

      for (let i = 0; i < freeVars.length; i++) {
        fullSolution[freeVars[i]] = assignments[i];
        currentSum += Number(assignments[i]);
      }

      if (currentSum >= minTotalPresses) return;

      for (let i = 0; i < pivotRow; i++) {
        const pCol = pivotCols[i];
        let val = M[i][cols];

        for (const fIdx of freeVars) {
          if (M[i][fIdx].n !== 0n) {
            const fVal = assignments[freeVars.indexOf(fIdx)];
            val = R.sub(val, R.mul(M[i][fIdx], R.from(fVal)));
          }
        }

        if (!R.isInteger(val) || !R.isNonNegative(val)) {
          possible = false;
          break;
        }

        fullSolution[pCol] = val.n;
        currentSum += Number(val.n);
      }

      if (possible) {
        minTotalPresses = Math.min(minTotalPresses, currentSum);
      }
      return;
    }

    const fIdx = freeVars[idx];
    let minVal = 0n;
    let maxVal = BigInt(Number.MAX_SAFE_INTEGER);

    for (let i = 0; i < pivotRow; i++) {
      const coeff = M[i][fIdx];
      if (coeff.n === 0n) continue;

      let canFutureVarsHelp = false;
      for (let k = idx + 1; k < freeVars.length; k++) {
        const futureCoeff = M[i][freeVars[k]];
        if (futureCoeff.n < 0n) {
          canFutureVarsHelp = true;
          break;
        }
      }

      if (canFutureVarsHelp) continue;

      let rhs = M[i][cols];
      for (let k = 0; k < idx; k++) {
        const prevF = freeVars[k];
        rhs = R.sub(rhs, R.mul(M[i][prevF], R.from(assignments[k])));
      }

      if (coeff.n > 0n) {
        const bound = R.floor(R.div(rhs, coeff));
        if (bound < maxVal) maxVal = bound;
      } else {
        const bound = R.ceil(R.div(rhs, coeff));
        if (bound > minVal) minVal = bound;
      }
    }

    if (minVal < 0n) minVal = 0n;
    if (maxVal === BigInt(Number.MAX_SAFE_INTEGER)) {
      maxVal = minVal + 2000n;
    }

    if (minVal > maxVal) return;

    for (let v = minVal; v <= maxVal; v++) {
      assignments[idx] = v;
      search(idx + 1, assignments);
      if (minTotalPresses !== Infinity && v - minVal > 100n) break;
    }
  }

  search(0, new Array(freeVars.length).fill(0n));

  if (minTotalPresses === Infinity) {
    throw new Error("No solution found");
  }

  return minTotalPresses;
}

export const part1 = (rawInput: string) => {
  const tasks = parse(rawInput);
  let sum = 0;
  tasks.forEach(({ targetStr, buttons }) => {
    sum += solveLights(targetStr, buttons);
  });
  return sum;
};

export const part2 = (rawInput: string) => {
  const tasks = parse(rawInput);
  let sum = 0;
  tasks.forEach(({ joltages, buttons }) => {
    const solution = solveJoltages(joltages, buttons);
    sum += solution;
  });
  return sum;
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
