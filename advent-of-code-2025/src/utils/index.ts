import fs from "fs";
import path from "path";

export function readInput(dir: string, fileName = "input.txt"): string {
  const filePath = path.join(dir, fileName);
  return fs.readFileSync(filePath, "utf-8").replace(/\r\n/g, "\n");
}
