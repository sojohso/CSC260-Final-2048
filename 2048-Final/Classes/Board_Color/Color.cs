using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_Final.Classes.ColorNS
{
    class Color
    {
        private ConsoleColor a; //0
        private ConsoleColor b; //2
        private ConsoleColor c; //4
        private ConsoleColor d; //8
        private ConsoleColor e; //16
        private ConsoleColor f; //32
        private ConsoleColor g; //64
        private ConsoleColor h; //128
        private ConsoleColor i; //256
        private ConsoleColor j; //512
        private ConsoleColor k; //1024
        private ConsoleColor l; //2048
        private ConsoleColor m; //default
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
