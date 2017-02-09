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
        

        public static Node BFS(Node startState, int[,] goalState)
        {
            Queue<Node> frontier = new Queue<Node>();
            Queue<Node> explored = new Queue<Node>();

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

        public static Node DFS(Node startState, int[,] goalState)
        {
            Stack<Node> frontier = new Stack<Node>();
            Stack<Node> explored = new Stack<Node>();

            frontier.Push(startState);

            while (frontier.Count > 0)
            {
                Node currentState = frontier.Pop();

                if (compareToGoalState(currentState, goalState))
                {
                    return currentState;
                }
                else
                {
                    Utility.CreateChildren(currentState);

                    foreach (Node n in currentState.Children)
                    {
                        frontier.Push(n);
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
    }
}
