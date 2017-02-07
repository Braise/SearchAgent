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
        public Node Parent { get; set; }

        public Node(int[,] state)
        {
            Board = state;
        }
    }
}
