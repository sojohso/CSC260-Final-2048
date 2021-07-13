using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_Final.Classes.Board
{
    internal class Board
    {
        int r = 4;
        int c = 4;
        public Board()
        {
            this.blocked = false;
            this.column = c;
            this.row = r;
        }
        public int[,] board = new int[4, 4];
        internal bool blocked { get; set; }
        internal int column { get; set; }
        internal int row { get; set; }
        internal int set_board_values(int row, int column, int val)
        {
            //add checks to make sure it does not go out of bounds
            board[row, column] = val;
            return val;
        }
        internal int get_board_values(int row, int column)
        {
            //add checks to make sure it does not go out of bounds
            return board[row, column];
        }
    }
}
