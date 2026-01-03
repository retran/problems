import { describe, it, expect } from 'vitest';
import { part1, part2 } from './index';

const exampleInput = `
[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}
[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}
`;

describe('Day 10', () => {
  describe('Part 1', () => {
    it('works with example', () => {
      expect(part1(exampleInput)).toBe(7);
    });
  });

  describe('Part 2', () => {
    it('works with example', () => {
      expect(part2(exampleInput)).toBe(33);
    });
  });
});
