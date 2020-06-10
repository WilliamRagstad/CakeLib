using System;
using Console = EzConsole.EzConsole;

namespace CakeLang
{
    public class File
    {
        public File(string name, byte[] content)
        {
            Name = name;
            Content = content;
        }

        public File(string name, string content) : this(name, System.Text.Encoding.UTF8.GetBytes(content)) { }

        public string Name { get; }
        public byte[] Content { get; }

        public void Create(string parentPath)
        {
            string path = System.IO.Path.Combine(parentPath, Name);
            Console.WriteLine("Generating File: " + path, ConsoleColor.Yellow);
            System.IO.File.WriteAllBytes(path, Content);
        }
    }
}