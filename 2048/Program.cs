using System;

namespace _2048
{
    using Classes;
    class Program
    {
        static void Main(string[] args)
        {
            Game_Board game_board = new Game_Board();
            game_board.insert_first_values();
        }
    }
}
