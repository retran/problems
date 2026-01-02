import { describe, it, expect } from "vitest";
import { part1, part2 } from "./index";

const exampleInput = `
7,1
11,1
11,7
9,7
9,5
2,5
2,3
7,3
`;

describe("Day 09", () => {
  describe("Part 1", () => {
    it("works with example", () => {
      expect(part1(exampleInput)).toBe(50);
    });
  });

  describe("Part 2", () => {
    it("works with example", () => {
      expect(part2(exampleInput)).toBe(24);
    });
  });
});
