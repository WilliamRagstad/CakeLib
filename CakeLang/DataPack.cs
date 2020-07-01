using CakeLang.Cake;
using CakeLang.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using Console = EzConsole.EzConsole;

namespace CakeLang
{
    public class DataPack
    {
        #region Data Pack Default Configs
        private static short pack_format = 5;
        private static byte[] pack_icon = { };
        #endregion

        public DataPack(string _name, string _description, string _namespace)
        {
            Name = _name;
            Description = _description;
            Namespace = _namespace;

            Advancements = new List<AAdvancement>();
            Functions = new EventList<Function>();
            LootTables = new List<ALootTable>();
            Predicates = new List<APredicate>();
            Recipes = new List<ARecipe>();
            Structures = new List<AStructure>();

            Tags = new Tags();
            Dimensions = new List<ADimension>();

            Functions.SetOnAdd((Function f) => {
                if (f is ArgumentsFunction) (f as ArgumentsFunction).ParentDataPacks.Add(this);
            });
        }

        public string Name { get; }
        public string Description { get; }
        public string Namespace { get; }

        public List<AAdvancement> Advancements;
        public EventList<Function> Functions;
        public List<ALootTable> LootTables;
        public List<APredicate> Predicates;
        public List<ARecipe> Recipes;
        public List<AStructure> Structures;

        public Tags Tags;
        public List<ADimension> Dimensions;

        public void CompileAndInject(string mcWorldName)
        {
            Compile().Inject(mcWorldName);
        }
        public Pack Compile()
        {
            Console.WriteLine("Compiling Data Pack...", ConsoleColor.Yellow);

            Directory ns = new Directory(Namespace);

            if (Advancements.Count > 0) ns.Add(new Directory("advancements", null, ToFiles(Advancements.ToArray())));
            if (Functions.Count > 0) ns.Add(new Directory("functions", null, ToFiles(Functions.ToArray())));
            if (LootTables.Count > 0) ns.Add(new Directory("loot_tables", null, ToFiles(LootTables.ToArray())));
            if (Predicates.Count > 0) ns.Add(new Directory("predicates", null, ToFiles(Predicates.ToArray())));
            if (Recipes.Count > 0) ns.Add(new Directory("recipes", null, ToFiles(Recipes.ToArray())));
            if (Structures.Count > 0) ns.Add(new Directory("structures", null, ToFiles(Structures.ToArray())));

            if (Tags.HasTags) ns.Add(new Directory("tags", Tags.ToDirectories()));

            if (Dimensions.Count > 0)
            {
                List<Directory> dimensionNamespaces = new List<Directory>();
                for (int i = 0; i < Dimensions.Count; i++) dimensionNamespaces.Add(Dimensions[0].ToDimensionType());
                ns.Add(new Directory("dimension_type‌", dimensionNamespaces.ToArray()));

                List<Directory> dimensions = new List<Directory>();
                for (int i = 0; i < Dimensions.Count; i++) dimensions.Add(Dimensions[0].ToDimension());
                ns.Add(new Directory("dimension‌", dimensions.ToArray()));
            }

            return new Pack(
                new Directory(Name,
                    new Directory("data", ns),
                    new File("pack.mcmeta",
                        JSON.JSON.ObjectBuilder(true, "", true, new KeyValuePair<string, object>("pack",
                            JSON.JSON.ObjectBuilder(true, "\t", false,
                                new KeyValuePair<string, object>("pack_format", pack_format),
                                new KeyValuePair<string, object>("description", Description)
                            )
                        ))
                    )
                )
            );
        }

        private File[] ToFiles(IMCModel[] models)
        {
            List<File> files = new List<File>();
            for (int i = 0; i < models.Length; i++) files.AddRange(models[i].ToFiles());
            return files.ToArray();
        }


        #region Functions
        public string[] Call(string functionName, params object[] args)
        {
            for (int i = 0; i < Functions.Count; i++)
            {
                if (Functions[i].Name == functionName)
                {
                    if (Functions[i] is ArgumentsFunction) return Call(Functions[i] as ArgumentsFunction, args);
                    else throw new ArgumentException("You can not call a function that is not of the type 'ArgumentsFunction'!");
                }
            }
            throw new ArgumentException("The specified function does not exist!");
        }
        public string[] Call(ArgumentsFunction function, params object[] args)
        {
            if (args.Length != function.Arguments.Count) throw new ArgumentException("Invalid ammount of arguments to function '" + function.Name + "'");

            List<string> code = new List<string>();

            Dictionary<string, Type>.KeyCollection.Enumerator argumentKeys = function.Arguments.Keys.GetEnumerator();
            argumentKeys.MoveNext();
            for (int i = 0; i < function.Arguments.Count; i++)
            {
                code.Add("data modify storage " + CakeLang.StorageRoot + ' ' + CakeLang.StorageFunctions + '.' + function.Name + '.' + CakeLang.StorageFunctionsArguments + '.' + argumentKeys.Current + " set value " + argumentToString(args[i]));
                argumentKeys.MoveNext();
            }

            code.Add("function " + Namespace + ':' + function.Name);

            return code.ToArray();
        }

        private string argumentToString<T>(T argument)
        {
            if (argument is string)
            {
                string str = argument.ToString();
                if (str.StartsWith('{') && str.EndsWith('}')) return str;
                else return '"' + str + '"';
            }
            // https://minecraft.gamepedia.com/NBT_format#NBT_definition
            else if (argument is int) return argument.ToString();
            else if (argument is byte) return argument.ToString() + 'b';
            else if (argument is short) return argument.ToString() + 's';
            else if (argument is long) return argument.ToString() + 'l';
            else if (argument is float) return argument.ToString().Replace(',', '.') + 'f';
            else if (argument is double) return argument.ToString().Replace(',', '.') + 'd';
            else if (argument.GetType().IsArray)
            {
                IEnumerable arr = argument as IEnumerable;
                string result = "[";

                bool isFirst = true;
                foreach (object element in arr)
                {
                    if (isFirst)
                    {
                        if (element is int) result += "I;";
                        else if (element is byte) result += "B;";
                        else if (element is long) result += "L;";
                        isFirst = false;
                    }

                    result += argumentToString(element) + ',';
                }
                result = result.TrimEnd(',');
                return result + ']';
            }
            else return argument.ToString().Replace(',', '.');
        }
        #endregion
    }
}
