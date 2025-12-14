import { describe, it, expect } from 'vitest';
import { part1, part2 } from './index';

const exampleInput = `11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124`;

describe('Day 02', () => {
  describe('Part 1', () => {
    it('works with example', () => {
      expect(part1(exampleInput)).toBe("1227775554");
    });
  });

  describe('Part 2', () => {
    it('works with example', () => {
      expect(part2(exampleInput)).toBe("4174379265");
    });
  });
});
