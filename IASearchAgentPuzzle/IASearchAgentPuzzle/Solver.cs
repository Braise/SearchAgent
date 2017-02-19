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
        private static Queue<Node> frontier;
        private static IList<Node> explored;

        public static Node BFS(Node startState, int[,] goalState)
        {
           if(compareToGoalState(startState, goalState))
            {
                return startState;
            }

            Utility.CreateChildren(startState);

            explored = new List<Node> { startState };
            Task<Node>[] tasks = new Task<Node>[4];
            int i = 0;

            foreach (Node n in startState.Children) // max 4 states, which is convenient because we only have 4 tasks waiting
            {
                Task<Node> thread = new Task<Node>(() => CheckForMatchBFS(n, goalState));
                tasks[i] = thread;
                i++;
            }

            int index = Task.WaitAny(tasks);

            return tasks[index].Result;
        }

        private static Node CheckForMatchBFS(Node currentState, int[,] goalState)
        {
            Queue<Node> frontier = new Queue<Node>();
            frontier.Enqueue(currentState);

            while(frontier.Count > 0)
            {
                if (compareToGoalState(currentState, goalState))
                {
                    return currentState;
                }
                else
                {
                    lock (explored) // write into the List, no other thread should access it during this time
                    {
                        explored.Add(currentState);
                    }
                    
                    Utility.CreateChildren(currentState);

                    foreach (Node n in currentState.Children)
                    {
                        lock (explored) // read the list, no other thread should modify it during this time
                        {
                            if (explored.Contains(n) || frontier.Contains(n))
                            {
                                break;
                            }
                        }     
                        frontier.Enqueue(n);
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
