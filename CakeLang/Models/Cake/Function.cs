namespace CakeLang.Cake
{
    public class Function : AFunction
    {
        /*
        public Function(string name, params Command[] commands) : base(name, EvaluateCommands(commands))
 

        public static string[] EvaluateCommands(Command[] commands)
        {

        }
        */
        public Function(string name, params string[] content) : base(name, content)
        {
        }
    }
}