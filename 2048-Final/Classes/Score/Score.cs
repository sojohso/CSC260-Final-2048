using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2048_Final.Classes.ScoreNS
{
    using MessageNS;
    class Score
    {
        private int score;
        private string high_score;
        private string file;
        private int hs;
        private Message msg = new Message();
        public Score()
        {
            this.score = 0;
            this.file = "..\\..\\..\\Classes\\Score\\high_score.txt";
            this.high_score = System.IO.File.ReadAllText(file);
            this.hs = int.Parse(high_score);
        }
        internal int get_score()
        {
            return score;
        }

        internal int calculate_score(int v)
        {
            score = score + v;
            check_hs();
            if (v == 2048)
                msg.won(2048, score);
            return score;
        }        
        internal void print_score()
        {
            Console.WriteLine("Score: " + score);
            Console.WriteLine("High Score: " + hs);
        }
        internal void check_hs() {
            if (hs < score)
            {
                hs = score;
                high_score = hs.ToString();
                File.WriteAllTextAsync(file, high_score);
            }
            else
                return;
        }

    }
}
