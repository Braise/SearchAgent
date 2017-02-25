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
        private static IList<Node> explored;
        private static IList<Node> exploredDfs;

        public static Node BFS(Node startState, int[,] goalState)
        {
           if(compareToGoalState(startState, goalState))
            {
                return startState;
            }

            Utility.CreateChildren(startState);

            explored = new List<Node> { startState };

            IList<Task<Node>> tasks = new List<Task<Node>>();

            foreach (Node n in startState.Children)
            {
                Task<Node> thread = new Task<Node>(() => CheckForMatchBFS(n, goalState));
                tasks.Add(thread);
                thread.Start();
            }

            bool wait = true;

            do
            {
                int index = Task.WaitAny(tasks.ToArray());

                if (tasks[index].Result != null)
                {
                    return tasks[index].Result;
                }

                tasks.RemoveAt(index);

            } while (wait);
            return null;
        }

        private static Node CheckForMatchBFS(Node currentState, int[,] goalState)
        {
            Queue<Node> frontier = new Queue<Node>();
            frontier.Enqueue(currentState);

            while(frontier.Count > 0)
            {
                currentState = frontier.Dequeue();
                if (compareToGoalState(currentState, goalState))
                {
                    Console.WriteLine("Find a solution");
                    return currentState;
                }
                else
                {
                    lock (explored) // write into the List, no other thread should access it during this time
                    {
                        if (!explored.Contains(currentState)) //It's possible that an other thread has already inserted th state into the list
                        {
                            explored.Add(currentState);
                        }
                    }

                    Utility.CreateChildren(currentState);

                    foreach (Node n in currentState.Children)
                    {
                        lock (explored) // read the list, no other thread should modify it during this time
                        {
                            if (explored.Contains(n) || frontier.Contains(n))
                            {
                                continue;
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
            if (compareToGoalState(startState, goalState))
            {
                return startState;
            }

            Utility.CreateChildren(startState);

            exploredDfs= new List<Node> { startState };

            IList<Task<Node>> tasks = new List<Task<Node>>();

            foreach (Node n in startState.Children)
            {
                Task<Node> thread = new Task<Node>(() => CheckForMatchDFS(n, goalState));
                tasks.Add(thread);
                thread.Start();
            }

            bool wait = true;

            do
            {
                int index = Task.WaitAny(tasks.ToArray());

                if (tasks[index].Result != null)
                {
                    return tasks[index].Result;
                }

                tasks.RemoveAt(index);

            } while (wait);
            return null;
        }

        private static Node CheckForMatchDFS(Node startState, int[,] goalState)
        {
            Stack<Node> frontier = new Stack<Node>();

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
                    lock (exploredDfs) // write into the List, no other thread should access it during this time
                    {
                        if (!exploredDfs.Contains(currentState)) //It's possible that an other thread has already inserted the state into the list
                        {
                            exploredDfs.Add(currentState);
                        }
                    }

                    foreach (Node n in currentState.Children)
                    {
                        lock (exploredDfs) // read the list, no other thread should modify it during this time
                        {
                            if (exploredDfs.Contains(n) || frontier.Contains(n))
                            {
                                continue;
                            }
                        }
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
