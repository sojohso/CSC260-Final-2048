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
    using Message; 
    class Game
    {
        private readonly int columns;
        private readonly int rows;
        Board board = new Board();
        Score score = new Score();
        Color color = new Color();
        Message msg = new Message();

        public Game()
        {

            this.columns = 4;
            this.rows = 4;
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
                x++;
            }
            print_board();
            keyboard_press();
        }

        private void print_board()
        {
            //Console.Clear();
            //will add ^ after debugging
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    using (new Color_Output(color.get_color(board.get_board_values(i, j))))
                    {
                        Console.Write(string.Format("{0,4}", board.get_board_values(i, j)));
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
            } 
            score.print_score();
            return;
        }
        private int check_full()
        {

            int i = 0, j;
            int tmp1, tmp2, tmp3, tmp4;
            int full = 0;

            for (j = 0; j <= 3; j++)
            {
                tmp1 = board.get_board_values(i, j); 
                tmp2 = board.get_board_values(i + 1, j); 
                tmp3 = board.get_board_values(i + 2, j); 
                tmp4 = board.get_board_values(i + 3, j); 
                if (tmp1 != 0 && tmp2 != 0 && tmp3 != 0 && tmp4 != 0)
                    full++;
            }
            return full;
        }
        private void insert_random_value()
        {
            Random random_number = new Random();
            int iVal, iRow, iCol, full;
            iVal = random_number.Next(0, 100) < 75 ? 2 : 4;
            iRow = random_number.Next(0, 3);
            iCol = random_number.Next(0, 3);
            full = check_full();
            if (full == 4)
                return;
            while (board.get_board_values(iRow, iCol) != 0)
            {
                iRow = random_number.Next(0, 4);
                iCol = random_number.Next(0, 4);
            }

            board.set_board_values(iRow, iCol, iVal);
        }

        private void keyboard_press()
        {
            int i, j;
            Console.WriteLine();
            Console.WriteLine("Use arrow keys to move the game pieces, ctrl+ c to exit");
            var input = Console.ReadKey();
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
            msg.lost_check(board, score.get_score());
            keyboard_press();
        }

        private void move_blocks_up()
        {

            int i = 0, j;
            int tmp1, tmp2, tmp3, tmp4;

            for (j = 0; j <= 3; j++)
            {
                tmp1 = board.get_board_values(i, j); 
                tmp2 = board.get_board_values(i + 1, j); 
                tmp3 = board.get_board_values(i + 2, j); 
                tmp4 = board.get_board_values(i + 3, j); 
                                                         
                if (tmp1 == tmp2 && tmp1 != 0 && tmp2 != 0)
                {
                    tmp1 = tmp1 * 2;
                    board.set_board_values(0, j, tmp1);

                    board.set_board_values(1, j, 0);
                    tmp2 = board.get_board_values(1, j);
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
                    }

                    score.calculate_score(tmp1);

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
                    tmp2 = tmp2 * 2;
                    board.set_board_values(0, j, tmp2);
                    board.set_board_values(1, j, tmp4);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(3, j, 0);
                    score.calculate_score(tmp2);
                }
                else if (tmp2 == tmp4 && tmp1 == 0 && tmp3 == 0 && tmp2 != 0 && tmp4 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(0, j, tmp2);
                    board.set_board_values(1, j, 0);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(3, j, 0);
                    score.calculate_score(tmp2);
                }
                else if (tmp2 == tmp4 && tmp1 != 0 && tmp3 == 0 && tmp2 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(1, j, tmp2);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(3, j, 0);
                    score.calculate_score(tmp2);
                } else if(tmp3 == tmp4 && tmp3 != 0 && tmp2 == 0 && tmp1 != 0 && tmp1 != tmp3)
                {
                    tmp3 *= 2;
                    board.set_board_values(1, j, tmp3);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(3, j, 0);
                }
                else if (tmp3 == tmp4 && tmp1 == 0 && tmp2 == 0 && tmp3 != 0)
                {
                    tmp3 = tmp3 * 2;
                    board.set_board_values(0, j, tmp3);
                    board.set_board_values(1, j, 0);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(3, j, 0);
                    score.calculate_score(tmp3);
                } else if (tmp3 == tmp4 && tmp3 != 0 && tmp2 == 0 && tmp3 != tmp1 && tmp1 != 0)
                {
                    tmp3 *= 2;
                    board.set_board_values(1, j, tmp3);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(3, j, 0);
                    score.calculate_score(tmp3);
                } else if(tmp1 == 0 && tmp2 != 0 && tmp3 == 0 && tmp4 != 0 && tmp2 != tmp4)
                {
                    board.set_board_values(0, j, tmp2);
                    board.set_board_values(1, j, tmp3);
                    board.set_board_values(3, j, 0);
                }
                else if (tmp1 == 0 && tmp2 == 0 && tmp3 == 0 && tmp4 != 0)
                {
                    board.set_board_values(0, j, tmp4);
                    board.set_board_values(1, j, 0);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(3, j, 0);
                }
                else if (tmp1 != tmp2 && tmp2 != 0 && tmp2 == tmp3 && tmp1 != 0 && tmp3 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(1, j, tmp2);
                    board.set_board_values(2, j, tmp4);
                    board.set_board_values(3, j, 0);
                    score.calculate_score(tmp2);
                }
                else if (tmp1 != tmp3 && tmp2 == 0 && tmp1 != 0 && tmp3 != tmp4)
                {
                    board.set_board_values(1, j, tmp3);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(3, j, 0);
                }
                else if (tmp1 != tmp4 && tmp2 == 0 && tmp3 == 0 && tmp1 != 0 && tmp4 != 0)
                {
                    board.set_board_values(1, j, tmp4);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(3, j, 0);
                }
                else if(tmp1 == 0 && tmp2 != 0 && tmp3 != 0 && tmp4 != 0 && tmp2 != tmp3 && tmp2 != tmp4 && tmp3 != tmp4)
                {
                    board.set_board_values(0, j, tmp2);
                    board.set_board_values(1, j, tmp3);
                    board.set_board_values(2, j, tmp4);
                    board.set_board_values(3, j, 0);
                }
                else if (tmp1 == 0 && tmp2 != 0)
                {
                    board.set_board_values(0, j, tmp2);
                    board.set_board_values(1, j, tmp3);
                    board.set_board_values(2, j, tmp4);
                    board.set_board_values(3, j, 0);
                }
                else if (tmp1 == 0 && tmp2 == 0 && tmp3 != 0)
                {
                    board.set_board_values(0, j, tmp3);
                    board.set_board_values(1, j, tmp4);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(3, j, 0);
                }
                else if (tmp2 == 0 && tmp2 == tmp3 && tmp2 == tmp1 && tmp4 != 0)
                {
                    board.set_board_values(0, j, tmp4);
                    board.set_board_values(3, j, 0);
                }
                else if(tmp3 == tmp4 && tmp3 != tmp2 && tmp1 != tmp2 && tmp2 != 0 && tmp3 != 0)
                {
                    tmp3 = tmp3 * 2;
                    board.set_board_values(2, j, tmp3);
                    board.set_board_values(3, j, 0);
                    score.calculate_score(tmp3);

                } else if(tmp1 != 0 && tmp2 != 0 && tmp3 == 0 && tmp4 != 0 && tmp1 != tmp2 && tmp2 != tmp4)
                {
                    board.set_board_values(2, j, tmp4);
                    board.set_board_values(3, j, 0);
                }
            }
            return;
        }
        private void move_blocks_down()
        {
            int i = 0, j = 0;
            int tmp1, tmp2, tmp3, tmp4;
            for (j = 0; j <= 3; j++)
            {
                tmp4 = board.get_board_values(i, j);        //uppermost
                tmp3 = board.get_board_values(i + 1, j);    //2nd uppermost
                tmp2 = board.get_board_values(i + 2, j);
                tmp1 = board.get_board_values(i + 3, j);
                                                         
                if (tmp1 == tmp2 && tmp1 != 0 && tmp2 != 0)
                {
                    tmp1 = tmp1 * 2;
                    board.set_board_values(3, j, tmp1);
                    board.set_board_values(2, j, 0);
                    tmp2 = board.get_board_values(2, j);
                    if (tmp3 == tmp4 && tmp3 != 0 && tmp4 != 0)
                    {
                        tmp3 = tmp3 * 2;
                        board.set_board_values(2, j, tmp3);
                        board.set_board_values(1, j, 0); 
                        board.set_board_values(0, j, 0); 
                        score.calculate_score(tmp3);
                    }
                    else
                    {
                        board.set_board_values(2, j, tmp3);
                        board.set_board_values(1, j, tmp4);
                        board.set_board_values(0, j, 0);
                    }

                    score.calculate_score(tmp1);

                }
                else if (tmp1 == tmp3 && tmp2 == 0 && tmp1 != 0 && tmp3 != 0)
                {
                    tmp1 = tmp1 * 2;
                    board.set_board_values(3, j, tmp1);
                    board.set_board_values(2, j, tmp4);
                    board.set_board_values(1, j, 0);
                    board.set_board_values(0, j, 0);
                    score.calculate_score(tmp1);
                }
                else if (tmp1 == tmp4 && tmp2 == 0 && tmp3 == 0 && tmp1 != 0)
                {
                    tmp1 = tmp1 * 2;
                    board.set_board_values(3, j, tmp1);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(1, j, 0);
                    board.set_board_values(0, j, 0);
                    score.calculate_score(tmp1);
                }
                else if(tmp2 == tmp3 && tmp2 != 0 && tmp1 != 0 && tmp1 != tmp2 && tmp3 != tmp4 && tmp4 != 0)
                {
                    tmp2 *= tmp2;
                    board.set_board_values(2, j, tmp2);
                    board.set_board_values(1, j, tmp4);
                    board.set_board_values(0, j, 0);
                    score.calculate_score(tmp2);

                }
                else if(tmp2 == tmp3 && tmp2 != tmp1 && tmp1 == 0 && tmp3 != tmp4 && tmp4 != 0)
                {
                    tmp2 *= 2;
                    board.set_board_values(3, j, tmp2);
                    board.set_board_values(2, j, tmp4);
                    board.set_board_values(1, j, 0);
                    board.set_board_values(0, j, 0);
                    score.calculate_score(tmp2);
                }
                else if(tmp2 == tmp3 && tmp2 != tmp1 && tmp3 != tmp4 && tmp1 != 0 && tmp4 != 0)
                {
                    tmp2 *= 2;
                    board.set_board_values(2, j, tmp2);
                    board.set_board_values(1, j, tmp4);
                    board.set_board_values(0, j, 0);
                    score.calculate_score(tmp2);
                }
                else if(tmp2 == tmp3 && tmp2 != 0 && tmp1 != 0 && tmp1 != tmp2)
                {
                    tmp2 *= 2;
                    board.set_board_values(2, j, tmp2);
                    board.set_board_values(1, j, tmp4);
                    board.set_board_values(0, j, 0);
                    score.calculate_score(tmp2);
                }
                else if (tmp2 == tmp3 && tmp1 == 0 && tmp2 != 0 && tmp3 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(3, j, tmp2);
                    board.set_board_values(2, j, tmp4);
                    board.set_board_values(1, j, 0);
                    board.set_board_values(1, j, 0);
                    score.calculate_score(tmp2);
                }
                else if (tmp2 == tmp4 && tmp1 == 0 && tmp3 == 0 && tmp2 != 0 && tmp4 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(3, j, tmp2);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(1, j, 0);
                    board.set_board_values(0, j, 0);
                    score.calculate_score(tmp2);
                }
                else if (tmp2 == tmp4 && tmp1 != 0 && tmp3 == 0 && tmp2 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(2, j, tmp2);
                    board.set_board_values(1, j, 0);
                    board.set_board_values(0, j, 0);
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
                    board.set_board_values(2, j, 0);
                    board.set_board_values(1, j, 0);
                    board.set_board_values(0, j, 0);
                    score.calculate_score(tmp3);
                }
                else if (tmp3 == tmp4 && tmp3 != tmp2 && tmp1 != tmp2)
                {
                    tmp3 = tmp3 * 2;
                    board.set_board_values(1, j, tmp3);
                    board.set_board_values(0, j, 0);
                    score.calculate_score(tmp3);

                }
                else if(tmp1 == 0 && tmp2 != 0 && tmp3 == 0 && tmp4 == 0)
                {
                    board.set_board_values(3, j, tmp2);
                    board.set_board_values(2, j, 0);

                }
                else if (tmp2 != 0 && tmp1 == 0 && tmp3 == 0 && tmp4 == 0)
                {
                    board.set_board_values(3, j, tmp2);
                    board.set_board_values(2, j, 0);
                }
                else if (tmp1 == 0 && tmp2 == 0 && tmp3 == 0 && tmp4 != 0)
                {
                    board.set_board_values(3, j, tmp4);
                    board.set_board_values(0, j, 0);
                }
                else if (tmp1 != tmp3 && tmp2 == 0 && tmp1 != 0 && tmp3 != tmp4)
                {
                    board.set_board_values(2, j, tmp3);
                    board.set_board_values(1, j, tmp4);
                    board.set_board_values(0, j, 0);
                }
                else if (tmp1 != tmp4 && tmp2 == 0 && tmp1 != 0 && tmp3 == 0 && tmp4 != 0)
                {
                    board.set_board_values(3, j, tmp4);
                    board.set_board_values(2, j, 0);
                    board.set_board_values(1, j, 0);
                }
                else if (tmp2 == tmp3 && tmp1 != tmp2 && tmp1 != 0 && tmp2 != 0 && tmp3 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(2, j, tmp2);
                    board.set_board_values(1, j, 0);
                    score.calculate_score(tmp2);
                }
                else if(tmp1 != 0 && tmp2 != 0 && tmp3 == 0 && tmp4 != 0 && tmp1 != tmp2 && tmp2 != tmp4)
                {
                    board.set_board_values(1, j, tmp4);
                    board.set_board_values(0, j, 0);
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
                    board.set_board_values(0, j, 0);
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
                    board.set_board_values(0, j, 0);
                }
                else if(tmp1 != 0 && tmp2 == 0 && tmp3 == 0 && tmp4 == 0)
                {
                    break;
                }
                else if (tmp1 != tmp3 && tmp2 == 0)
                {
                    board.set_board_values(1, j, tmp3);
                    board.set_board_values(2, j, tmp4);
                    board.set_board_values(3, j, 0);
                }

            }

            }
        private void move_blocks_left()
        {
            int i, j = 0;
            int tmp1, tmp2, tmp3, tmp4;

            for (i = 0; i <= 3; i++)
            {
                tmp1 = board.get_board_values(i, j);  //leftmost
                tmp2 = board.get_board_values(i, j + 1); 
                tmp3 = board.get_board_values(i, j + 2); 
                tmp4 = board.get_board_values(i, j + 3); 

                if (tmp1 == tmp2 && tmp1 != 0 && tmp2 != 0)
                {
                    tmp1 = tmp1 * 2;
                    board.set_board_values(i, 0, tmp1);

                    board.set_board_values(i, 1, 0);
                    tmp2 = board.get_board_values(i, 1);
                    if (tmp3 == tmp4 && tmp3 != 0 && tmp4 != 0)
                    {
                        tmp3 = tmp3 * 2;
                        board.set_board_values(i, 1, tmp3);
                        board.set_board_values(i, 2, 0); 
                        board.set_board_values(i, 3, 0); 
                        score.calculate_score(tmp3);
                    }
                    else
                    {
                        board.set_board_values(i, 1, tmp3);
                        board.set_board_values(i, 2, tmp4);
                        board.set_board_values(i, 3, 0);
                    }

                    score.calculate_score(tmp1);

                }
                else if (tmp1 == tmp3 && tmp2 == 0 && tmp1 != 0 && tmp3 != 0)
                {
                    tmp1 = tmp1 * 2;
                    board.set_board_values(i, 0, tmp1);
                    board.set_board_values(i, 1, tmp4);
                    board.set_board_values(i, 2, 0);
                    board.set_board_values(i, 3, 0);
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
                    board.set_board_values(i, 3, 0);
                    score.calculate_score(tmp3);

                }
                else if (tmp2 == tmp3 && tmp1 == 0 && tmp2 != 0 && tmp3 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(i, 0, tmp2);
                    board.set_board_values(i, 1, tmp4);
                    board.set_board_values(i, 2, 0);
                    board.set_board_values(i, 3, 0);
                    score.calculate_score(tmp2);
                }
                else if (tmp2 == tmp4 && tmp1 == 0 && tmp3 == 0 && tmp2 != 0 && tmp4 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(i, 0, tmp2);
                    board.set_board_values(i, 1, 0);
                    board.set_board_values(i, 2, 0);
                    board.set_board_values(i, 3, 0);
                    score.calculate_score(tmp2);
                }
                else if (tmp2 == tmp4 && tmp1 != 0 && tmp3 == 0 && tmp2 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(i, 1, tmp2);
                    board.set_board_values(i, 2, 0);
                    board.set_board_values(i, 3, 0);
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
                    board.set_board_values(i, 1, 0);
                    board.set_board_values(i, 2, 0);
                    board.set_board_values(i, 3, 0);
                    score.calculate_score(tmp3);
                }
                else if(tmp1 != 0 && tmp2 != 0 && tmp3 == 0 && tmp4 != 0 && tmp1 != tmp2 && tmp2 != tmp4)
                {
                    board.set_board_values(i, 2, tmp4);
                    board.set_board_values(i, 3, 0);
                }
                else if (tmp1 == 0 && tmp2 == 0 && tmp3 == 0 && tmp4 != 0)
                {
                    board.set_board_values(i, 0, tmp4);
                    board.set_board_values(i, 1, 0);
                    board.set_board_values(i, 2, 0);
                    board.set_board_values(i, 3, 0);
                }
                else if (tmp1 != tmp2 && tmp2 != 0 && tmp2 == tmp3 && tmp1 != 0 && tmp3 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(i, 1, tmp2);
                    board.set_board_values(i, 2, tmp4);
                    board.set_board_values(i, 3, 0);
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
                        board.set_board_values(i, 2, 0);
                    }
                    board.set_board_values(i, 3, 0);
                }
                else if (tmp1 != tmp4 && tmp2 == 0 && tmp3 == 0 && tmp1 != 0 && tmp4 != 0)
                {
                    board.set_board_values(i, 1, tmp4);
                    board.set_board_values(i, 2, 0);
                    board.set_board_values(i, 3, 0);
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
                    board.set_board_values(i, 3, 0);
                }
                else if (tmp1 == 0 && tmp2 == 0 && tmp3 != 0)
                {
                    board.set_board_values(i, 0, tmp3);
                    board.set_board_values(i, 1, tmp4);
                    board.set_board_values(i, 2, 0);
                    board.set_board_values(i, 3, 0);
                }
                else if (tmp2 == 0 && tmp2 == tmp3 && tmp2 == tmp1 && tmp4 != 0)
                {
                    board.set_board_values(i, 0, tmp4);
                    board.set_board_values(i, 3, 0);
                }
                else if (tmp1 != tmp3 && tmp2 == 0)
                {
                    board.set_board_values(i, 1, tmp3);
                    board.set_board_values(i, 2, tmp4);
                    board.set_board_values(i, 3, 0);
                }
            }
            return;
        }
        private void move_blocks_right()
        {
            int i, j = 0;
            int tmp1, tmp2, tmp3, tmp4;
            for (i = 0; i <= 3; i++)
            {
                tmp4 = board.get_board_values(i, j); 
                tmp3 = board.get_board_values(i, j + 1);
                tmp2 = board.get_board_values(i, j + 2);
                tmp1 = board.get_board_values(i, j + 3);

                if (tmp1 == tmp2 && tmp1 != 0 && tmp2 != 0)
                {
                    tmp1 = tmp1 * 2;
                    board.set_board_values(i, 3, tmp1);

                    board.set_board_values(i, 2, 0);
                    tmp2 = board.get_board_values(i, 2);
                    if (tmp3 == tmp4 && tmp3 != 0 && tmp4 != 0)
                    {
                        tmp3 = tmp3 * 2;
                        board.set_board_values(i, 2, tmp3);
                        board.set_board_values(i, 1, 0); 
                        board.set_board_values(i, 0, 0); 
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
                        board.set_board_values(i, 0, 0);
                    }

                    score.calculate_score(tmp1);

                }
                else if (tmp1 == tmp3 && tmp2 == 0 && tmp1 != 0 && tmp3 != 0)
                {
                    tmp1 = tmp1 * 2;
                    board.set_board_values(i, 3, tmp1);
                    board.set_board_values(i, 2, tmp4);
                    board.set_board_values(i, 1, 0);
                    board.set_board_values(i, 0, 0);
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
                    board.set_board_values(i, 0, 0);
                    score.calculate_score(tmp3);

                }
                else if (tmp2 == tmp3 && tmp1 == 0 && tmp2 != 0 && tmp3 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(i, 3, tmp2);
                    board.set_board_values(i, 2, tmp4);
                    board.set_board_values(i, 1, 0);
                    board.set_board_values(i, 0, 0);
                    score.calculate_score(tmp2);
                }
                else if (tmp2 == tmp4 && tmp1 == 0 && tmp3 == 0 && tmp2 != 0 && tmp4 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(i, 3, tmp2);
                    board.set_board_values(i, 2, 0);
                    board.set_board_values(i, 1, 0);
                    board.set_board_values(i, 0, 0);
                    score.calculate_score(tmp2);
                }
                else if (tmp2 == tmp4 && tmp1 != 0 && tmp3 == 0 && tmp2 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(i, 2, tmp2);
                    board.set_board_values(i, 1, 0);
                    board.set_board_values(i, 0, 0);
                    score.calculate_score(tmp2);
                }
                else if (tmp3 == tmp4 && tmp1 == 0 && tmp2 == 0)
                {
                    tmp3 = tmp3 * 2;
                    board.set_board_values(i, 3, tmp3);
                    board.set_board_values(i, 2, 0);
                    board.set_board_values(i, 1, 0);
                    board.set_board_values(i, 0, 0);
                    score.calculate_score(tmp3);
                }
                else if (tmp3 == tmp4 && tmp2 == 0 && tmp1 != 0 && tmp3 != 0 && tmp1 != tmp3)
                {
                    tmp3 = tmp3 * 2;
                    board.set_board_values(i, 2, tmp3);
                    board.set_board_values(i, 1, 0);
                    board.set_board_values(i, 0, 0);
                    score.calculate_score(tmp3);
                }
                else if(tmp4 != 0 && tmp3 == 0 && tmp2 != 0 && tmp1 != 0 && tmp4 != tmp2 && tmp2 != tmp1)
                {
                    board.set_board_values(i, 1, tmp4);
                    board.set_board_values(i, 0, 0);
                }
                else if (tmp1 == 0 && tmp2 == 0 && tmp3 == 0 && tmp4 != 0)
                {
                    board.set_board_values(i, 3, tmp4);
                    board.set_board_values(i, 2, 0);
                    board.set_board_values(i, 1, 0);                   
                    board.set_board_values(i, 0, 0);
                }
                else if (tmp2 == tmp3 && tmp1 != tmp2 && tmp2 != 0 && tmp1 != 0 && tmp3 != 0)
                {
                    tmp2 = tmp2 * 2;
                    board.set_board_values(i, 2, tmp2);
                    board.set_board_values(i, 1, tmp4);
                    board.set_board_values(i, 0, 0);
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
                    board.set_board_values(i, 1, 0);
                    board.set_board_values(i, 0, 0);
                } else if(tmp1 != 0 && tmp2 != 0 && tmp3 == 0 && tmp4 != 0 && tmp1 != tmp2 && tmp1 != tmp4 && tmp2 != tmp4)
                {
                    board.set_board_values(i, 1, tmp4);
                    board.set_board_values(i, 0, 0);
                }
                else if (tmp1 != tmp4 && tmp2 == 0 && tmp3 == 0 && tmp1 != 0 && tmp4 != 0)
                {
                    board.set_board_values(i, 2, tmp4);
                    board.set_board_values(i, 1, 0);
                    board.set_board_values(i, 0, 0);
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
                    board.set_board_values(i, 0, 0);
                }
                else if (tmp1 == 0 && tmp2 == 0 && tmp3 != 0)
                {
                    board.set_board_values(i, 3, tmp3);
                    board.set_board_values(i, 2, tmp4);
                    board.set_board_values(i, 1, 0);
                    board.set_board_values(i, 0, 0);
                }
                else if (tmp2 == 0 && tmp2 == tmp3 && tmp2 == tmp1 && tmp4 != 0)
                {
                    board.set_board_values(i, 3, tmp4);
                    board.set_board_values(i, 0, 0);
                }
                else if (tmp1 != tmp3 && tmp2 == 0)
                {
                    board.set_board_values(i, 2, tmp3);
                    board.set_board_values(i, 1, tmp4);
                    board.set_board_values(i, 0, 0);
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

