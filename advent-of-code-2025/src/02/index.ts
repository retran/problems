import { readInput } from "../utils";

const parse = (input: string) => {
  return input.trim().split(",");
};

const bigIntMax = (a: bigint, b: bigint) => (a > b ? a : b);
const bigIntMin = (a: bigint, b: bigint) => (a < b ? a : b);

export const part1 = (rawInput: string) => {
  const lines = parse(rawInput);
  let totalSum = 0n;

  for (const line of lines) {
    const parts = line.split("-");
    const start = BigInt(parts[0]);
    const end = BigInt(parts[1]);

    let minLen = parts[0].length;
    if (minLen % 2 !== 0) minLen++;

    const maxLen = parts[1].length;

    for (let length = minLen; length <= maxLen; length += 2) {
      const halfLen = length / 2;
      const halfLenBI = BigInt(halfLen);

      const multiplier = 10n ** halfLenBI + 1n;

      const minHalf = 10n ** (halfLenBI - 1n);
      const maxHalf = 10n ** halfLenBI - 1n;

      const startHalf = (start + multiplier - 1n) / multiplier;
      const endHalf = end / multiplier;

      const effectiveStart = bigIntMax(minHalf, startHalf);
      const effectiveEnd = bigIntMin(maxHalf, endHalf);

      if (effectiveStart <= effectiveEnd) {
        const count = effectiveEnd - effectiveStart + 1n;
        const sumOfHalves = ((effectiveStart + effectiveEnd) * count) / 2n;
        totalSum += sumOfHalves * multiplier;
      }
    }
  }

  return totalSum.toString();
};

const gcd = (a: number, b: number): number => {
  return b === 0 ? a : gcd(b, a % b);
};

const lcm = (a: number, b: number): number => {
  return (a * b) / gcd(a, b);
};

const factorsCache = new Map<number, number[]>();

const getUniquePrimeFactors = (n: number): number[] => {
  if (factorsCache.has(n)) return factorsCache.get(n)!;

  const factors = new Set<number>();
  let d = 2;
  let temp = n;
  while (d * d <= temp) {
    if (temp % d === 0) {
      factors.add(d);
      while (temp % d === 0) temp /= d;
    }
    d++;
  }
  if (temp > 1) factors.add(temp);

  const result = Array.from(factors);
  factorsCache.set(n, result);
  return result;
};

export const part2 = (rawInput: string) => {
  const lines = parse(rawInput);
  let totalSum = 0n;

  for (const line of lines) {
    const parts = line.split("-");
    const start = BigInt(parts[0]);
    const end = BigInt(parts[1]);

    const minTotalLen = parts[0].length;
    const maxTotalLen = parts[1].length;

    for (let totalLen = minTotalLen; totalLen <= maxTotalLen; totalLen++) {
      const primes = getUniquePrimeFactors(totalLen);

      if (primes.length === 0) continue;

      const count = 1 << primes.length;

      for (let mask = 1; mask < count; mask++) {
        let divisorLcm = 1;
        let setBits = 0;

        for (let i = 0; i < primes.length; i++) {
          if ((mask >> i) & 1) {
            divisorLcm = lcm(divisorLcm, primes[i]);
            setBits++;
          }
        }

        const blockLen = totalLen / divisorLcm;
        const blockLenBI = BigInt(blockLen);
        const totalLenBI = BigInt(totalLen);

        const numerator = 10n ** totalLenBI - 1n;
        const denominator = 10n ** blockLenBI - 1n;
        const multiplier = numerator / denominator;

        const minBlock = 10n ** (blockLenBI - 1n);
        const maxBlock = 10n ** blockLenBI - 1n;

        const startBlock = (start + multiplier - 1n) / multiplier;
        const endBlock = end / multiplier;

        const effectiveStart = bigIntMax(minBlock, startBlock);
        const effectiveEnd = bigIntMin(maxBlock, endBlock);

        if (effectiveStart <= effectiveEnd) {
          const n = effectiveEnd - effectiveStart + 1n;
          const sumOfBlocks = ((effectiveStart + effectiveEnd) * n) / 2n;
          const currentSum = sumOfBlocks * multiplier;

          if (setBits % 2 !== 0) {
            totalSum += currentSum;
          } else {
            totalSum -= currentSum;
          }
        }
      }
    }
  }

  return totalSum.toString();
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
