import { describe, it, expect } from 'vitest';
import { part1, part2 } from './index';

const exampleInput = `
123 328  51 64 
 45 64  387 23 
  6 98  215 314
*   +   *   + 
`;

describe('Day 06', () => {
  describe('Part 1', () => {
    it('works with example', () => {
      expect(part1(exampleInput)).toBe(4277556);
    });
  });

  describe('Part 2', () => {
    it('works with example', () => {
      expect(part2(exampleInput)).toBe(3263827);
    });
  });
});
