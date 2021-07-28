using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_Final.Classes.BoardNS
{
    internal class Board
    {
        readonly private int r = 4;
        readonly private int c = 4;
        public Board()
        {
            this.column = c;
            this.row = r;
        }
        internal int[,] board = new int[4, 4];
        internal int column { get; set; }
        internal int row { get; set; }
        internal int set_board_values(int row, int column, int val)
        {
            board[row, column] = val;
            return val;
        }
        internal int get_board_values(int row, int column)
        {
            return board[row, column];
        }
    }
}
