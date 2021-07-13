using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048.Classes
{
	class Game : Game_Board
	{
		private int score;
		private int seed;
		private int i, j; //may need or not
		private int[,] board = new int[4, 4];
		internal bool _win;
		internal int high_score;
		int newField;

		public Game()
		{

			columns = 4;
			rows = 4;
			score = 0;
			seed = 0; //add code to implement a seed for the random number generator
		}

		public void print_board() //after first use
		{
			//Board_Colors board_colors = new Board_Colors();
			for (i = 0; i < rows; i++)
			{
				for (j = 0; j < columns; j++)
				{
					Console.Write(string.Format("{0,5}", board[i, j]));
				}
				Console.WriteLine();
			}
			//insert functionality to add colors from board colors class
		}
		//timer will be added to the game_board class

		private int calculate_score()
		{
			throw new NotImplementedException();
		}

		internal bool win()
		{
			throw new NotImplementedException();
		}

		internal char keyboard_press()
		{
			throw new NotImplementedException();
			//switch statement
			//pass which one by <, >, ^, v into the separate class
			//return this value, then main will insert that value into the class.
			//will insert according to that
		}

		public void game_over()
		{
			throw new NotImplementedException();
		}

		public void restart_game()
		{
			throw new NotImplementedException();
			//clear out board
			//restart as if a new game, therefore reference former functions in other classes
		}
	}
}

