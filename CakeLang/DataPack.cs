using System;
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
            Functions = new List<AFunction>();
            LootTables = new List<ALootTable>();
            Predicates = new List<APredicate>();
            Recipes = new List<ARecipe>();
            Structures = new List<AStructure>();

            Tags = new Tags();
            Dimensions = new List<ADimension>();
        }

        public string Name { get; }
        public string Description { get; }
        public string Namespace { get; }

        public List<AAdvancement> Advancements;
        public List<AFunction> Functions;
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
@"{
    ""pack"": {
    ""pack_format"": " + pack_format + @",
    ""description"": " + Description + @"
    }
}"
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
    }
}
