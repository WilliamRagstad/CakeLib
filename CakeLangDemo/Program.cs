using System;
using CakeLang;
using Console = EzConsole.EzConsole;

namespace CakeLangDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("=== "); Console.Write("Cake", ConsoleColor.Red); Console.Write("Lang", ConsoleColor.White); Console.WriteLine(" Demo ===");

            Console.WriteLine("For version: " + CakeLang.CakeLang.Version.ToString() + '\n', ConsoleColor.DarkYellow);

            // --- Data Pack Code -----------------------------------------

            DataPack demoPack = new DataPack("Demo Pack", "A demonstration of CakeLang!", "demopack");

            string message = "Hi there";

            demoPack.Functions.Add(new CakeLang.Vanilla.Function("fun1",
                "say " + message + " @s! How are you"
                ));
            demoPack.Functions.Add(new CakeLang.Vanilla.Function("fun1",
                "say " + message + " @s! How are you"
                ));
            demoPack.Functions.Add(new CakeLang.Vanilla.Function("fun1",
                "say " + message + " @s! How are you"
                ));
            demoPack.Functions.Add(new CakeLang.Vanilla.Function("fun1",
                "say " + message + " @s! How are you"
                ));
            demoPack.Functions.Add(new CakeLang.Vanilla.Function("fun1",
                "say " + message + " @s! How are you"
                ));
            demoPack.Functions.Add(new CakeLang.Vanilla.Function("fun1",
                "say " + message + " @s! How are you"
                ));

            demoPack.Functions.Add(new CakeLang.Cake.Function("fun2"));


            demoPack.CompileAndInject("CakeLang Demo");

            // --- Data Pack Code -----------------------------------------
        }
    }
}
