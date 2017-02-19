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
            IList<Node> explored = new List<Node>();

            frontier.Enqueue(startState);

            while(frontier.Count > 0)
            {
                Node currentState = frontier.Dequeue();

                if (compareToGoalState(currentState, goalState))
                {
                    return currentState;
                }else
                {
                    explored.Add(currentState);
                    Utility.CreateChildren(currentState);

                    foreach (Node node in currentState.Children)
                    {
                        if (explored.Contains(node) || frontier.Contains(node))
                        {
                            break;
                        }
                        frontier.Enqueue(node);
                    }
                }
            }
            return null;
        }

        public static Node DFS(Node startState, int[,] goalState)
        {
            Stack<Node> frontier = new Stack<Node>();
            IList<Node> explored = new List<Node>();

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
                    explored.Add(currentState);
                    Utility.CreateChildren(currentState);

                    foreach (Node node in currentState.Children)
                    {                       
                        if (explored.Contains(node) || frontier.Contains(node))
                        {
                            break;
                        }
                        frontier.Push(node);   
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
