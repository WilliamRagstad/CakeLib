using System;
using System.Collections.Generic;

namespace CakeLang
{
    public class Function : IMCModel
    {
        internal Function(string name)
        {
            Name = name.ToLower();
        }
        public Function(string name, params string[] content) : this(name)
        {
            Content = content;
        }
        public Function(string name, params string[][] content) : this(name)
        {
            List<string> lines = new List<string>();
            for (int i = 0; i < content.Length; i++)
                for (int j = 0; j < content[i].Length; j++) lines.Add(content[i][j]);
            Content = lines.ToArray();
        }

        public string Name { get; set; }
        public string[] Content { get; set; }

        public Function FromFile(string file)
        {
            if (!System.IO.File.Exists(file))
            {
                throw new ErrorManager.IOError("Source file '" + file + "' does not exist.");
            }

            string functionName = System.IO.Path.GetFileNameWithoutExtension(file);
            string[] lines = System.IO.File.ReadAllLines(file);

            return new Function(functionName, lines);
        }
        File[] IMCModel.ToFiles()
        {
            return new[] {
                new File(Name + ".mcfunction", Content)
            };
        }
    }
}
