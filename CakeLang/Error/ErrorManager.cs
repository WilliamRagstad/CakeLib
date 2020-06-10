using System;
using System.Collections.Generic;
using System.Text;
using Console = EzConsole.EzConsole;

namespace CakeLang
{
    internal static class ErrorManager
    {
        public static void GeneralError(string message, string type = "ERROR") => Console.WriteLine("[" + type + "] " + message, ConsoleColor.Red);
        public static void IOError(string message) => GeneralError(message, "IO ERROR");
        public static void InjectError(string message) => GeneralError(message, "INJECTION ERROR");
    }
}
