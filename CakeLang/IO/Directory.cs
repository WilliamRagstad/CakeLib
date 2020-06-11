using System;
using System.Collections.Generic;
using Console = EzConsole.EzConsole;

namespace CakeLang
{
    internal class Directory
    {
        public Directory(string name, Directory[] directories = null, File[] files = null)
        {
            Name = name;
            if (directories != null) Directories = new List<Directory>(directories);
            else Directories = new List<Directory>();
            if (files != null) Files = new List<File>(files);
            else Files = new List<File>();
        }

        public Directory(string name, Directory directory = null, File file = null) : this(name, new[] { directory }, new[] { file }) { }
        public Directory(string name) : this(name, new Directory[] { }, new File[] { }) { }

        public string Name { get; }
        public List<Directory> Directories { get; }
        public List<File> Files { get; }

        public void Create(string parentPath)
        {
            string path = System.IO.Path.Combine(parentPath, Name);
            if (System.IO.Directory.Exists(path)) System.IO.Directory.Delete(path, true);
            
            Console.WriteLine("Generating Directory: " + path, ConsoleColor.Yellow);
            System.IO.Directory.CreateDirectory(path);
            

            foreach (Directory d in Directories) if (d != null) d.Create(path);
            foreach (File f in Files) if (f != null) f.Create(path);
        }

        public void Add(File file) => Files.Add(file);
        public void Add(Directory directory) => Directories.Add(directory);
    }
}