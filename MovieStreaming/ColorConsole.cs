using System;

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

        public static void WriteLineRed(string message) => WriteLineColor(message, ConsoleColor.Red);        

        public static void WriteLineCyan(string message) => WriteLineColor(message, ConsoleColor.Cyan);

        public static void WriteLineGray(string message) => WriteLineColor(message, ConsoleColor.Gray);

        public static void WriteLineWhite(string message) => WriteLineColor(message, ConsoleColor.White);

        public static void WriteLineMagenta(string message) => WriteLineColor(message, ConsoleColor.Magenta);
    }
}
