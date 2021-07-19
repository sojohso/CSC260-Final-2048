using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_Final.Classes.Message
{
    using Game;
    class Message
    {
        private bool _lost;
        private string msg;
        private string input;

        public Message()
        {
            _lost = false;
            msg = "nothing";
        }

        private void  display_msg(bool _lost, int score)
        {
            if (!_lost) {
                Console.WriteLine("You won!");
                Console.WriteLine("Score: " + score);
                //print high score
                while (input[0] != 'y' || input[0] != 'n' || input[0] != 'Y' || input[0] != 'Y')
                {
                    Console.WriteLine("Continue Y/N: ");
                    input = Console.ReadLine();
                }
                if(input[0] == 'n' || input[0] == 'N')
                {
                    Environment.Exit(0);
                }
            }
        }

        internal void lost()
        {

        }

        internal void won(int block, int score)
        {
            if (block == 2048)
                display_msg(_lost, score);
        }
    }
}
