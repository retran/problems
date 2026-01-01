import { describe, it, expect } from 'vitest';
import { part1, part2 } from './index';

const exampleInput = `
3-5
10-14
16-20
12-18

1
5
8
11
17
32
`;

describe('Day 05', () => {
  describe('Part 1', () => {
    it('works with example', () => {
      expect(part1(exampleInput)).toBe(3);
    });
  });

  describe('Part 2', () => {
    it('works with example', () => {
      expect(part2(exampleInput)).toBe(14);
    });
  });
});
