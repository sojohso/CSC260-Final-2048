using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_Final.Classes.Game
{
    using Board;
    class Game
    {
        private readonly int columns;
        private readonly int rows;
        Board board = new Board();
        Board empty_spaces = new Board();
        internal bool _win;
        internal bool _done;
        internal bool _moved;

        public Game()
        {

            this.columns = 4;
            this.rows = 4;
            _win = false;
            _done = false;
            _moved = true;
            initialize_board();
            insert_first_value();
        }

        private void initialize_board()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    board.set_board_values(i, j, 0);
                }
            }
            insert_first_value();
        }

        private void insert_first_value()
        {
            Random random_number = new Random();
            int x = 0;
            int iVal;
            while (x < 3)
            {
                int iRow = random_number.Next(0, 3);
                int iCol = random_number.Next(0, 3);
                iVal = random_number.Next(0, 100) < 75 ? 2 : 4;
                board.set_board_values(iRow, iCol, iVal);
                //add checks to make sure it does not do the same index twice
                x++;
            }
            print_board();
            keyboard_press();
            //create new class of game; timer, score, and the board will be inherited.
        }

        private void print_board() //after first use
        {
            //Board_Colors board_colors = new Board_Colors();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(string.Format("{0,4}", board.get_board_values(i, j))); //found from github.com

                }
                Console.WriteLine();
            }

            return;
            //insert functionality to add colors from board colors class
        }

        internal void keyboard_press()
        {
            int i, j;
            Console.WriteLine();
            Console.WriteLine("Use arrow keys to move the game pieces, ctrl+ c to exit");
            var input = Console.ReadKey();
            //switch statement
            switch (input.Key)
            {
                case ConsoleKey.LeftArrow:
                    break;
                case ConsoleKey.RightArrow:
                    break;
                case ConsoleKey.UpArrow:
                    for (i = 0; i < rows; i++)
                    {
                        j = 1;
                        while (j < 4)
                        {
                            if (board.get_board_values(i, j) != 0)
                            {
                                move_block_vertically(i, j, -1);
                            }

                            j++;
                        }
                    }
                    print_board();
                    Console.WriteLine("Hello World!");
                    keyboard_press();
                    break;
                case ConsoleKey.DownArrow:
                    break;
                default:
                    break;
            }
            //pass which one by <, >, ^, v into the separate class
            //return this value, then main will insert that value into the class.
            //will insert according to that
        }

        internal void move_block_vertically(int i, int j, int x)
        {
            //if(board[x, y + d]!= 0 && board[x, y + d] == board[x, y])
            //{
            /* board[i, j] = 0;
             board[i, j + x] *= 2;
             //calculate score
             _moved = true;*/
            //}
        }
    }
}
