using System;
using Console = EzConsole.EzConsole;

using CakeLang;
using CakeLang.Cake;
using CakeLang.JSON;

namespace CakeLangDemo
{
    class Program : CakeLang.Cake.Program
    {
        static void Main(string[] args)
        {
            Console.Write("=== "); Console.Write("Cake", ConsoleColor.Red); Console.Write("Lang", ConsoleColor.White); Console.WriteLine(" Demo ===");

            Console.WriteLine("For version: " + CakeLang.CakeLang.Version.ToString() + '\n', ConsoleColor.DarkYellow);

            // --- Data Pack Code -----------------------------------------

            DataPack demoPack = new DataPack("Demo Pack", "A demonstration of CakeLang!", "demopack");

            string message = "Hello friend";
            demoPack.Functions.Add(new Function("fun2",
                AsAt("s", Run(
                    Tellraw('s', JSON.Text(message, JSON.StyleFormattings.Color(Colors.Aqua), JSON.StyleFormattings.Bold))
                ))
            ));
            demoPack.Functions.Add(new Function("flying",
                If.Block("~ ~-1 ~", Namespace("air"),
                    Run(
                        Tellraw('s', JSON.Text("Bro, you're flying!", JSON.StyleFormattings.Color(Colors.Yellow), JSON.StyleFormattings.Italic))
                    )
                ),
                Unless.Block("~ ~-1 ~", Namespace("air"),
                    Run(
                        Tellraw('s', JSON.Text("Nah, not flyin' yet", JSON.StyleFormattings.Color(Colors.Yellow), JSON.StyleFormattings.Underlined))
                    )
                )
            ));

            ArgumentsFunction introduce = new ArgumentsFunction("introduce", new System.Collections.Generic.Dictionary<string, Type>()
            {
                ["name"] = typeof(string),
                ["position"] = typeof(float[])
            },
                Tellraw(Selector.Executor, JSON.ArrayBuilder(
                    JSON.Text("My name is ", JSON.StyleFormattings.Bold, JSON.StyleFormattings.Color(Colors.Gold)),
                    JSON.FunctionVariable("introduce", "name", JSON.StyleFormattings.Italic, JSON.StyleFormattings.Color(Colors.Yellow)),
                    JSON.Text(", I'm at ", JSON.StyleFormattings.Bold, JSON.StyleFormattings.Color(Colors.Gold)),
                    JSON.FunctionVariable("introduce", "position[0]", JSON.StyleFormattings.Italic, JSON.StyleFormattings.Color(Colors.Yellow)),
                    JSON.Text(" "),
                    JSON.FunctionVariable("introduce", "position[1]", JSON.StyleFormattings.Italic, JSON.StyleFormattings.Color(Colors.Yellow)),
                    JSON.Text(" "),
                    JSON.FunctionVariable("introduce", "position[2]", JSON.StyleFormattings.Italic, JSON.StyleFormattings.Color(Colors.Yellow)),
                    JSON.Text(" "),
                    JSON.Text("!", JSON.StyleFormattings.Bold, JSON.StyleFormattings.Color(Colors.Gold))
                    ))
            );
            demoPack.Functions.Add(introduce);

            demoPack.Functions.Add(new Function("introducePeople",
                introduce.Call("Jeff", new[] { 0.5f, 2f, -4f }),
                introduce.Call("Monica", new[] { 0, 0, 0 }),
                introduce.Call("Boobo", new[] { 6, 6, 9, -2 })
            ));

            // --- Data Pack Code -----------------------------------------

            try
            {
                demoPack.CompileAndInject("CakeLang Demo");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message, ConsoleColor.Red);
            }
        }
    }
}
