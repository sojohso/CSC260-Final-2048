using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _2048_Final.Classes.Game
{
    using Board;
    using Score;
    using Color;
    using Color_Output;
    using Message; //could try to inherit functions
    class Game
    {
        private readonly int columns;
        private readonly int rows;
        Board board = new Board();
        Board empty_spaces = new Board();
        Score score = new Score();
        Color color = new Color();
        Message msg = new Message();
        int locked;

        public Game()
        {

            this.columns = 4;
            this.rows = 4;
            this.locked = 0;
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
            //Console.Clear();
            //will add ^ after debugging
            //Board_Colors board_colors = new Board_Colors();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    using (new Color_Output(color.get_color(board.get_board_values(i, j))))
                    {
                        Console.Write(string.Format("{0,4}", board.get_board_values(i, j))); //found from github.com
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
            } 
            score.print_score();
            return;
            //insert functionality to add colors from board colors class
        }
        internal int check_full()
        {

            int i = 0, j;
            int tmp1, tmp2, tmp3, tmp4;
            int full = 0;

            for (j = 0; j <= 3; j++)
            {
                tmp1 = board.get_board_values(i, j); //uppermost
                tmp2 = board.get_board_values(i + 1, j); //2nd uppermost
                tmp3 = board.get_board_values(i + 2, j); //3rd uppermost
                tmp4 = board.get_board_values(i + 3, j); //4th uppermost
                if (tmp1 != 0 && tmp2 != 0 && tmp3 != 0 && tmp4 != 0)
                    full++;
            }
            return full;
        }
        internal void insert_random_value()
        {
            Random random_number = new Random();
            int iVal, iRow, iCol, full;
            iVal = random_number.Next(0, 100) < 75 ? 2 : 4;
            iRow = random_number.Next(0, 3);
            iCol = random_number.Next(0, 3);
            full = check_full();
            if (full == 4)
                return;
            while (board.get_board_values(iRow, iCol) != 0) //checks will be added to make sure it does not overflow
            {
                iRow = random_number.Next(0, 4);
                iCol = random_number.Next(0, 4);
            }

            board.set_board_values(iRow, iCol, iVal);
            //add checks to make sure it does not do the same index twice
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
                    move_blocks_left();
                    Console.WriteLine("left");
                    break;
                case ConsoleKey.RightArrow:
                    move_blocks_right();
                    Console.WriteLine("right");
                    break;
                case ConsoleKey.UpArrow:
                    move_blocks_up();
                    Console.WriteLine("up");
                    break;
                case ConsoleKey.DownArrow:
                    move_blocks_down();
                    Console.WriteLine("down");
                    break;
                default:
                    break;
            }
            insert_random_value();
            print_board();
            //msg.lost_check(board, score.get_score());
            keyboard_press();
            //pass which one by <, >, ^, v into the separate class
            //return this value, then main will insert that value into the class.
            //will insert according to that
        }

        internal void move_blocks_up()
        {

            int i = 0, j;
            int tmp1, tmp2, tmp3, tmp4;

            for (j = 0; j <= 3; j++)
            {
                tmp1 = board.get_board_values(i, j); //uppermost
                tmp2 = board.get_board_values(i + 1, j); //2nd uppermost
                tmp3 = board.get_board_values(i + 2, j); //3rd uppermost
                tmp4 = board.get_board_values(i + 3, j); //4th uppermost
                                                         //else if tmp 1 = 0 don't alter anything
                if (tmp1 == tmp2 && tmp1 != 0 && tmp2 != 0)
                {
                    Console.WriteLine("hey");
                    tmp1 = tmp1 * 2;
                    board.set_board_values(0, j, tmp1);
                    if (tmp3 == tmp4 && tmp3 != 0 && tmp4 != 0)
                    {
                        tmp3 = tmp3 * 2;
                        board.set_board_values(1, j, tmp3);
                        board.set_board_values(2, j, 0);
                        board.set_board_values(3, j, 0);
                        score.calculate_score(tmp3);
                    }
                    else
                    {
                        board.set_board_values(1, j, tmp3);
                        board.set_board_values(2, j, tmp4);
                        board.set_board_values(3, j, 0);

                        score.calculate_score(tmp1);
                    }

                }
                else if (tmp1 != 0 && tmp2 == 0 && tmp3 == 0 && tmp1 != tmp4 && tmp4 != 0)
                {
                    board.set_board_values(1, j, tmp4);
                    board.set_board_values(3, j, 0);
                }
                else if (tmp1 == tmp3 && tmp2 == 0 && tmp1 != 0 && tmp3 != 0)
                {
                    tmp1 = tmp1 * 2;
                    board.set_board_values(0, j, tmp1);
                    board.set_board_values(1, j, tmp4);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(3, j, 0);
                    score.calculate_score(tmp1);
                }
                else if (tmp2 == tmp3 && tmp1 == 0 && tmp2 != 0 && tmp3 != 0)
                {
                    Console.WriteLine("hey1");
                    tmp2 = tmp2 * 2;
                    board.set_board_values(0, j, tmp2);
                    board.set_board_values(1, j, tmp4);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(3, j, 0);
                    score.calculate_score(tmp2);
                }
                else if (tmp2 == tmp4 && tmp1 == 0 && tmp3 == 0 && tmp2 != 0 && tmp4 != 0)
                {
                    Console.WriteLine("hey2");
                    tmp2 = tmp2 * 2;
                    board.set_board_values(0, j, tmp2);
                    board.set_board_values(1, j, 0);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(3, j, 0);
                    score.calculate_score(tmp2);
                }
                else if (tmp2 == tmp4 && tmp1 != 0 && tmp3 == 0 && tmp2 != 0)
                {
                    Console.WriteLine("hey3");
                    tmp2 = tmp2 * 2;
                    board.set_board_values(1, j, tmp2);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(3, j, 0);
                    score.calculate_score(tmp2);
                } else if(tmp3 == tmp4 && tmp3 != 0 && tmp2 == 0 && tmp1 != 0 && tmp1 != tmp3)
                {
                    Console.WriteLine("hey4");
                    tmp3 *= 2;
                    board.set_board_values(1, j, tmp3);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(3, j, 0);
                }
                else if (tmp3 == tmp4 && tmp1 == 0 && tmp2 == 0 && tmp3 != 0)
                {
                    Console.WriteLine("hey5");
                    tmp3 = tmp3 * 2;
                    board.set_board_values(0, j, tmp3);
                    board.set_board_values(1, j, 0);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(3, j, 0);
                    score.calculate_score(tmp3);
                } else if (tmp3 == tmp4 && tmp3 != 0 && tmp2 == 0 && tmp3 != tmp1 && tmp1 != 0)
                {
                    Console.WriteLine("hey6");
                    tmp3 *= 2;
                    board.set_board_values(1, j, tmp3);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(3, j, 0);
                    score.calculate_score(tmp3);
                }
                else if (tmp1 == 0 && tmp2 == 0 && tmp3 == 0 && tmp4 != 0)
                {
                    Console.WriteLine("hey7");
                    board.set_board_values(0, j, tmp4);
                    board.set_board_values(1, j, 0);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(3, j, 0);
                }
                else if (tmp1 != tmp2 && tmp2 != 0 && tmp2 == tmp3 && tmp1 != 0 && tmp3 != 0)
                {
                    Console.WriteLine("hey8");
                    tmp2 = tmp2 * 2;
                    board.set_board_values(1, j, tmp2);
                    board.set_board_values(2, j, tmp4);
                    board.set_board_values(3, j, 0);
                    score.calculate_score(tmp2);
                }
                else if (tmp1 != tmp3 && tmp2 == 0 && tmp1 != 0 && tmp3 != tmp4)
                {
                    Console.WriteLine("hey9");
                    board.set_board_values(1, j, tmp3);
                    board.set_board_values(2, j, 0); //add function for block to appear
                    board.set_board_values(3, j, 0); //add function for block to appear
                }
                else if (tmp1 != tmp4 && tmp2 == 0 && tmp3 == 0 && tmp1 != 0 && tmp4 != 0)
                {
                    Console.WriteLine("hey10");
                    board.set_board_values(1, j, tmp4);
                    board.set_board_values(2, j, 0); //add function for block to appear
                    board.set_board_values(3, j, 0); //add function for block to appear
                }
                else if(tmp1 == 0 && tmp2 != 0 && tmp3 != 0 && tmp4 != 0 && tmp2 != tmp3 && tmp2 != tmp4 && tmp3 != tmp4)
                {
                    Console.WriteLine("hey11");
                    board.set_board_values(0, j, tmp2);
                    board.set_board_values(1, j, tmp3);
                    board.set_board_values(2, j, tmp4);
                    board.set_board_values(3, j, 0);
                }
                else if (tmp1 == 0 && tmp2 != 0)
                {
                    Console.WriteLine("hey12");
                    board.set_board_values(0, j, tmp2);
                    board.set_board_values(1, j, tmp3);
                    board.set_board_values(2, j, tmp4);
                    board.set_board_values(3, j, 0); //add function for block to appear
                }
                else if (tmp1 == 0 && tmp2 == 0 && tmp3 != 0)
                {
                    Console.WriteLine("hey13");
                    board.set_board_values(0, j, tmp3);
                    board.set_board_values(1, j, tmp4);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(3, j, 0); //add function for block to appear
                }
                else if (tmp2 == 0 && tmp2 == tmp3 && tmp2 == tmp1 && tmp4 != 0)
                {
                    Console.WriteLine("hey14");
                    board.set_board_values(0, j, tmp4);
                    board.set_board_values(3, j, 0); //add function for block to appear
                }
                else if(tmp3 == tmp4 && tmp3 != tmp2 && tmp1 != tmp2 && tmp2 != 0 && tmp3 != 0)
                {
                    Console.WriteLine("hey15");
                    tmp3 = tmp3 * 2;
                    board.set_board_values(2, j, tmp3);
                    board.set_board_values(3, j, 0); //add function for block to appear
                    score.calculate_score(tmp3);

                } else if(tmp1 != 0 && tmp2 != 0 && tmp3 == 0 && tmp4 != 0 && tmp1 != tmp2 && tmp2 != tmp4)
                {
                    Console.WriteLine("hey16");
                    board.set_board_values(2, j, tmp4);
                    board.set_board_values(3, j, 0);
                }
                else
                {
                    Console.WriteLine("hey17");
                }
                //else, then the player cannot move the grid this way
            }
            return;
        }
        internal void move_blocks_down()
        {
            int i = 0, j = 0;
            int tmp1, tmp2, tmp3, tmp4;
            for (j = 0; j <= 3; j++)
            {
                tmp4 = board.get_board_values(i, j); //uppermost
                tmp3 = board.get_board_values(i + 1, j); //2nd uppermost
                tmp2 = board.get_board_values(i + 2, j); //2nd bottommost
                tmp1 = board.get_board_values(i + 3, j); //bottomsmost
                                                         //else if tmp 1 = 0 don't alter anything
                if (tmp1 == tmp2 && tmp1 != 0 && tmp2 != 0)
                {
                    tmp1 = tmp1 * 2;
                    board.set_board_values(3, j, tmp1);
                    if (tmp3 == tmp4 && tmp3 != 0 && tmp4 != 0)
                    {
                        tmp3 = tmp3 * 2;
                        board.set_board_values(2, j, tmp3);
                        board.set_board_values(1, j, 0);  //add function for block to appear
                        board.set_board_values(0, j, 0);  //add function for block to appear
                        score.calculate_score(tmp3);
                    }
                    else
                    {
                        board.set_board_values(2, j, tmp3);
                        board.set_board_values(1, j, tmp4);
                        board.set_board_values(0, j, 0); //add chance for random square, maybe in another function
                        score.calculate_score(tmp1);
                    }

                }
                else if (tmp1 == tmp3 && tmp2 == 0 && tmp1 != 0 && tmp3 != 0)
                {
                    tmp1 = tmp1 * 2;
                    board.set_board_values(3, j, tmp1);
                    board.set_board_values(2, j, tmp4);
                    board.set_board_values(1, j, 0); //add function for block to appear
                    board.set_board_values(0, j, 0); //add function for block to appear
                    score.calculate_score(tmp1);
                }
                else if (tmp1 == tmp4 && tmp2 == 0 && tmp3 == 0 && tmp1 != 0)//added
                {
                    tmp1 = tmp1 * 2;
                    board.set_board_values(3, j, tmp1);
                    board.set_board_values(2, j, 0);//add function for block to appear
                    board.set_board_values(1, j, 0); //add function for block to appear
                    board.set_board_values(0, j, 0); //add function for block to appear
                    score.calculate_score(tmp1);
                }
                else if (tmp2 == tmp3 && tmp1 == 0 && tmp2 != 0 && tmp3 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(3, j, tmp2);
                    board.set_board_values(2, j, tmp4);
                    board.set_board_values(1, j, 0); //add function for block to appear
                    board.set_board_values(1, j, 0); //add function for block to appear
                    score.calculate_score(tmp2);
                }
                else if (tmp2 == tmp4 && tmp1 == 0 && tmp3 == 0 && tmp2 != 0 && tmp4 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(3, j, tmp2);
                    board.set_board_values(2, j, 0); //add function for block to appear
                    board.set_board_values(1, j, 0); //add function for block to appear
                    board.set_board_values(0, j, 0); //add function for block to appear
                    score.calculate_score(tmp2);
                }
                else if (tmp2 == tmp4 && tmp1 != 0 && tmp3 == 0 && tmp2 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(2, j, tmp2);
                    board.set_board_values(1, j, 0); //add function for block to appear
                    board.set_board_values(0, j, 0); //add function for block to appear
                    score.calculate_score(tmp2);
                } 
                else if(tmp3 == tmp4 && tmp3 != 0 && tmp2 != 0 && tmp2 != tmp3 && tmp1 == 0)
                {
                    tmp3 *= 2;
                    board.set_board_values(3, j, tmp2);
                    board.set_board_values(2, j, tmp3);
                    board.set_board_values(1, j, 0);
                    board.set_board_values(0, j, 0);
                    score.calculate_score(tmp3);
                }
                else if (tmp3 == tmp4 && tmp1 == 0 && tmp2 == 0 && tmp3 != 0)
                {
                    tmp3 = tmp3 * 2;
                    board.set_board_values(3, j, tmp3);
                    board.set_board_values(2, j, 0); //add function for block to appear
                    board.set_board_values(1, j, 0); //add function for block to appear
                    board.set_board_values(0, j, 0); //add function for block to appear
                    score.calculate_score(tmp3);
                }
                else if (tmp3 == tmp4 && tmp3 != tmp2 && tmp1 != tmp2)
                {
                    tmp3 = tmp3 * 2;
                    board.set_board_values(1, j, tmp3);
                    board.set_board_values(0, j, 0); //add function for block to appear
                    score.calculate_score(tmp3);

                }
                else if (tmp2 != 0 && tmp1 == 0 && tmp3 == 0 && tmp4 == 0)
                {
                    Console.Write("HEYYYYY");
                    board.set_board_values(3, j, tmp2);
                    board.set_board_values(2, j, 0);
                }
                else if (tmp1 == 0 && tmp2 == 0 && tmp3 == 0 && tmp4 != 0)
                {
                    board.set_board_values(3, j, tmp4);
                    board.set_board_values(0, j, 0); //add function for block to appear                      board.set_board_values(3, j, 0); //add function for block to appear
                }
                else if (tmp1 != tmp3 && tmp2 == 0 && tmp1 != 0 && tmp3 != tmp4)
                {
                    board.set_board_values(2, j, tmp3);
                    board.set_board_values(1, j, tmp4); //add function for block to appear
                    board.set_board_values(0, j, 0); //add function for block to appear
                }
                else if (tmp1 != tmp4 && tmp2 == 0 && tmp1 != 0 && tmp3 == 0 && tmp4 != 0)
                {
                    board.set_board_values(3, j, tmp4);
                    board.set_board_values(2, j, 0); //add function for block to appear
                    board.set_board_values(1, j, 0); //add function for block to appear
                }
                else if (tmp2 == tmp3 && tmp1 != tmp2 && tmp1 != 0 && tmp2 != 0 && tmp3 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(2, j, tmp2);
                    board.set_board_values(1, j, 0); //add function for block to appear
                    score.calculate_score(tmp2);
                }
                else if (tmp1 == 0 && tmp2 != 0 && tmp3 == 0 && tmp4 != 0 && tmp2 != tmp4)
                {
                    board.set_board_values(3, j, tmp2);
                    board.set_board_values(2, j, tmp4);
                    board.set_board_values(0, j, 0);
                }
                else if (tmp1 == 0 && tmp2 != 0)
                {
                    board.set_board_values(3, j, tmp2);
                    board.set_board_values(2, j, tmp3);
                    board.set_board_values(1, j, tmp4);
                    board.set_board_values(0, j, 0); //add function for block to appear
                }
                else if(tmp4 != 0 && tmp1 == 0 && tmp2 == 0 && tmp3 == 0)
                {
                    board.set_board_values(3, j, tmp4);
                    board.set_board_values(0, j, 0);
                }
                else if(tmp1 != 0 && tmp3 != 0 && tmp1 != tmp3 && tmp2 == 0 && tmp4 == 0)
                {
                    board.set_board_values(2, j, tmp3);
                    board.set_board_values(1, j, 0);
                }
                else if (tmp1 == 0 && tmp2 == 0 && tmp3 != 0)
                {
                    board.set_board_values(3, j, tmp3);
                    board.set_board_values(2, j, tmp4);
                    board.set_board_values(1, j, 0);
                    board.set_board_values(0, j, 0); //add function for block to appear
                }
                else if(tmp1 != 0 && tmp2 == 0 && tmp3 == 0 && tmp4 == 0)
                {
                    break;
                }
                else if (tmp1 != tmp3 && tmp2 == 0)
                {
                    board.set_board_values(1, j, tmp3);
                    board.set_board_values(2, j, tmp4);
                    board.set_board_values(3, j, 0); //add function for block to appear
                }

            }

            }
        internal void move_blocks_left()
        {
            int i, j = 0;
            int tmp1, tmp2, tmp3, tmp4;

            for (i = 0; i <= 3; i++)
            {
                tmp1 = board.get_board_values(i, j); //leftmost
                tmp2 = board.get_board_values(i, j + 1); //2nd left most
                tmp3 = board.get_board_values(i, j + 2); //3rd left most
                tmp4 = board.get_board_values(i, j + 3); //4th left most
                                                         //else if tmp 1 = 0 don't alter anything
                if (tmp1 == tmp2 && tmp1 != 0 && tmp2 != 0)
                {
                    tmp1 = tmp1 * 2;
                    board.set_board_values(i, 0, tmp1);
                    if (tmp3 == tmp4 && tmp3 != 0 && tmp4 != 0)
                    {
                        tmp3 = tmp3 * 2;
                        board.set_board_values(i, 1, tmp3);
                        board.set_board_values(i, 2, 0);  //add function for block to appear
                        board.set_board_values(i, 3, 0);  //add function for block to appear
                        score.calculate_score(tmp3);
                    }
                    else
                    {
                        board.set_board_values(i, 1, tmp3);
                        board.set_board_values(i, 2, tmp4);
                        board.set_board_values(i, 3, 0); //add chance for random square, maybe in another function
                        score.calculate_score(tmp3);
                    }

                }
                else if (tmp1 == tmp3 && tmp2 == 0 && tmp1 != 0 && tmp3 != 0)
                {
                    tmp1 = tmp1 * 2;
                    board.set_board_values(i, 0, tmp1);
                    board.set_board_values(i, 1, tmp4);
                    board.set_board_values(i, 2, 0); //add function for block to appear
                    board.set_board_values(i, 3, 0); //add function for block to appear
                    score.calculate_score(tmp1);
                }
                else if(tmp1 == tmp4 && tmp1 != 0 && tmp2 == 0 && tmp3 == 0)
                {
                    tmp1 *= 2;
                    board.set_board_values(i, 0, tmp1);
                    board.set_board_values(i, 3, 0);
                    score.calculate_score(tmp1);
                }
                else if (tmp3 == tmp4 && tmp3 != tmp2 && tmp1 != tmp2 && tmp1 != 0 && tmp2 != 0 && tmp3 != 0)
                {
                    tmp3 = tmp3 * 2;
                    board.set_board_values(i, 2, tmp3);
                    board.set_board_values(i, 3, 0); //add function for block to appear
                    score.calculate_score(tmp3);

                }
                else if (tmp2 == tmp3 && tmp1 == 0 && tmp2 != 0 && tmp3 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(i, 0, tmp2);
                    board.set_board_values(i, 1, tmp4);
                    board.set_board_values(i, 2, 0); //add function for block to appear
                    board.set_board_values(i, 3, 0); //add function for block to appear
                    score.calculate_score(tmp2);
                }
                else if (tmp2 == tmp4 && tmp1 == 0 && tmp3 == 0 && tmp2 != 0 && tmp4 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(i, 0, tmp2);
                    board.set_board_values(i, 1, 0); //add function for block to appear
                    board.set_board_values(i, 2, 0); //add function for block to appear
                    board.set_board_values(i, 3, 0); //add function for block to appear
                    score.calculate_score(tmp2);
                }
                else if (tmp2 == tmp4 && tmp1 != 0 && tmp3 == 0 && tmp2 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(i, 1, tmp2);
                    board.set_board_values(i, 2, 0); //add function for block to appear
                    board.set_board_values(i, 3, 0); //add function for block to appear
                    score.calculate_score(tmp2);
                } else if(tmp3 == tmp4 && tmp3 != 0 && tmp1 == 0 && tmp2 != 0 && tmp2 != tmp3)
                {
                    tmp3 *= 2;
                    board.set_board_values(i, 0, tmp2);
                    board.set_board_values(i, 1, tmp3);
                    board.set_board_values(i, 2, 0);
                    board.set_board_values(i, 3, 0);
                    score.calculate_score(tmp3);
                }
                else if (tmp3 == tmp4 && tmp1 == 0 && tmp2 == 0)
                {
                    tmp3 = tmp3 * 2;
                    board.set_board_values(i, 0, tmp3);
                    board.set_board_values(i, 1, 0); //add function for block to appear
                    board.set_board_values(i, 2, 0); //add function for block to appear
                    board.set_board_values(i, 3, 0); //add function for block to appear
                    score.calculate_score(tmp3);
                }
                else if (tmp1 == 0 && tmp2 == 0 && tmp3 == 0 && tmp4 != 0)
                {
                    board.set_board_values(i, 0, tmp4);
                    board.set_board_values(i, 1, 0); //add function for block to appear
                    board.set_board_values(i, 2, 0); //add function for block to appear                      board.set_board_values(3, j, 0); //add function for block to appear
                    board.set_board_values(i, 3, 0); //add function for block to appear                      board.set_board_values(3, j, 0); //add function for block to appear
                }
                else if (tmp1 != tmp2 && tmp2 != 0 && tmp2 == tmp3 && tmp1 != 0 && tmp3 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(i, 1, tmp2);
                    board.set_board_values(i, 2, tmp4);
                    board.set_board_values(i, 3, 0); //add function for block to appear
                    score.calculate_score(tmp2);
                }
                else if(tmp1 != 0 && tmp4 != 0 && tmp2 == 0 && tmp3 == 0 && tmp1 != tmp4){
                    board.set_board_values(i, 1, tmp4);
                    board.set_board_values(i, 3, 0);
                }
                else if (tmp1 != tmp3 && tmp2 == 0 && tmp1 != 0 && tmp3 != tmp4)
                {
                    if (tmp4 != 0)
                    {
                        board.set_board_values(i, 1, tmp3);
                        board.set_board_values(i, 2, tmp4);
                    }
                    else
                    {
                        board.set_board_values(i, 1, tmp3);
                        board.set_board_values(i, 2, 0); //add function for block to appear
                    }
                    board.set_board_values(i, 3, 0); //add function for block to appear
                }
                else if (tmp1 != tmp4 && tmp2 == 0 && tmp3 == 0 && tmp1 != 0 && tmp4 != 0)
                {
                    board.set_board_values(i, 1, tmp4);
                    board.set_board_values(i, 2, 0); //add function for block to appear
                    board.set_board_values(i, 3, 0); //add function for block to appear
                } else if(tmp1 == 0 && tmp2 != 0 && tmp3 == 0 && tmp4 != 0 && tmp2 != tmp4)
                {
                    board.set_board_values(i, 0, tmp2);
                    board.set_board_values(i, 1, tmp4);
                    board.set_board_values(i, 2, 0);
                    board.set_board_values(i, 3, 0);
                }
                else if (tmp1 == 0 && tmp2 != 0)
                {
                    board.set_board_values(i, 0, tmp2);
                    board.set_board_values(i, 1, tmp3);
                    board.set_board_values(i, 2, tmp4);
                    board.set_board_values(i, 3, 0); //add function for block to appear
                }
                else if (tmp1 == 0 && tmp2 == 0 && tmp3 != 0)
                {
                    board.set_board_values(i, 0, tmp3);
                    board.set_board_values(i, 1, tmp4);
                    board.set_board_values(i, 2, 0);
                    board.set_board_values(i, 3, 0); //add function for block to appear
                }
                else if (tmp2 == 0 && tmp2 == tmp3 && tmp2 == tmp1 && tmp4 != 0)
                {
                    board.set_board_values(i, 0, tmp4);
                    board.set_board_values(i, 3, 0); //add function for block to appear
                }
                else if (tmp1 != tmp3 && tmp2 == 0)
                {
                    board.set_board_values(i, 1, tmp3);
                    board.set_board_values(i, 2, tmp4);
                    board.set_board_values(i, 3, 0); //add function for block to appear
                }
                else
                {
                    locked++;
                }
            }
            return;
        }
        internal void move_blocks_right()
        {
            int i, j = 0;
            int tmp1, tmp2, tmp3, tmp4;
            for (i = 0; i <= 3; i++)
            {
                tmp4 = board.get_board_values(i, j); //leftmost
                tmp3 = board.get_board_values(i, j + 1);
                tmp2 = board.get_board_values(i, j + 2);
                tmp1 = board.get_board_values(i, j + 3);
                                                         //else if tmp 1 = 0 don't alter anything
                if (tmp1 == tmp2 && tmp1 != 0 && tmp2 != 0)
                {
                    tmp1 = tmp1 * 2;
                    board.set_board_values(i, 3, tmp1);
                    if (tmp3 == tmp4 && tmp3 != 0 && tmp4 != 0)
                    {
                        tmp3 = tmp3 * 2;
                        board.set_board_values(i, 2, tmp3);
                        board.set_board_values(i, 1, 0);  //add function for block to appear
                        board.set_board_values(i, 0, 0);  //add function for block to appear
                        score.calculate_score(tmp3);
                    }
                    else if(tmp3 == 0 && tmp4 != 0)
                    {
                        board.set_board_values(i, 2, tmp4);
                        board.set_board_values(i, 1, 0);
                        board.set_board_values(i, 0, 0);
                    }
                    else
                    {
                        board.set_board_values(i, 2, tmp3);
                        board.set_board_values(i, 1, tmp4);
                        board.set_board_values(i, 0, 0); //add chance for random square, maybe in another function
                        score.calculate_score(tmp1);
                    }

                }
                else if (tmp1 == tmp3 && tmp2 == 0 && tmp1 != 0 && tmp3 != 0)
                {
                    tmp1 = tmp1 * 2;
                    board.set_board_values(i, 3, tmp1);
                    board.set_board_values(i, 2, tmp4);
                    board.set_board_values(i, 1, 0); //add function for block to appear
                    board.set_board_values(i, 0, 0); //add function for block to appear
                    score.calculate_score(tmp1);
                }
                else if(tmp1 == tmp4 && tmp2 == 0 && tmp3 == 0 && tmp1 != 0)
                {
                    tmp4 = tmp4 * 2;
                    board.set_board_values(i, 3, tmp4);
                    board.set_board_values(i, 0, 0);
                    score.calculate_score(tmp4);
                } else if (tmp3 == tmp4 && tmp3 != tmp2 && tmp3 != tmp1 && tmp2 != 0 && tmp1 == 0)
                {
                    tmp3 *= 2;
                    board.set_board_values(i, 3, tmp2);
                    board.set_board_values(i, 2, tmp3);
                    board.set_board_values(i, 1, 0);
                    board.set_board_values(i, 0, 0);
                    score.calculate_score(tmp3);
                }
                else if (tmp3 == tmp4 && tmp3 != tmp2 && tmp1 != tmp2 && tmp1 != 0 && tmp2 != 0 && tmp3 != 0)
                {
                    tmp3 = tmp3 * 2;
                    board.set_board_values(i, 1, tmp3);
                    board.set_board_values(i, 0, 0); //add function for block to appear
                    score.calculate_score(tmp3);

                }
                else if (tmp2 == tmp3 && tmp1 == 0 && tmp2 != 0 && tmp3 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(i, 3, tmp2);
                    board.set_board_values(i, 2, tmp4);
                    board.set_board_values(i, 1, 0); //add function for block to appear
                    board.set_board_values(i, 0, 0); //add function for block to appear
                    score.calculate_score(tmp2);
                }
                else if (tmp2 == tmp4 && tmp1 == 0 && tmp3 == 0 && tmp2 != 0 && tmp4 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(i, 3, tmp2);
                    board.set_board_values(i, 2, 0); //add function for block to appear
                    board.set_board_values(i, 1, 0); //add function for block to appear
                    board.set_board_values(i, 0, 0); //add function for block to appear
                    score.calculate_score(tmp2);
                }
                else if (tmp2 == tmp4 && tmp1 != 0 && tmp3 == 0 && tmp2 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(i, 2, tmp2);
                    board.set_board_values(i, 1, 0); //add function for block to appear
                    board.set_board_values(i, 0, 0); //add function for block to appear
                    score.calculate_score(tmp2);
                }
                else if (tmp3 == tmp4 && tmp1 == 0 && tmp2 == 0)
                {
                    tmp3 = tmp3 * 2;
                    board.set_board_values(i, 3, tmp3);
                    board.set_board_values(i, 2, 0); //add function for block to appear
                    board.set_board_values(i, 1, 0); //add function for block to appear
                    board.set_board_values(i, 0, 0); //add function for block to appear
                    score.calculate_score(tmp3);
                }
                else if (tmp3 == tmp4 && tmp2 == 0 && tmp1 != 0 && tmp3 != 0 && tmp1 != tmp3)
                {
                    tmp3 = tmp3 * 2;
                    board.set_board_values(i, 2, tmp3);
                    board.set_board_values(i, 1, 0); //add function for block to appear
                    board.set_board_values(i, 0, 0); //add function for block to appear
                    score.calculate_score(tmp3);
                }
                else if (tmp1 == 0 && tmp2 == 0 && tmp3 == 0 && tmp4 != 0)
                {
                    board.set_board_values(i, 3, tmp4);
                    board.set_board_values(i, 2, 0); //add function for block to appear
                    board.set_board_values(i, 1, 0); //add function for block to appear                      board.set_board_values(3, j, 0); //add function for block to appear
                    board.set_board_values(i, 0, 0); //add function for block to appear                      board.set_board_values(3, j, 0); //add function for block to appear
                }
                else if (tmp2 == tmp3 && tmp1 != tmp2 && tmp2 != 0 && tmp1 != 0 && tmp3 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(i, 2, tmp2);
                    board.set_board_values(i, 1, tmp4);
                    board.set_board_values(i, 0, 0); //add function for block to appear
                    score.calculate_score(tmp2);
                }
                else if(tmp2 == 0 && tmp3 == 0 && tmp1 != 0 && tmp4 != 0 && tmp1 != tmp4)
                {
                    board.set_board_values(i, 2, tmp4);
                    board.set_board_values(i, 0, 0);
                }
                else if (tmp1 != tmp3 && tmp2 == 0 && tmp1 != 0 && tmp3 != tmp4)
                {
                    board.set_board_values(i, 2, tmp3);
                    board.set_board_values(i, 1, 0); //add function for block to appear
                    board.set_board_values(i, 0, 0); //add function for block to appear
                } else if(tmp1 != 0 && tmp2 != 0 && tmp3 == 0 && tmp4 != 0 && tmp1 != tmp2 && tmp1 != tmp4 && tmp2 != tmp4)
                {
                    board.set_board_values(i, 1, tmp4);
                    board.set_board_values(i, 0, 0);
                }
                else if (tmp1 != tmp4 && tmp2 == 0 && tmp3 == 0 && tmp1 != 0 && tmp4 != 0)
                {
                    board.set_board_values(i, 2, tmp4);
                    board.set_board_values(i, 1, 0); //add function for block to appear
                    board.set_board_values(i, 0, 0); //add function for block to appear
                }
                else if (tmp1 == 0 && tmp1 == tmp3 && tmp2 != tmp4 && tmp2 != 0 && tmp4 != 0)
                {
                    board.set_board_values(i, 3, tmp2);
                    board.set_board_values(i, 2, tmp4);
                    board.set_board_values(i, 0, 0);
                }
                else if (tmp1 == 0 && tmp2 != 0)
                {
                    board.set_board_values(i, 3, tmp2);
                    board.set_board_values(i, 2, tmp3);
                    board.set_board_values(i, 1, tmp4);
                    board.set_board_values(i, 0, 0); //add function for block to appear
                }
                else if (tmp1 == 0 && tmp2 == 0 && tmp3 != 0)
                {
                    board.set_board_values(i, 3, tmp3);
                    board.set_board_values(i, 2, tmp4);
                    board.set_board_values(i, 1, 0);
                    board.set_board_values(i, 0, 0); //add function for block to appear
                }
                else if (tmp2 == 0 && tmp2 == tmp3 && tmp2 == tmp1 && tmp4 != 0)
                {
                    board.set_board_values(i, 3, tmp4);
                    board.set_board_values(i, 0, 0); //add function for block to appear
                }
                else if (tmp1 != tmp3 && tmp2 == 0)
                {
                    board.set_board_values(i, 2, tmp3);
                    board.set_board_values(i, 1, tmp4);
                    board.set_board_values(i, 0, 0); //add function for block to appear
                } else if (tmp4 != 0 && tmp3 != 0 && tmp2 != 0 && tmp1 == 0 && tmp4 != tmp3 && tmp3 != tmp2)
                {
                    board.set_board_values(i, 3, tmp2);
                    board.set_board_values(i, 2, tmp3);
                    board.set_board_values(i, 1, tmp4);
                }
            }
            return;
        }
    }
    }

