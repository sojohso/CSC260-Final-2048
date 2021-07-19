using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_Final.Classes.Color
{
    class Color
    {
        ConsoleColor a; //0
        ConsoleColor b; //2
        ConsoleColor c; //4
        ConsoleColor d; //8
        ConsoleColor e; //16
        ConsoleColor f; //32
        ConsoleColor g; //64
        ConsoleColor h; //128
        ConsoleColor i; //256
        ConsoleColor j; //512
        ConsoleColor k; //1024
        ConsoleColor l; //2048
        ConsoleColor m; //defult
        public Color()
        {
            a = ConsoleColor.White;
            b = ConsoleColor.Yellow;
            c = ConsoleColor.Cyan;
            d = ConsoleColor.DarkGreen;
            e = ConsoleColor.DarkMagenta;
            f = ConsoleColor.DarkRed;
            g = ConsoleColor.DarkYellow;
            h = ConsoleColor.Gray;
            i = ConsoleColor.DarkGray;
            j = ConsoleColor.Blue;
            k = ConsoleColor.Green;
            l = ConsoleColor.DarkCyan;
            m = ConsoleColor.Red;

        }

        internal ConsoleColor get_color(int number)
        {
            switch (number)
            {
                case 0:
                    return a;
                case 2:
                    return b;
                case 4:
                    return c;
                case 8:
                    return d;
                case 16:
                    return e;
                case 32:
                    return f;
                case 64:
                    return g;
                case 128:
                    return h;
                case 256:
                    return i;
                case 512:
                    return j;
                case 1024:
                    return k;
                case 2048:
                    return l;
                default:
                    return m;

            }
        }
    }
}
