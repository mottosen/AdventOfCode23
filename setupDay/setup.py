import sys
from pathlib import Path
import shutil

curr_dir = Path('.')
advent_dir = curr_dir / "../AdventOfCode"

def main(args):

    if len(args) != 2 or not args[1].isdigit():
        print("Program must take exactly one argument, the day to setup.")
        return 1
    
    day = int(args[1])
    
    if (advent_dir / f"inputs/day{day}.txt").is_file() or (advent_dir / f"Day{day}.fs").is_file():
        print("some files already exist.")
        return 1

    file_out = advent_dir / f"inputs/Day{day}.txt"
    with open(file_out, "w") as f:
        f.write("")

    shutil.copyfile(curr_dir / "NewDay.fs", advent_dir / f"Day{day}.fs")
    
    return 0

if __name__ == "__main__":
    main(sys.argv)