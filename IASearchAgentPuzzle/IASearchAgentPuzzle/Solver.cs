using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IASearchAgentPuzzle
{
    class Solver
    {
        private static Queue<Node> frontier = new Queue<Node>();
        private static Queue<Node> explored = new Queue<Node>();

        public static Node BFS(Node startState, int[,] goalState)
        {
            frontier.Enqueue(startState);

            while(frontier.Count > 0)
            {
                Node currentState = frontier.Dequeue();

                if (compareToGoalState(currentState, goalState))
                {
                    return currentState;
                }else
                {
                    Utility.CreateChildren(currentState);

                    foreach(Node n in currentState.Children)
                    {
                        frontier.Enqueue(n);
                    }
                }
            }
            return null;
        }

        private static bool compareToGoalState(Node state, int[,] goalState)
        {
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if(state.Board[i,j] != goalState[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static IList<Node> GetChildState(Node currentState)
        {
            IList<Node> liste = new List<Node>();

            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if(currentState.Board[i,j] == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                if(j == 0)
                                {
                                    //création des nouveaux état sans perdre le précédent
                                }
                                break;
                        }
                    }
                }
            }

            return null;
        }
    }
}
