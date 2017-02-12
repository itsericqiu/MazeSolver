# MazeSolver
A Breadth-First Search Maze Solver capable of retracing paths

This is a command line program written in C# with Visual Studio as self-practice with Breadth-First Search and Classes/Structs during Grade 11.

This program was implemented with a Breadth-First Search method, as well as a custom-written Node class capable of remembering its parent Node.

It will also traceback routes found and print colour-coded solved mazes with completed paths.

## Input
The program reads from a file named `MAZE.txt` in the same directory, which contains the information for one or more text mazes in the form
```
10
8
XXXXXXXX
X      X
XXSXX XX
X      X
X XX X X
XXXX   X
X EX X X
X XX XXX
X      X
XXXXXXXX
11
20
XXXXXXXXXXXXXXXXXXXX
X XS  X  XX        X
X X  XX XXX XX XXXXX
X    X  X X X  X   X
X X XX XX XXX X  X X
X X       X X X XX X
X X XXXX X X     XEX
XXX  X      X   X  X
X   X X   X X  X X X
X                  X
XXXXXXXXXXXXXXXXXXXX
```
Where the first line contains the maze length (or height) `L`, the second line contains the maze width `W`, and are then followed by `L` lines with `W` characters
representing a maze with `X`s representing walls, `S`s representing starting point(s), and `E`s representing end point(s).

The program supports multiple start options and end options, and in virtually all cases will find the shortest path between any one Start and any one End possible.

`MAZE.txt` can contain as many of these datasets, the program will process each maze and automatically detect the end of the file.

## Output
If a solution is found, the program gives the option to print completed paths represented with letters (`ABC...`) to show step order, or simply a `#` character for each intermediary step between `S` and `E` in the solution.

When run in the Windows Command Line, the output will be colour coded to highlight the solution steps, the start point(s) and the end point(s).
