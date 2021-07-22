using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_Final.Classes.Color_Output
{
    class Color_Output : IDisposable
    {
        public Color_Output (ConsoleColor fg)
        {
            Console.ForegroundColor = fg;
        }
        public void Dispose()
        {
            Console.ResetColor();
        }
    }
}
