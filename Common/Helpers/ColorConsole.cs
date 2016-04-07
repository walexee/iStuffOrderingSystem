using System;

namespace Common.Helpers
{
    public class ColorConsole
    {
        public static void WriteGreen(string text, params object[] args)
        {
            WriteLine(ConsoleColor.Green, text, args);
        }

        public static void WriteRed(string text, params object[] args)
        {
            WriteLine(ConsoleColor.Red, text, args);
        }

        public static void WriteBlue(string text, params object[] args)
        {
            WriteLine(ConsoleColor.Blue, text, args);
        }

        public static void WriteYellow(string text, params object[] args)
        {
            WriteLine(ConsoleColor.Yellow, text, args);
        }

        public static void WriteLine(ConsoleColor color, string text, params object[] args)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text, args);

            Console.ResetColor();
        }
    }
}
