namespace FindExitInLabrinth
{
    internal class Program
    {
        static int[,] labrynth = new int[,]
        {
            {1, 1, 1, 1, 1, 1, 1 },
            {1, 0, 0, 0, 0, 0, 1 },
            {1, 0, 1, 1, 1, 0, 1 },
            {0, 0, 0, 0, 1, 0, 0 },
            {1, 1, 0, 0, 1, 1, 1 },
            {1, 1, 1, 0, 1, 1, 1 },
            {1, 1, 1, 0, 1, 1, 1 },
            {1, 1, 1, 0, 0, 0, 1 }
        };

        static int FindExits(int[,] labr, int startI, int startJ)
        {
            int edgeOfI = labr.GetLength(0)-1;
            int edgeOfJ = labr.GetLength(1)-1;
            if (startI > edgeOfI || startJ > edgeOfJ || labr[startI, startJ] == 1)
            {
                Console.WriteLine("Index  out of bounds or start is on the wall!");
                return 0;
            }
            var moveStack = new Stack<(int, int)>();
            moveStack.Push((startI,startJ));
            int exits = 0;
            while (moveStack.Count > 0)
            {
                var temp = moveStack.Pop();
                bool isIOutOfEdge = (temp.Item1 > 0 && temp.Item1 < edgeOfI); 
                bool isJOutOfEdge = (temp.Item2 > 0 && temp.Item2 < edgeOfJ);
                bool indexOutOfEdges = isIOutOfEdge && isJOutOfEdge;
                if (labr[temp.Item1, temp.Item2] == 0 && !(indexOutOfEdges))
                {
                   exits++;
                }
                labr[temp.Item1, temp.Item2] = 1;
                bool canMoveUp = isJOutOfEdge && labr[temp.Item1, temp.Item2 - 1] != 1;
                bool canMoveDown = isJOutOfEdge && labr[temp.Item1, temp.Item2 + 1] != 1;
                bool canMoveLeft = isIOutOfEdge && labr[temp.Item1 - 1, temp.Item2] != 1;
                bool canMoveRight = isIOutOfEdge && labr[temp.Item1 + 1, temp.Item2] != 1;
                if (canMoveUp)
                    moveStack.Push(new(temp.Item1, temp.Item2 - 1)); // move up
                if (canMoveDown)
                    moveStack.Push(new(temp.Item1, temp.Item2 + 1)); // move down
                if (canMoveLeft)
                    moveStack.Push(new(temp.Item1 - 1, temp.Item2)); // move left
                if (canMoveRight)
                    moveStack.Push(new(temp.Item1 + 1, temp.Item2)); // move right
                printLabrinth(labr);
            }
            return exits;
        }
        static void printLabrinth(int[,] labr)
        {
            for(int i = 0; i < labr.GetLength(0); i++)
            {
                for(int j = 0; j < labr.GetLength(1); j++)
                {
                    Console.Write(labr[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            printLabrinth(labrynth);
            int exits = FindExits(labrynth, 1, 1);
            Console.WriteLine("Exits in labrinths = " + exits);
        }
    }
}
