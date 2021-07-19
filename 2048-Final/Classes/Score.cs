using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_Final.Classes.Score
{
    using Message;
    class Score
    {
        private int score;
        Message msg = new Message();
        public Score()
        {
            this.score = 0;
        }

        internal int calculate_score(int v)
        {
            score = score + v;
            if (v == 2048)
                msg.won(2048, score);
            return score;
        }        
        internal void print_score()
        {
            Console.WriteLine("Score: " + score);
        }

    }
}
