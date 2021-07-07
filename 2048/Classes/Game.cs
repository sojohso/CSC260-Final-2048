using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048.Classes
{
    class Game : Update_Board
    {
		internal bool _win;
		internal int high_score;
		int newField;

		public Game()
		{
			throw new NotImplementedException();
		}

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
