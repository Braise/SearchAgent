﻿using System;
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
    }
}
