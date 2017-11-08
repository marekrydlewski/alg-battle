using System;
using System.Collections.Generic;
using System.Text;

namespace AlgBattle.Utils
{
    public class ConsoleSpiner
    {    
        public static void Print(string text)
        {
            Console.Write(text);
            Console.SetCursorPosition(Console.CursorLeft - text.Length, Console.CursorTop);            
        }
    }
}