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
            DisplayBoard(startState);
            int[,] goalState = GetGoalState();

            Node startNode = new Node(startState);

            Utility.CreateChildren(startNode);

            Check(startNode);

            Solver.BFS(startNode, goalState);
        }

        private static void Check(Node n)
        {
            Queue<Node> tree = new Queue<Node>();
            int nbrNode = 0;
            tree.Enqueue(n);
            while(tree.Count > 0)
            {
                
                Node current = tree.Dequeue();

                DisplayBoard(current.Board);

                foreach(Node child in current.Children)
                {
                    tree.Enqueue(child);
                    nbrNode++;
                }
            }

            Console.WriteLine("Nombre de Noeud : " + nbrNode);

            Console.ReadLine();
        }

        private static void DisplayBoard(int[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(" |"+ board[i,j] +"| ");
                }
                Console.WriteLine("\n-----------------------\n");
            }
            Console.WriteLine("*****************************************************");
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

                sendBack[i, j] = (int)char.GetNumericValue(c);
                Console.WriteLine("Valeur convertie : " + sendBack[i,j]);
                j++;
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
                for(int j = 0; j < 3; j++)
                {
                    goalState[i,j] = value++;
                }
            }

            return goalState;
        }
    }
}
