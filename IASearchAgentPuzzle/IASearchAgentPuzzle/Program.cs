using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IASearchAgentPuzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Entrez l'algorithme à appliquer : ");
            string algo = Console.ReadLine();

            Console.WriteLine("Entrez le tableau de départ : ");
            string startBoard = Console.ReadLine();

            int[,] startState = GetBoardFromString(startBoard);
            int[,] goalState = GetGoalState();

            Node startNode = new Node(startState);

            Solver.BFS(startNode, goalState);
        }

        private static int[,] GetBoardFromString(string startBoard)
        {
            int[,] sendBack = new int[3,3];
            int i = 0;
            int j = 0;


            foreach(char c in startBoard)
            {
                if(c == ',')
                {
                    continue;
                }

                sendBack[i, j++] = Convert.ToInt32(c); 

                if(j == 3)
                {
                    j = 0;
                    i++;
                }
            }

            return sendBack;
        }

        private static int[,] GetGoalState()
        {
            int[,] goalState = new int[3, 3];
            int value = 0;


            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; i < 3; j++)
                {
                    goalState[i, j] = value++;
                }
            }

            return goalState;
        }
    }
}
