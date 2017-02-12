using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MazeSolver
{
    class Program
    {
		// Class that stores information for a point
		// Not sure how I got it to store a version of itself as the parent, but it works
        public class Point
        {
            int x;
            int y;
            Point parent;

            public Point(int daX, int daY)
            {
                 x = daX;
                 y = daY;
                 parent = null;
            }

            public Point(int daX, int daY, Point daParent)
            {
                x = daX;
                y = daY;
                if (daParent != null)
                {
                    parent = (Point)daParent;
                }
                else parent = new Point(-1, -1);
            }

            public override string ToString()
            {
                return String.Format("({0}, {1})", x, y);
            }

            public List<Point> TracePath()
            {
                List<Point> path = new List<Point>();

                Point curPoint = this;

                path.Add(curPoint);

                while (curPoint.Parent != null && curPoint.Parent.X != -1)
                {
                    path.Add(curPoint.Parent);

                    
                    curPoint = curPoint.Parent;

                }

                path.Reverse();

                return path;
            }

            public Point Parent{
                get { return parent; }
            }

            public int X
            {
                get { return x; }
                set { value = x; }
            }
            public int Y
            {
                get { return y; }
                set { value = y; }
            }
        }
		
		// The actual algorithm
        static List<Point> BreadthFirst(int X, int Y, bool[,] array, char[,] given)
        {
            Queue<Point> queue = new Queue<Point>();

            queue.Enqueue(new Point(X, Y, null));

            array[X, Y] = true; // mark current position as true, to show algorithm's already been here
			// array is same size as maze grid
			// and is true where there is wall
			// or where the algorithm has already been

            while (queue.Count > 0)
            {
                Point current = queue.Dequeue();
				
				// Check if at end
                if (given[current.X, current.Y] == 'E')
                {
                    List<Point> path = current.TracePath();
                    return path;
                }
				
				// Check Left
                if (!array[current.X - 1, current.Y])
                {
                    queue.Enqueue(new Point ( current.X - 1, current.Y, current ));
                    array[current.X - 1, current.Y] = true;
                }
				// Check Right
                if (!array[current.X + 1, current.Y])
                {
                    queue.Enqueue(new Point ( current.X + 1, current.Y , current));
                    array[current.X + 1, current.Y] = true;
                }
				// Check Below
                if (!array[current.X, current.Y - 1])
                {
                    queue.Enqueue(new Point ( current.X, current.Y - 1 , current));
                    array[current.X, current.Y - 1] = true;
                }
				// Check Above
                if (!array[current.X, current.Y + 1])
                {
                    queue.Enqueue(new Point ( current.X, current.Y + 1 , current));
                    array[current.X, current.Y + 1] = true;
                }


            }

            return null;
        }


        static void Main(string[] args)
        {
            StreamReader Input = new StreamReader("MAZE.txt");
            Console.Write("Letter (1) or '#' (2) Paths? ");
            bool letterPath = (Console.ReadLine() == "1");
            while (true)
            {
                int length = int.Parse(Input.ReadLine());
                int width = int.Parse(Input.ReadLine());

                char[,] given = new char[width, length];
                bool[,] array = new bool[width, length];

                int[] start = new int[2];

                for (int row = 0; row < length; row++)
                {
                    string curLine = Input.ReadLine();
                    for (int column = 0; column < width; column++)
                    {
                        if (curLine[column] == 'X') array[column, row] = true;
                        else array[column, row] = false;

                        given[column, row] = curLine[column];

                        if (curLine[column] == 'S') start = new int[] { column, row };
                    }
                }


                List<Point> path = BreadthFirst(start[0], start[1], array, given);

                PrintPath(path, given, letterPath);

                for (int row = 0; row < array.GetLength(1); row++)
                {
                    Console.WriteLine();
                    for (int column = 0; column < array.GetLength(0); column++)
                    {
                        if (array[column, row]) Console.Write("X");
                        else Console.Write(" ");
                    }
                }

                Console.WriteLine();

                if (Input.EndOfStream) break;
            }

            Console.WriteLine("\nNOTE: \n'S', 'E', and 'X' are reserved for Start, End, and Walls\nand are not used for in-between steps");
            Console.ReadKey(true);
        }

        static void PrintPath(List<Point> path, char[,] given, bool letterPath)
        {
            //If a path is possible, add it to given
            //If there is no path, skip this step
            if (path != null)
            {
                List<char> letters = new List<char>();
                letters.AddRange("ABCDFGHIJKLMNOPQRTUVWYZ");

                for (int step = 0; step < path.Count; step++)
                {
                    int x = path[step].X;
                    int y = path[step].Y;
                    if (step == 0) given[x, y] = 'S';
                    else if (step == path.Count - 1) given[x, y] = 'E';
                    else
                    {
                        //For Letter Steps
                        if (letterPath)
                        {
                            given[x, y] = letters[(step - 1) % letters.Count];
                        }
                        //For # Steps
                        else
                        given[x, y] = '#';
                    }
                }
            }

                for (int row = 0; row < given.GetLength(1); row++)
                {
                    for (int column = 0; column < given.GetLength(0); column++)
                    {
                        char curChar = given[column, row];

                        if (curChar == 'S') Console.ForegroundColor = ConsoleColor.Green;
                        else if (curChar == 'E') Console.ForegroundColor = ConsoleColor.Red;
                        else if (curChar == 'X') Console.ForegroundColor = ConsoleColor.DarkGray;
                        else Console.ForegroundColor = ConsoleColor.Cyan;

                        Console.Write(curChar);
                    }

                    Console.WriteLine();
                }

                Console.ForegroundColor = ConsoleColor.Gray;

                if (path == null) Console.WriteLine("NO PATH POSSIBLE");
                else Console.WriteLine("{0} STEPS", path.Count - 1);

        }
    }
}
