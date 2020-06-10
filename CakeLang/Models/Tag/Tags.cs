using System;
using System.Collections.Generic;

namespace CakeLang
{
    public class Tags
    {
        public Tags()
        {
            Blocks = new List<Block>();
            Entities = new List<Entity>();
            Fluids = new List<Fluid>();
            Functions = new List<FunctionTag>();
            Items = new List<Item>();
        }

        public List<Block> Blocks;
        public List<Entity> Entities;
        public List<Fluid> Fluids;
        public List<FunctionTag> Functions;
        public List<Item> Items;

        public bool HasTags
        {
            get
            {
                return
                    Blocks.Count > 0    ||
                    Entities.Count > 0  ||
                    Fluids.Count > 0    ||
                    Functions.Count > 0 ||
                    Items.Count > 0;
            }
        }

        internal Directory[] ToDirectories()
        {
            List<Directory> directories = new List<Directory>();

            if (Blocks.Count > 0) directories.Add(new Directory("blocks", null, ToFiles(Blocks.ToArray())));
            if (Entities.Count > 0) directories.Add(new Directory("entity_types", null, ToFiles(Entities.ToArray())));
            if (Fluids.Count > 0) directories.Add(new Directory("fluids", null, ToFiles(Fluids.ToArray())));
            if (Functions.Count > 0) directories.Add(new Directory("functions", null, ToFiles(Functions.ToArray())));
            if (Items.Count > 0) directories.Add(new Directory("items", null, ToFiles(Items.ToArray())));

            return directories.ToArray();
        }
        private File[] ToFiles(IMCModel[] models)
        {
            List<File> files = new List<File>();
            for (int i = 0; i < models.Length; i++) files.AddRange(models[i].ToFiles());
            return files.ToArray();
        }
    }
}