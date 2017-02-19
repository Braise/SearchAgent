using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IASearchAgentPuzzle
{
    class Node
    {
        public int[,] Board { get; set; }
        public IList<Node> Children { get; set; }
        public Queue<Utility.Movement> path { get; set; }

        public Node(int[,] state)
        {
            Board = state;
            Children = new List<Node>();
        }

        public override bool  Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }

            Node n = obj as Node;

            if(n == null)
            {
                return false;
            }

            return Equals(n);
        }

        public bool Equals(Node n)
        {
            if(n == null)
            {
                return false;
            }

            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if(n.Board[i,j] != Board[i,j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
