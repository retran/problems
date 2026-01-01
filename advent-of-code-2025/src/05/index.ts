import { readInput } from "../utils";

const parse = (input: string) => {
  let lines = input.trim().split("\n");

  var ranges: { from: Number; to: Number }[] = [];
  var values: Number[] = [];
  var readingRanges = true;

  for (let line of lines) {
    if (line === "") {
      readingRanges = false;
      continue;
    }

    if (readingRanges) {
      const [min, max] = line.split("-").map((v) => parseInt(v));
      ranges.push({ from: min, to: max });
    } else {
      values.push(parseInt(line));
    }
  }

  return {
    ranges: ranges,
    values: values,
  };
};

export const mergeRanges = (ranges: { from: Number; to: Number }[]) => {
  ranges.sort((a, b) => a.from.valueOf() - b.from.valueOf());
  var i = 0;
  while (i < ranges.length - 1) {
    const current = ranges[i];
    const next = ranges[i + 1];
    if (current.to.valueOf() >= next.from.valueOf()) {
      current.to = Math.max(current.to.valueOf(), next.to.valueOf());
      ranges.splice(i + 1, 1);
    } else {
      i++;
    }
  }
};

export const isValueInRanges = (
  value: Number,
  ranges: { from: Number; to: Number }[],
) => {
  // binary search
  let left = 0;
  let right = ranges.length - 1;
  while (left <= right) {
    const mid = Math.floor((left + right) / 2);
    const range = ranges[mid];
    if (value.valueOf() < range.from.valueOf()) {
      right = mid - 1;
    } else if (value.valueOf() > range.to.valueOf()) {
      left = mid + 1;
    } else {
      return true;
    }
  }
  return false;
};

export const part1 = (rawInput: string) => {
  const { ranges, values } = parse(rawInput);
  mergeRanges(ranges);
  var count = 0;
  for (let value of values) {
    count += isValueInRanges(value, ranges) ? 1 : 0;
  }
  return count;
};

export const part2 = (rawInput: string) => {
  const { ranges, values } = parse(rawInput);
  mergeRanges(ranges);

  var count = 0;
  for (let range of ranges) {
    count += range.to.valueOf() - range.from.valueOf() + 1;
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
