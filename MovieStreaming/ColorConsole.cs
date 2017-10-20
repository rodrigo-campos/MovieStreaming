using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStreaming
{
    public static class ColorConsole
    {
        private static void WriteLineColor(string message, ConsoleColor consoleColor)
        {
            var beforeColor = Console.ForegroundColor;

            Console.ForegroundColor = consoleColor;

            Console.WriteLine(message);

            Console.ForegroundColor = beforeColor;
        }

        public static void WriteLineYellow(string message) => WriteLineColor(message, ConsoleColor.Yellow);

        public static void WriteLineGreen(string message) => WriteLineColor(message, ConsoleColor.Green);
    }
}
