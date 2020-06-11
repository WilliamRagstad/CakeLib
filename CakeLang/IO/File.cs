using System;
using Console = EzConsole.EzConsole;

namespace CakeLang
{
    internal class File
    {
        public File(string name, byte[] content)
        {
            Name = name;
            Content = content;
        }

        public File(string name, string content) : this(name, System.Text.Encoding.UTF8.GetBytes(content)) { }
        public File(string name, string[] content) : this(name, System.Text.Encoding.UTF8.GetBytes(string.Join('\n', content))) { }

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