import fs from 'fs';
import path from 'path';

const args = process.argv.slice(2);
const dayRaw = args[0];

if (!dayRaw) {
  console.error('Please specify the day: npm run gen <day>');
  process.exit(1);
}

const day = dayRaw.padStart(2, '0');

const baseDir = path.resolve(__dirname, '../src');
const dayDir = path.join(baseDir, day);

if (fs.existsSync(dayDir)) {
  console.error(`Folder src/${day} already exists!`);
  process.exit(1);
}
fs.mkdirSync(dayDir);

const solutionTemplate = `import { readInput } from '../utils';

const parse = (input: string) => {
  return input.trim().split('\\n');
};

export const part1 = (rawInput: string) => {
  const lines = parse(rawInput);
  return;
};

export const part2 = (rawInput: string) => {
  const lines = parse(rawInput);
  return;
};
`;

const testTemplate = `import { describe, it, expect } from 'vitest';
import { part1, part2 } from './index';

const exampleInput = \`
\`;

describe('Day ${day}', () => {
  describe('Part 1', () => {
    it('works with example', () => {
      expect(part1(exampleInput)).toBe(undefined);
    });
  });

  describe('Part 2', () => {
    it('works with example', () => {
      expect(part2(exampleInput)).toBe(undefined);
    });
  });
});
`;

fs.writeFileSync(path.join(dayDir, 'index.ts'), solutionTemplate);
fs.writeFileSync(path.join(dayDir, 'index.test.ts'), testTemplate);
fs.writeFileSync(path.join(dayDir, 'input.txt'), '');

console.log(`Created: src/${day}`);
console.log(`Don't forget to paste your data into input.txt!`);
