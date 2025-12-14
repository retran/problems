import { describe, it, expect } from 'vitest';
import { part1, part2 } from './index';

const exampleInput = `
L68
L30
R48
L5
R60
L55
L1
L99
R14
L82
`;

describe('Day 01', () => {
  describe('Part 1', () => {
    it('works with example', () => {
      expect(part1(exampleInput)).toBe(3);
    });
  });

  describe('Part 2', () => {
    it('works with example', () => {
      expect(part2(exampleInput)).toBe(6);
    });
  });
});
