using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_Final.Classes.Message
{
    using Game;
    using Board;
    using Score;
    class Message
    {
        private bool _lost;
        internal string msg;
        private string input;

        public Message()
        {
            this.msg = "nothing";
        }

        private void display_msg(int score)
        {
            Console.WriteLine(msg);
            Console.WriteLine("Score: " + score);
            //print high score
            if (!_lost)
            {
                do
                {
                    Console.WriteLine("Continue Y/N: ");
                    input = Console.ReadLine();
                } while (input[0] != 'y' || input[0] != 'n' || input[0] != 'Y' || input[0] != 'Y');
                if (input[0] == 'n' || input[0] == 'N')
                {
                    Environment.Exit(0);
                }
            } else
            {
                Environment.Exit(0);
            }
        }
        internal void lost_check(Board board, int score) //will call this function after the keyboard press
        {
            int check = 0;
            check = check_vertical(board);
            check += check_horizontal(board);
            //check += 
            if (check == 8)
            {
                msg = "You lost!";
                display_msg(score);
                _lost = true;
            }
            return;
        }

        internal void won(int block, int score)
        {
            if (block == 2048)
            {
                msg = "You won!";
                display_msg(score);
                _lost = false;
            }
        }

        private int check_vertical(Board board)
        {

            int i = 0, j;
            int check = 0; //if it equals 4, then they can't move this way
            int tmp1, tmp2, tmp3, tmp4;

            for (j = 0; j <= 3; j++)
            {
                tmp1 = board.get_board_values(i, j); //uppermost
                tmp2 = board.get_board_values(i + 1, j); //2nd uppermost
                tmp3 = board.get_board_values(i + 2, j); //3rd uppermost
                tmp4 = board.get_board_values(i + 3, j); //4th uppermost

                if (tmp1 != tmp2 && tmp2 != tmp3 && tmp3 != tmp4 && tmp1 != 0 && tmp2 != 0 && tmp3 != 0 && tmp4 != 0)
                    check++;
            }
            return check;
        }       
        private int check_horizontal(Board board)
        {

            int i = 0, j;
            int check = 0; //if it equals 4, then they can't move this way
            int tmp1, tmp2, tmp3, tmp4;

            for (j = 0; j <= 3; j++)
            {
                tmp1 = board.get_board_values(j, i); //leftmost
                tmp2 = board.get_board_values(j, i + 1);
                tmp3 = board.get_board_values(j, i + 2);
                tmp4 = board.get_board_values(j, i + 3);

                if (tmp1 != tmp2 && tmp2 != tmp3 && tmp3 != tmp4 && tmp1 != 0 && tmp2 != 0 && tmp3 != 0 && tmp4 != 0)
                    check++;
            }
            return check;
        }
    }
 
}

