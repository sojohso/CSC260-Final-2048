using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048.Classes
{
    class Game_Board
    {
		private readonly int columns;
		private readonly int rows;
		private int score; //don't know if i need this
        private int timer;
		private readonly Random random_number = new Random();
        private int i, j; //may need or not
		private int[,] board = new int[4, 4];

		public Game_Board()
		{
			columns = 4;
			rows = 4;
			score = 0;
        }
        public void insert_first_values()
        {
            i = 0;
            while (i < 3)
            {
                // Find all empty slots
                List<Tuple<int, int>> empty_spaces = new List<Tuple<int, int>>();
                for (int iRows = 0; iRows < rows; iRows++)
                {
                    for (int iColumns = 0; iColumns < columns; iColumns++)
                    {
                        if (board[iRows, iColumns] == 0)
                        {
                            empty_spaces.Add(new Tuple<int, int>(iRows, iColumns));
                        }
                    }
                }
                int iSpaces = random_number.Next(0, empty_spaces.Count);
                int value = random_number.Next(0, 100) < 70 ? (int)2 : (int)4;
                board[empty_spaces[iSpaces].Item1, empty_spaces[iSpaces].Item2] = value;
                i++;
            }
            print_board();
            //create new class of game; timer, score, and the board will be inherited.
        }

        private void print_board() //after first use
		{
			//Board_Colors board_colors = new Board_Colors();
			for (i = 0; i < rows; i++)
            {
				for (j = 0; j < columns; j++)
                {
					Console.Write(string.Format("{0,4}", board[i, j])); //found from github.com
                }
				Console.WriteLine();
            }
			//insert functionality to add colors from board colors class
		}
		//timer will be added to the game_board class

	}
}
