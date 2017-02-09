using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IASearchAgentPuzzle
{
    class Utility
    {
        public enum Movement { Up, Down, Left, Right}

        public static void CreateChildren(Node leaf)
        {
            IList<Movement> listMovement = GetAllMovements(leaf.Board);

            if(listMovement == null)
            {
                return;
            }

            foreach (Movement mv in listMovement)
            {
                int line, column;
                int[,] board = CopyArray(leaf.Board);
                GetPosition0(board ,out line, out column);

                int temp;
                switch (mv)
                {
                    case Movement.Up:
                        temp = board[line, column];
                        board[line, column] = board[(line - 1 ), column];
                        board[(line - 1), column] = temp;
                        break;
                    case Movement.Down:
                        temp = board[line, column];
                        board[line, column] = board[(line + 1), column];
                        board[(line + 1), column] = temp;
                        break;
                    case Movement.Right:
                        temp = board[line, column];
                        board[line, column] = board[line, (column + 1)];
                        board[line, (column + 1)] = temp;
                        break;
                    case Movement.Left:
                        temp = board[line, column];
                        board[line, column] = board[line, (column - 1)];
                        board[line, (column - 1)] = temp;
                        break;
                }

                Node newNode = new Node(board);

                if(leaf.path == null)
                {
                    newNode.path = new Queue<Movement>(); // Leaf is root
                }else
                {
                    newNode.path = new Queue<Movement>(leaf.path);
                }
                
                newNode.path.Enqueue(mv);
                leaf.Children.Add(newNode);
            }    
        }

        private static int[,] CopyArray(int[,] toCopy)
        {
            int[,] sendBack = new int[3,3];

            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    sendBack[i, j] = toCopy[i, j];
                }
            }

            return sendBack;
        }

        private static void GetPosition0(int[,] board, out int line, out int column)
        {

            // Get the position of 0
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 0)
                    {
                        line = i;
                        column = j;
                        return;
                    }
                }
            }
            line = -1;
            column = -1;
        }

        private static IList<Movement> GetAllMovements(int[,] board)
        {
            int line, column;

            GetPosition0(board, out line, out column);

            //Get the possible movements
            IList<Movement> listMovement = new List<Movement>();

            switch (line)
            {
                case 0:
                    switch (column)
                    {
                        case 0:
                            return null;
                        case 1:
                            listMovement.Add(Movement.Left);
                            listMovement.Add(Movement.Right);
                            return listMovement;
                        case 2:
                            listMovement.Add(Movement.Left);
                            return listMovement;
                    }
                    break;
                case 1:
                    switch (column)
                    {
                        case 0:
                            listMovement.Add(Movement.Up);
                            listMovement.Add(Movement.Down);
                            listMovement.Add(Movement.Right);
                            return listMovement;
                        case 1:
                            listMovement.Add(Movement.Left);
                            listMovement.Add(Movement.Right);
                            listMovement.Add(Movement.Up);
                            listMovement.Add(Movement.Down);
                            return listMovement;
                        case 2:
                            listMovement.Add(Movement.Left);
                            listMovement.Add(Movement.Up);
                            listMovement.Add(Movement.Down);
                            return listMovement;
                    }
                    break;
                case 2:
                    switch (column)
                    {
                        case 0:
                            listMovement.Add(Movement.Up);
                            listMovement.Add(Movement.Right);
                            return listMovement;
                        case 1:
                            listMovement.Add(Movement.Up);
                            listMovement.Add(Movement.Left);
                            listMovement.Add(Movement.Right);
                            return listMovement;
                        case 2:
                            listMovement.Add(Movement.Up);
                            listMovement.Add(Movement.Left);
                            return listMovement;
                    }
                    break;
                default:
                    return null;
            }
            return null;
        }
    }
}
