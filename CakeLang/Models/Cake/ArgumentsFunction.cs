using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CakeLang.Cake
{
    public class ArgumentsFunction : Function
    {
        public ArgumentsFunction(string name, Dictionary<string, Type> arguments, params string[] contents) : this(name, arguments, true, true, new[] { contents }) { }
        public ArgumentsFunction(string name, Dictionary<string, Type> arguments, params string[][] contents) : this(name, arguments, true, true, contents) { }
        public ArgumentsFunction(string name, Dictionary<string, Type> arguments, bool requireArguments, bool runOnce, params string[] contents) : this(name, arguments, requireArguments, runOnce, new[] { contents }) { }
        public ArgumentsFunction(string name, Dictionary<string, Type> arguments, bool requireArguments, bool runOnce, params string[][] content) : base(name)
        {
            Arguments = arguments;
            List<string> lines = new List<string>();

            // Check parameters before executing
            if (requireArguments) lines.Add("execute unless data storage " + CakeLang.StorageRoot + ' ' + CakeLang.StorageFunctions + '.' + Name + '.' + CakeLang.StorageFunctionsArguments + " run tellraw @a [{\"text\":\"[Cake Warning] The function '" + Name + "' is runned without parameters. This may break parts of the function!\",\"color\":\"red\"}]");

            // Add function commands
            for (int i = 0; i < content.Length; i++)
                for (int j = 0; j < content[i].Length; j++) lines.Add(content[i][j]);

            // Clear parameters after executing
            if (runOnce) lines.Add("data remove storage " + CakeLang.StorageRoot + ' ' + CakeLang.StorageFunctions + '.' + Name + '.' + CakeLang.StorageFunctionsArguments);

            Content = lines.ToArray();
            ParentDataPacks = new List<DataPack>();
        }

        public Dictionary<string, Type> Arguments { get; }
        public List<DataPack> ParentDataPacks { get; set; }

        public string[] Call(DataPack datapack, params object[] args)
        {
            for (int i = 0; i < ParentDataPacks.Count; i++)
            {
                if (ParentDataPacks[i].Equals(datapack)) datapack.Call(this, args);
            }
            throw new ArgumentException("The datapack does not contain this function!");
        }
        public string[] Call(params object[] args)
        {
            if (ParentDataPacks.Count == 1) return ParentDataPacks[0].Call(this, args);
            else throw new ArgumentException("Too ambigious call, please specify which datapacks function you want to call!");
        }
        
    }
}
