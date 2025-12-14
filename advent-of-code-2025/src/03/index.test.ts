import { describe, it, expect } from 'vitest';
import { part1, part2 } from './index';

const exampleInput = `
987654321111111
811111111111119
234234234234278
818181911112111
`;

describe('Day 03', () => {
  describe('Part 1', () => {
    it('works with example', () => {
      expect(part1(exampleInput)).toBe(357);
    });
  });

  describe('Part 2', () => {
    it('works with example', () => {
      expect(part2(exampleInput)).toBe(3121910778619);
    });
  });
});
